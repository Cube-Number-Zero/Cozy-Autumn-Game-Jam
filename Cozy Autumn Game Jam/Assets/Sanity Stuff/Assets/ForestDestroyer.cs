using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestDestroyer : MonoBehaviour
{


    //Get Trigger Script
    TriggerTest triggerTestScript;


    //Create Cooldown
    public float coolDown = 30;

    public bool numGenerated = false;
    public float randNum;


    public bool changeForest = false;


    //Timer
    public float changePerSec = 0;

    public float testNumber = 0;


    
    void Start()
    {
        triggerTestScript = GameObject.Find("trigger").GetComponent<TriggerTest>();
    }

    
    void Update()
    {
        //Checks to see if player is in trigger, then Generates random number once
        if (triggerTestScript.isInTrigger == true && numGenerated == false && coolDown <= 0)
        {
            RandomChance();
            numGenerated = true;
            coolDown += 30;
        }


        if(triggerTestScript.isInTrigger == false && numGenerated == true)
        {
            numGenerated = false;
        }


        if (randNum == 7)
        {
            changeForest = true;
            Destroy(this.gameObject);
            
     
        }

        if (coolDown >= 0)
        {
            TreeClock();
        }

    }




    void RandomChance()
    {
        randNum = Random.Range(0, 10);
    }


    void TreeClock()
    {
        coolDown -= changePerSec * Time.deltaTime;
    }

}
