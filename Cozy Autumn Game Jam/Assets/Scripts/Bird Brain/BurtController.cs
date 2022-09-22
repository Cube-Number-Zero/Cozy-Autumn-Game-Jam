using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace StarterAssets
{
	public class BurtController : MonoBehaviour
	{

		[Header("Tweakable Parameters")]
		[Tooltip("How sharp Burt is able to turn, before burt's frustration system affects it")]
		public float flightTurningSpeed = 1f;
		[Tooltip("How quickly Burt flies")]
		public float flightSpeed = 10f;
		[Tooltip("How quickly Burt improves his turning ability if he can't get to the target. Also affects how quickly Burt will teleport to his target")]
		public float frustrationGrowthRate = 1f; // The base rate at which Burt's frustration will grow, note that Burt's frustration is compounding; this just gives the frustration a kick to start the system
		[Tooltip("This affects how close Burt is willing to fly to a wall before he turns around.")]
		public float obstacleAvoidingSightRadius = 5f; // How far Burt can see when trying not to fly into walls
		[Tooltip("This affects how large a hole needs to be for Burt to think he can fly through it")]
		public float obstacleAvoidingSpherecastRadius = 0.75f;

		[Header("Don't change these in the inspector before running")]
		public bool isPlayerVisible = false;
		[HideInInspector]
		public Vector3 flightDirectionToTarget;
		public Vector3 currentFlightDirection = new Vector3(1f, 0f, 0f);
		[Range(0f, 1000f)]
		public float targetOrbitFrustration = 1f; // This will make Burt better at turning the longer he spends moving away from the target; implemented to prevent "orbiting" behavior
		private float lastTargetDistance = 0f;
		public Vector3 lastSeenPlayerLoc = new Vector3(0f, 0f, 0f);
		public bool burtDistracted = false;


		public GameObject seenPlayerTargetPrefab;


		GameObject target;
		Transform player;

		void Start()
		{

			target = GameObject.Find("BurtFlightTarget"); // allows BurtFlightTarget to be referenced at any time with "target"
			player = GameObject.Find("PlayerCapsule").transform; // allows PlayerCapsule to be referenced at any time with "player"

		}

		void Update()
		{
			if (target.GetComponent<TargetController>().lastSeenPlayerLoc != null)
				lastSeenPlayerLoc = target.GetComponent<TargetController>().lastSeenPlayerLoc.transform.position;

			isPlayerVisible = CanSeePlayer(); // Tests if Burt has line of sight to the player
			if (isPlayerVisible)
                Instantiate(seenPlayerTargetPrefab, player.position + new Vector3(0f, 1.5f, 0f), Quaternion.identity);

            flyTowardsTarget();

			testForHomicide();

		}

		void flyTowardsTarget()
		{

			flightDirectionToTarget = (target.transform.position - transform.position) / Vector3.Distance(target.transform.position, transform.position); // Update flightDirectionToTarget to the new normalized vector

			Vector3 originalDirection = currentFlightDirection;

			Vector3 turningNeeded = flightDirectionToTarget - currentFlightDirection; // How much does Burt need to turn to be facing the target?
			turningNeeded *= flightTurningSpeed * targetOrbitFrustration * Time.deltaTime / Vector3.Distance(currentFlightDirection, flightDirectionToTarget);
			currentFlightDirection += turningNeeded; // Turn slightly towards the target
			currentFlightDirection = currentFlightDirection.normalized; // Normalize to avoid Burt speeding up or slowing down

			Vector3 howMuchDidBurtJustTurn = currentFlightDirection - originalDirection; // The pinnacle of naming variables
			howMuchDidBurtJustTurn = howMuchDidBurtJustTurn.normalized * flightTurningSpeed * targetOrbitFrustration * Time.deltaTime; // god i just love normalizing vectors
			currentFlightDirection = originalDirection + howMuchDidBurtJustTurn;
			currentFlightDirection = currentFlightDirection.normalized; // Normalize to avoid Burt speeding up or slowing down...again

			if (targetOrbitFrustration > 250f) // Burt's really mad he can't get to the target, and now doesn't bother turning smoothly at all. He's out for blood. BurtFlightTarget.blood
			{
				currentFlightDirection = flightDirectionToTarget;
			}
			if (targetOrbitFrustration > 1000f) // Burt's probably stuck. If the player won't notice, teleport Burt to the target.
			{
				if (!(isPlayerVisible // If neither Burt can see the player (and thus the player can see Burt)
					|| CanSeePlayer(target.transform.position) // Nor could Burt see the player from the target (and thus the player could see Burt at the target)
					|| Vector3.Distance(player.position, target.transform.position) < 16f // Nor is the target is too close to the player
					|| Vector3.Distance(player.position, transform.position) < 4f)) // Nor is Burt too close (Note that this is more lenient. It's no fun if Burt teleports next to you, but it is if he suddenly teleports away) 
					transform.position = target.transform.position; // Then just teleport Burt to the target
			}
			if (Physics.SphereCast(transform.position, obstacleAvoidingSpherecastRadius, currentFlightDirection, out RaycastHit hit, obstacleAvoidingSightRadius))
			{
				DontHitWalls();
			}

			transform.position += currentFlightDirection * Time.deltaTime * flightSpeed; // Fly forwards, the random value is to prevent Burt from "orbiting" the target

			if (transform.position.y < 0.6f)
			{
				transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z);
				currentFlightDirection.y = 0f;
			}

			if (lastTargetDistance < Vector3.Distance(transform.position, target.transform.position))
			{
				targetOrbitFrustration += (frustrationGrowthRate + targetOrbitFrustration - 1f) * Time.deltaTime;
			}
			else
			{
                targetOrbitFrustration -= (frustrationGrowthRate + targetOrbitFrustration - 1f) * 0.5f * Time.deltaTime;
            }

			lastTargetDistance = Vector3.Distance(transform.position, target.transform.position);

			// Finally, rotate the model according to the movement direction

			Quaternion lookRotation = Quaternion.LookRotation(currentFlightDirection);
			transform.rotation = lookRotation;

			//Debug.DrawRay(transform.position, currentFlightDirection.normalized * obstacleAvoidingSightRadius * 2, Color.blue);

		}



		void DontHitWalls() // I wrote this in a fugue state or something; I don't know what it does
		{
			//Figuring out where to look for open paths

			List<Vector3> BoidHelperRayDirections = new List<Vector3>();

			int numPoints = 100;

			float turnFraction = (1f + Mathf.Sqrt(5f)) / 2f;

			for (int i = 0; i < numPoints; i++)
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
				if (Physics.SphereCast(transform.position, obstacleAvoidingSpherecastRadius, dir, out hit, obstacleAvoidingSightRadius))
				{
					if (hit.distance > furthestUnobstructedDst)
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

		public bool CanSeePlayer()
		{
			Vector3 raycastDir = new Vector3(0f, 0.5f, 0f) + player.position - transform.position; // get the direction Burt needs to look in to see the player
			Debug.DrawRay(transform.position, raycastDir, Color.green);
			var ray = new Ray(transform.position, raycastDir); // look in the direction of the player with a raycast
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				return (hit.transform.name == "PlayerCapsule"); // test if that raycast hit the player and return the output
			}
			else
			{
				return false; // return false if the raycast never saw anything at all
			}
		}

		public bool CanSeePlayer(Vector3 eyeLocation)
		{
			// If you supply CanSeePlayer with a Vector3 parameter, the function will test from that location instead of Burt's
			Vector3 raycastDir = new Vector3(0f, 0.5f, 0f) + player.position - eyeLocation;
			var ray = new Ray(eyeLocation, raycastDir);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				return (hit.transform.name == "PlayerCapsule");
			}
			else
			{
				return false;
			}
		}

		void OnDrawGizmosSelected()
		{
			//Gizmos.color = Color.red;
			//Gizmos.DrawWireSphere(raycasthitpos, 1f);
		}

		public bool testForHomicide()
		{
			if (!PlayerManager.inCabin && isPlayerVisible && !burtDistracted && Vector3.Distance(transform.position, player.position + new Vector3(0f, 2f, 0f)) < 2.5f && !PlayerManager.hasPlayerWon)
			{
				homicide();
				return true;
			}
			else
				return false;
		}

		public void homicide()
		{
			// Kill the player.
			Debug.Log("pwned");
			SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

        }
	}
}