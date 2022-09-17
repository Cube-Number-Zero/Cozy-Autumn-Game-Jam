using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSanity : MonoBehaviour
{
    //Get TriggerTest Script
    public TriggerTest playerTriggerTest;
    public GameObject otherGameObject;

   




    public SanityBar sanityBar;


    public float sanityLevel = 100;
    public float changePerSecond;
    public bool subtractionPossible;
    public bool additionPossible = true;
   
    //Sanity Events
    public float randNum;
   
    //Checks to see if threshold between sanities has been met
    public bool doSanityEvent = false;

    //Basic Sanity Event
    public bool bushrotatescene = false;
    
   
    //Used to calculate how long inbetween events (Broken in code, use inspector to change value)
    public float eventWaitTimer = 5;
   
    //Used specifically for how long an event lasts
    public float eventTimer;
    public bool eventTimeGenerated = false;

    
    void Start()
    {
        playerTriggerTest = otherGameObject.GetComponent<TriggerTest>();
    }




    //Sanity Events generator
    public void RandomGenerate()
    {
        randNum = Random.Range(1, 10);
        Debug.Log(randNum);

        if (randNum == 2)
        {
            bushrotatescene = true;
            
        }
    }





    void Update()
    {
        //Procecss Sanity Events

     if (sanityLevel > 70 && sanityLevel < 90)
        {
            doSanityEvent = true;
            eventWaitTimer -= changePerSecond * Time.deltaTime;
        }


        if (doSanityEvent == true && eventWaitTimer <= 0)
        {
            RandomGenerate();
            eventWaitTimer += 5;
            doSanityEvent = false;
            
        }

        if (bushrotatescene == true)
        {
            if (eventTimer > 0)
            {
                eventTimer -= changePerSecond * Time.deltaTime;
            }

            if (eventTimer <= 0 && eventTimeGenerated == false)
            {
                randomEventTime();
                eventTimeGenerated = true;
            }

            
            if (eventTimer <= 0)
            {
                bushrotatescene = false;
                eventTimeGenerated = false;
            }
            
        }


      
        


        // Checks sanity level and determines if it is possible to either add or subtract sanity
        if (sanityLevel < 0)
        {
            subtractionPossible = false;
        }   else
        {
            subtractionPossible = true;
        }


        if (sanityLevel > 100)
        {
            additionPossible = false;
        } else
        {
            additionPossible = true;
        }


        //Checks if player is in or out of trigger and does the appropriate action to sanity

        if (playerTriggerTest.isInTrigger == true && additionPossible == true)
        {
            sanityLevel += changePerSecond * Time.deltaTime;
            
        }




        if (playerTriggerTest.isInTrigger == false && subtractionPossible == true)
        {
            sanityLevel -= changePerSecond * Time.deltaTime;
            
        }


        //Sanity UI

        sanityBar.SetSanity(sanityLevel);



        void randomEventTime()
        {
            eventTimer = Random.Range(1, 10);
        }

    }

    


}
