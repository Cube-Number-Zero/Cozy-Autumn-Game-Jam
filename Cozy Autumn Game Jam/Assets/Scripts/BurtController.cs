using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BurtController : MonoBehaviour
{
	private float canopyHeight = 1f;                                    // What is the height of the tree canopy that hides the player from Burt?				  
	public Vector3 sightlines = new Vector3(0, 0, 0); // This is used for the framework of the mechanic allowing
	public bool hasLineOfSight = false;                          // the player to hide from Burt under the forest canopy
	
	public float lookRadius = 10f; // How far Burt can see the player from
	
	
	Transform target;
	GameObject sightlinesMarker;
	NavMeshAgent agent;
	
    // Start is called before the first frame update
    void Start()
    {
		sightlinesMarker = GameObject.Find("Burt's Canopy LoS Raycast");
		target = GameObject.Find("PlayerCapsule").transform;// make the player a target to observe
        agent = GetComponent<NavMeshAgent>();
		
		//FirstPersonController FPCtest = ptarget.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
		#region MakeshiftRaycast
		
			float canopyRaycastHitRatio = (canopyHeight - target.position.y) / (transform.position.y - target.position.y); // This is how far the canopy is along the line from the player to Burt
			sightlines.x = canopyRaycastHitRatio * (transform.position.x - target.position.x) + target.position.x;         // The x coordinate of the raycast's result
			sightlines.z = canopyRaycastHitRatio * (transform.position.z - target.position.z) + target.position.z;         // The z coordinate of the raycast's result
			sightlines.y = canopyHeight;
			
			/*
				If we know what areas of the map have a canopy above them (with, for example, a 2D image of the map with areas covered in one color and areas exposed in another) we can use the x and z coordinates
				calculated above to determine if Burt has line of sight to the player.
			*/
			
			sightlinesMarker.transform.position = sightlines;
		
		#endregion
		
		
		
        float distance = Vector3.Distance(target.position, transform.position); // get distance to player
		if (distance <= lookRadius) // if the player is within a certain radius, do:
		{
			
			agent.SetDestination(target.position); // start navigating to the player
			
			if (distance <= agent.stoppingDistance)
			{
				//murder the player
			}
		}
    }
	
	
	
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}
