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
        public GameObject lastSeenPlayerLoc;
        public GameObject mostRecentFootstepHeard;
        public static List<GameObject> targetCandidates = new List<GameObject>();

        void Start()
        {

        }

        void Update()
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
            if (maxPriority == 0f) // If Burt has nothing better to do,
            {
                // Then circle above the map randomly
                transform.position = new Vector3(Random.Range(-50f, 50f), Random.Range(20f, 30f), Random.Range(-50f, 50f));
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
    }
}