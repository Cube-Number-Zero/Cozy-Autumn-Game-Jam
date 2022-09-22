using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace StarterAssets
{
    public class TargetController : MonoBehaviour
    {
        [Header("Wandering Controls")]

        // These control the area in which Burt will wander when he doesn't know where the player is

        public Vector3 minAllowedWanderArea = new Vector3(-200f, 20f, -75f);
        public Vector3 maxAllowedWanderArea = new Vector3(200f, 30f, 300f);

        public GameObject randomWander;

        public GameObject lastSeenPlayerLoc;
        public GameObject mostRecentFootstepHeard;
        public static List<GameObject> targetCandidates = new List<GameObject>();

        void Start()
        {

        }

        void Update()
        {
            if(!PlayerManager.hasPlayerWon)
            {
            float maxPriority = 0f;
            int ID = -3;
            if (lastSeenPlayerLoc != null)
            {
                maxPriority = lastSeenPlayerLoc.GetComponent<PotentialTarget>().priority;
                ID = -2;
            }
            if (mostRecentFootstepHeard != null)
            {
                if (mostRecentFootstepHeard.GetComponent<PotentialTarget>().priority > maxPriority)
                {
                    maxPriority = mostRecentFootstepHeard.GetComponent<PotentialTarget>().priority;
                    ID = -1;
                }
            }
            for (int i = 0; i < targetCandidates.Count; i++)
            {
                if (targetCandidates[i] != null)
                {
                    if (targetCandidates[i].GetComponent<PotentialTarget>().priority > maxPriority)
                    {
                        maxPriority = targetCandidates[i].GetComponent<PotentialTarget>().priority;
                        ID = i;
                    }
                }
            }
                if (maxPriority < 25f) // If Burt has nothing better to do,
                {
                    // Then circle above the map randomly
                    Instantiate(randomWander, new Vector3(
                        Random.Range(minAllowedWanderArea.x, maxAllowedWanderArea.x),
                        Random.Range(minAllowedWanderArea.y, maxAllowedWanderArea.y),
                        Random.Range(minAllowedWanderArea.z, maxAllowedWanderArea.z)),
                        Quaternion.identity);
                    PlayerManager.burt.GetComponent<BurtController>().targetOrbitFrustration = 0f;
                }
                else
                {
                    if (ID == -2)
                    {
                        transform.position = lastSeenPlayerLoc.transform.position;
                    }
                    else if (ID == -1)
                    {
                        transform.position = mostRecentFootstepHeard.transform.position;
                    }
                    else
                    {
                        transform.position = targetCandidates[ID].transform.position;
                    }
                }
            }
            else
            {
                //The player has won
                transform.position += new Vector3(0f, Time.deltaTime, 0f);
            }
        }
    }
}