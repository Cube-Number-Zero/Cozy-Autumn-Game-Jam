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
        if (posGrabbed ==false && bushesCreated < 35)
        {
            GetRandomPos();
            Instantiate(Bush,new Vector3(-randx, 0f, -randz), Quaternion.identity);
        }
        

        

    }

    void Update()
    {
        if (posGrabbed == false && bushesCreated < 25)
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
        randx = Random.Range(39f, -40f);
        randz = Random.Range(-40f, 39f);
        bushesCreated += 1;
    }



}
