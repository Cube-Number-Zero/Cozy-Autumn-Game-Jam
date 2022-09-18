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

    [Range(0f, 100f)]
    public static float sanityLevel = 100f;
    public float changePerSecond;
    public bool subtractionPossible = true;
    public bool additionPossible = false;

    //Sanity Events
    [Range(1, 10)]
    public int randNum;
   
    //Checks to see if threshold between sanities has been met
    public bool doSanityEvent = false;

    //Basic Sanity Event
    public bool bushrotatescene = false;
    
   
    //Used to calculate how long inbetween events (Broken in code, use inspector to change value)
    public float eventWaitTimer = 5f;
   
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


        if (doSanityEvent && eventWaitTimer <= 0f)
        {
            RandomGenerate();
            eventWaitTimer = 5f;
            doSanityEvent = false;
            
        }

        if (bushrotatescene)
        {
            if (eventTimer > 0f)
            {
                eventTimer -= changePerSecond * Time.deltaTime;
            }
            else
            {
                if(!eventTimeGenerated)
                {
                    randomEventTime();
                    eventTimeGenerated = true;
                }
                bushrotatescene = false;
                eventTimeGenerated = false;
            }
        }


      
        


        // Checks sanity level and determines if it is possible to either add or subtract sanity
        subtractionPossible = (sanityLevel > 0f);
        additionPossible = (sanityLevel < 100f);


        //Checks if player is in or out of trigger and does the appropriate action to sanity

        if (playerTriggerTest.isInTrigger && additionPossible)
        {
            sanityLevel += changePerSecond * Time.deltaTime;
            
        }

        if (!playerTriggerTest.isInTrigger && subtractionPossible)
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
