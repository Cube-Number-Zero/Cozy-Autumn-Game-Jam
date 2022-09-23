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
        if (posGrabbed ==false && bushesCreated < 65)
        {
            GetRandomPos();
            Instantiate(Bush,new Vector3(-randx, 0f, -randz), Quaternion.identity);
        }
        

        

    }

    void Update()
    {
        if (posGrabbed == false && bushesCreated < 65)
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
        randx = Random.Range(-217f, 222f);
        randz = Random.Range(370f, 16f);
        bushesCreated += 1;
    }



}
