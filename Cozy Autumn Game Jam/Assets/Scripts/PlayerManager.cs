using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    
	#region Singleton // no clue what this does
	
	public static PlayerManager instance;
	
	void Awake ()
	{
		instance = this;
	}
	
	#endregion
	
	public GameObject player; // Allows other scripts to find the player with PlayerManager.instance.player
	public GameObject burt; // Allows other scripts to find Burt with PlayerManager.instance.burt
	public float sanity = 1f; // This is the player's sanity; I put it here because so many systems will use it
	
}
