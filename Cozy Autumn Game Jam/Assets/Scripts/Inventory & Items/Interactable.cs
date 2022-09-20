using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Item item;

    [Range(0f, 10f)]
    public float usableRadius = 3f; // How far the player can be and still pick up the item

    private float maxTime;

    [Range(0f, 1f)]
    public float visibleTimer = 0.5f;
    [HideInInspector]
    public float timer;

    GameObject player;
    
    void Start()
    {
        player = GameObject.Find("PlayerCapsule");
        maxTime = item.maxTimer;
        timer = maxTime;
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            visibleTimer = timer / maxTime;
            if (timer <= 0f)
            {
                item = item.turnInto;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, usableRadius);
    }
}
