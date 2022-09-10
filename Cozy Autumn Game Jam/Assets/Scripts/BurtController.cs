using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BurtController : MonoBehaviour
{
	
	public float lookRadius = 10f; // How far Burt can see the player from
	
	Transform target;
	NavMeshAgent agent;
	
    // Start is called before the first frame update
    void Start()
    {
		target = PlayerManager.instance.player.transform; // make the player a target to observe
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position); // get distance to player
		
		if (distance <= lookRadius) // if the player is within a certain radius, do:
		{
			agent.SetDestination(target.position); // start navigating to the player
		}
    }
	
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
}
