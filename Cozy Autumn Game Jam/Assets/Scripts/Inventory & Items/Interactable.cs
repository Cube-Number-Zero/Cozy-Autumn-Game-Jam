using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Item item;

    [Range(0f, 10f)]
    public float usableRadius = 3f; // How far the player can be and still pick up the item

    GameObject player;

    void Start()
    {
        player = GameObject.Find("PlayerCapsule");


        //Debug.Log(player.GetComponent<InteractionController>().hasBag);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, usableRadius);
    }
}
