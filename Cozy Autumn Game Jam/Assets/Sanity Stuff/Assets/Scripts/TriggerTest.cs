using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class TriggerTest : MonoBehaviour
    {

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log("Player has entered Trigger");
            PlayerManager.inCabin = true;
        }

        public void OnTriggerExit(Collider other)
        {
            Debug.Log("Player has exited Trigger");
            PlayerManager.inCabin = false;
        }

    }
}