using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    public bool isInTrigger = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    




    }

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
