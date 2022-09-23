using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageRandomizer : MonoBehaviour
{


    float randx;
    float randz;
    bool posGrabbed = false;
    int bushesCreated = 0;
    public bool simChange = false;

    public GameObject Bush;
    
    void Start()
    {
        for (int i = 0; i < 70; i++)
        {
            if (posGrabbed == false)
            {
                GetRandomPos();
                Instantiate(Bush, new Vector3(randx, 0f, randz), Quaternion.identity);
                bushesCreated++;
            }
        }
        

        

    }

    void Update()
    {
        if (posGrabbed == false && bushesCreated < 70)
        {
            GetRandomPos();
            Instantiate(Bush, new Vector3(randx, 0f, randz), Quaternion.identity);
        }


        if (simChange)
        {
            GetRandomPos();
           
        }


    }



    void GetRandomPos()
    {
        randx = Random.Range(-160f, 180f);
        randz = Random.Range(30f, 300f);
        bushesCreated += 1;
    }



}
