using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
	#region Singleton
	
	public static PlayerManager instance;
	
	void Awake ()
	{
		instance = this;
	}
	
	#endregion
	
	public static GameObject player; // Allows other scripts to find the player with PlayerManager.instance.player
	public static GameObject burt; // Allows other scripts to find Burt with PlayerManager.instance.burt
	public static bool inCabin = true;

	void Start()
	{
		player = GameObject.Find("PlayerCapsule");
        player = GameObject.Find("Burt");
    }
}
