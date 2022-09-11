using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BurtController : MonoBehaviour
{
	
	public bool isPlayerVisible = false;
	
	public Vector3 flightDirectionToTarget;

	public Vector3 currentFlightDirection = new Vector3(1f, 0f, 0f);

	public float flightTurningSpeed = 1f;

	public float flightSpeed = 10f;

	public float targetOrbitFrustration = 1f; // This will make Burt better at turning the longer he spends moving away from the target; implemented to prevent "orbiting" behavior

	public float frustrationGrowthRate = 0.01f; // The base rate at which Burt's frustration will grow, note that Burt's frustration is compounding; this just gives the frustration a kick to start the system

	private float lastTargetDistance = 0;
	
	Transform target;
	Transform player;

	//NavMeshAgent agent;       <- Ignore this, I'm working on a better method. -CM
	
    void Start()
    {
		
		target = GameObject.Find("BurtFlightTarget").transform; // allows BurtFlightTarget to be referenced at any time with "target"
		player = GameObject.Find("PlayerCapsule").transform; // allows PlayerCapsule to be referenced at any time with "player"


        //agent = GetComponent<NavMeshAgent>();       <- Ignore this, I'm working on a better method. -CM
		
    }

    void Update()
    {

		flightDirectionToTarget = (target.position - transform.position) / Vector3.Distance(target.position, transform.position); // Update flightDirectionToTarget to the new normalized vector

		isPlayerVisible = CanSeePlayer();

		flyTowardsTarget();

    }

	void flyTowardsTarget ()
	{

        Vector3 originalDirection = currentFlightDirection;

        Vector3 turningNeeded = flightDirectionToTarget - currentFlightDirection; // How much does Burt need to turn to be facing the target?
        turningNeeded *= flightTurningSpeed * targetOrbitFrustration * Time.deltaTime / Vector3.Distance(currentFlightDirection, flightDirectionToTarget);
        currentFlightDirection += turningNeeded; // Turn slightly towards the target
        currentFlightDirection = currentFlightDirection.normalized; // Normalize to avoid Burt speeding up or slowing down

		Vector3 howMuchDidBurtJustTurn = currentFlightDirection - originalDirection; // The pinnacle of naming variables
		howMuchDidBurtJustTurn = howMuchDidBurtJustTurn.normalized * flightTurningSpeed * targetOrbitFrustration * Time.deltaTime; // god i just love normalizing vectors
		currentFlightDirection = originalDirection + howMuchDidBurtJustTurn;
        currentFlightDirection = currentFlightDirection.normalized; // Normalize to avoid Burt speeding up or slowing down...again

        transform.position += currentFlightDirection * Time.deltaTime * flightSpeed; // Fly forwards, the random value is to prevent Burt from "orbiting" the target

		if (lastTargetDistance < Vector3.Distance(transform.position, target.position))
		{
			targetOrbitFrustration += (frustrationGrowthRate + targetOrbitFrustration - 1f) * Time.deltaTime;
		}

		lastTargetDistance = Vector3.Distance(transform.position, target.position);

        Debug.DrawRay(transform.position, flightDirectionToTarget, Color.red);
        Debug.DrawRay(transform.position, currentFlightDirection, Color.blue);

		if(Vector3.Distance(transform.position, target.position) < 1)
		{

			target.position = new Vector3(
				Random.Range(-11.7f, 20f),
				Random.Range(10f, 20f),
				Random.Range(-1.5f, 28.5f)
			);

			targetOrbitFrustration = 1f;
        }

		// Finally, rotate the model according to the movement direction

		Quaternion lookRotation = Quaternion.LookRotation(currentFlightDirection);
		transform.rotation = lookRotation;

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
