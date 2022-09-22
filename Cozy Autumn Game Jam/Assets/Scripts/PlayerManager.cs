using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
	public class PlayerManager : MonoBehaviour
	{

        public static GameObject player; // Allows other scripts to find the player with PlayerManager.instance.player
        public static GameObject burt; // Allows other scripts to find Burt with PlayerManager.instance.burt
        public static GameObject target;
        public static bool hasPlayerWon = false;
        public static bool inCabin = false;

        public static PlayerManager instance;

        void Awake()
        {
            instance = this;
            player = GameObject.Find("PlayerCapsule");
            burt = GameObject.Find("Burt");
            target = GameObject.Find("BurtFlightTarget");
        }

        void Start()
        {
            if(burt == null)
            {
                burt = GameObject.Find("Burt");
            }
        }
	}
}