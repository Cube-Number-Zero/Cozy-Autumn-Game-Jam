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
	
	public GameObject player; // Allows other scripts to find the player with PlayerManager.instance.player
	public GameObject burt; // Allows other scripts to find Burt with PlayerManager.instance.burt
	[Range(0f, 100f)]
	public float sanity = 100f; // This is the player's sanity; I put it here because so many systems will use it
	
}
