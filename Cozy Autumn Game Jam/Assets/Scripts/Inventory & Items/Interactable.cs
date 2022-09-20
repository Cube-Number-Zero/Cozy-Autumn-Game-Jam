using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Item item;

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
                if (item.turnInto != null)
                    item = item.turnInto;
                else
                    Destroy(this.gameObject);
            }
        }
    }
}
