using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BurtController : MonoBehaviour
{
	#region AI CONTROL PARAMETERS

	// These variables change certain aspects about Burt's AI!
	// Change them as much as you want to help balance the game
	// Note: these values aren't percentages. Setting them to 100 will multiply their default values by 100x
	[Header("AI Control Parameters!")]
    [Tooltip("How quickly Burt targets the player after seeing him")]
    public float burtVisionCertaintyGrowth = 0.1f;
	[Tooltip("How quickly Burt will forget about seeing the player")]
	public float burtVisionCertaintyDecay = 0.05f;
	[Tooltip("How quickly being near Burt will decay the player's sanity")]
	public float burtProximitySanityDecay = 1f;
	[Tooltip("How aggresive Burt is when sanity is full")]
	[Range(0f, 1f)]
	public float baseAggression = 0.3f;

	#endregion

	[Header("Tweakable Parameters")]
    [Tooltip("How sharp Burt is able to turn, before burt's frustration system affects it")]
    public float flightTurningSpeed = 1f;
	[Tooltip("How quickly Burt flies")]
    public float flightSpeed = 10f;
	[Tooltip("How quickly Burt improves his turning ability if he can't get to the target. Also affects how quickly Burt will teleport to his target")]
    public float frustrationGrowthRate = 0.01f; // The base rate at which Burt's frustration will grow, note that Burt's frustration is compounding; this just gives the frustration a kick to start the system
	[Tooltip("This affects how close Burt is willing to fly to a wall before he turns around.")]
    public float obstacleAvoidingSightRadius = 5f; // How far Burt can see when trying not to fly into walls
	[Tooltip("This affects how large a hole needs to be for Burt to think he can fly through it")]
    public float obstacleAvoidingSpherecastRadius = 1f;

	[Header("Don't change these in the inspector before running")]
    public bool isPlayerVisible = false;
	[HideInInspector]
	public Vector3 flightDirectionToTarget;
	public Vector3 currentFlightDirection = new Vector3(1f, 0f, 0f);
	[Range(1f, 1000f)]
	public float targetOrbitFrustration = 1f; // This will make Burt better at turning the longer he spends moving away from the target; implemented to prevent "orbiting" behavior
	[Range(0f, 100f)]
	public float seenPlayerCertainty = 0f;
	private float lastTargetDistance = 0f;
	[Range(0f, 1f)]
    public float burtAggressionGeneral = 1f;
	public Vector3 lastSeenPlayerLoc = new Vector3(0f, 0f, 0f);



    Transform target;
	Transform player;
	PlayerManager pmanager;

    void Start()
    {
		
		target = GameObject.Find("BurtFlightTarget").transform; // allows BurtFlightTarget to be referenced at any time with "target"
		player = GameObject.Find("PlayerCapsule").transform; // allows PlayerCapsule to be referenced at any time with "player"
		pmanager = GameObject.Find("GameManager").GetComponent<PlayerManager>();

    }

    void Update()
    {
		burtAggressionGeneral = (1f - pmanager.sanity * 0.01f) * (1f - baseAggression) + baseAggression;
		if(CanSeePlayer()) // Tests if Burt has line of sight to the player
		{
			lastSeenPlayerLoc = player.position;
			seenPlayerCertainty += seenPlayerCertainty + burtVisionCertaintyGrowth * Time.deltaTime > 1f ? 1f - seenPlayerCertainty : burtVisionCertaintyGrowth * Time.deltaTime;
			isPlayerVisible = true;
		}
		else
		{
			isPlayerVisible = false;
			seenPlayerCertainty -= seenPlayerCertainty - burtVisionCertaintyDecay * Time.deltaTime < 0f ? seenPlayerCertainty : burtVisionCertaintyDecay * Time.deltaTime;
		}

		if(seenPlayerCertainty > 0)
		{
            Vector3 playerTargetOffset = (lastSeenPlayerLoc - target.position); // Where the player is in relation to the target
            playerTargetOffset.y *= 0.5f;
            target.position += playerTargetOffset.normalized * Time.deltaTime * seenPlayerCertainty * Mathf.Pow(burtAggressionGeneral, 2) * 16f;
        }

		float sanityRemoved = burtProximitySanityDecay / Mathf.Pow(Vector3.Distance(transform.position, player.position), 3f) * Time.deltaTime;
        pmanager.sanity -= pmanager.sanity <= sanityRemoved ? pmanager.sanity : sanityRemoved;
        flyTowardsTarget();

    }

	void flyTowardsTarget ()
	{

        flightDirectionToTarget = (target.position - transform.position) / Vector3.Distance(target.position, transform.position); // Update flightDirectionToTarget to the new normalized vector

        Vector3 originalDirection = currentFlightDirection;

        Vector3 turningNeeded = flightDirectionToTarget - currentFlightDirection; // How much does Burt need to turn to be facing the target?
        turningNeeded *= flightTurningSpeed * targetOrbitFrustration * Time.deltaTime / Vector3.Distance(currentFlightDirection, flightDirectionToTarget);
        currentFlightDirection += turningNeeded; // Turn slightly towards the target
        currentFlightDirection = currentFlightDirection.normalized; // Normalize to avoid Burt speeding up or slowing down

		Vector3 howMuchDidBurtJustTurn = currentFlightDirection - originalDirection; // The pinnacle of naming variables
		howMuchDidBurtJustTurn = howMuchDidBurtJustTurn.normalized * flightTurningSpeed * targetOrbitFrustration * Time.deltaTime; // god i just love normalizing vectors
		currentFlightDirection = originalDirection + howMuchDidBurtJustTurn;
        currentFlightDirection = currentFlightDirection.normalized; // Normalize to avoid Burt speeding up or slowing down...again

		if(targetOrbitFrustration > 250f)
		{
			currentFlightDirection = flightDirectionToTarget;
		}
        if (targetOrbitFrustration > 1000f && !isPlayerVisible)
        {
			transform.position = target.position;
        }
        if (Physics.SphereCast(transform.position, obstacleAvoidingSpherecastRadius, currentFlightDirection, out RaycastHit hit, obstacleAvoidingSightRadius))
		{
			DontHitWalls();
		}

        transform.position += currentFlightDirection * Time.deltaTime * flightSpeed; // Fly forwards, the random value is to prevent Burt from "orbiting" the target

		if(transform.position.y < 0.6f)
		{
			transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z);
			currentFlightDirection.y = 0f;
		}

		if (lastTargetDistance < Vector3.Distance(transform.position, target.position))
		{
			targetOrbitFrustration += (frustrationGrowthRate + targetOrbitFrustration - 1f) * Time.deltaTime;
		}

		lastTargetDistance = Vector3.Distance(transform.position, target.position);

		if(Vector3.Distance(transform.position, target.position) < 1f)
		{

			target.position = new Vector3(
				Random.Range(-11.7f, 20f),
				Random.Range(0.6f, 20.0f),
				Random.Range(-1.5f, 28.5f)
			);

			targetOrbitFrustration = 1f;
        }

		// Finally, rotate the model according to the movement direction

		Quaternion lookRotation = Quaternion.LookRotation(currentFlightDirection);
		transform.rotation = lookRotation;

        //Debug.DrawRay(transform.position, currentFlightDirection.normalized * obstacleAvoidingSightRadius * 2, Color.blue);

    }



	void DontHitWalls ()
	{
		//Figuring out where to look for open paths

		List<Vector3> BoidHelperRayDirections = new List<Vector3>();

		int numPoints = 100;

		float turnFraction = (1f + Mathf.Sqrt(5f)) / 2f;

		for(int i = 0; i < numPoints; i++)
		{
			float t = i / (numPoints - 1f);
			float inclination = Mathf.Acos(1f - 2f * t);
			float azimuth = 2f * Mathf.PI * turnFraction * i;

			float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);
			BoidHelperRayDirections.Add(new Vector3(x, y, z));
        }




		// Main algorithm for avoiding walls
		Vector3 bestDir = currentFlightDirection;
		float furthestUnobstructedDst = 0;
		RaycastHit hit;
		for (int j = 0; j < BoidHelperRayDirections.Count; j++)
		{
			// transform ray from local to world space so the smaller dir changes are examined first
			Vector3 dir = transform.TransformDirection(BoidHelperRayDirections[j]);
			if (Physics.SphereCast (transform.position, obstacleAvoidingSpherecastRadius, dir, out hit, obstacleAvoidingSightRadius))
			{
				if(hit.distance > furthestUnobstructedDst)
				{
					bestDir = dir;
					furthestUnobstructedDst = hit.distance;
				}
                Debug.DrawRay(transform.position, dir.normalized * obstacleAvoidingSightRadius, Color.red);
            }
			else
			{
				bestDir = dir;
				j = BoidHelperRayDirections.Count;
				Debug.DrawRay(transform.position, dir.normalized * obstacleAvoidingSightRadius, Color.green);
            }
		}
        currentFlightDirection = bestDir;
	}

    bool CanSeePlayer ()
	{
		Vector3 raycastDir = new Vector3(0f, 0.5f, 0f) + player.position - transform.position; // get the direction Burt needs to look in to see the player
		var ray = new Ray(transform.position, raycastDir); // look in the direction of the player with a raycast
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			return (hit.transform.name == "Capsule_Player"); // test if that raycast hit the player and return the output
		}
		else
		{
			return false; // return false if the raycast never saw anything at all
		}
	}
	
	void OnDrawGizmosSelected ()
	{
		//Gizmos.color = Color.red;
		//Gizmos.DrawWireSphere(raycasthitpos, 1f);
	}
}
