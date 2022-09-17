using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageRandomizer : MonoBehaviour
{


    float randx;
    float randz;
    bool posGrabbed = false;
    float bushesCreated = 0;
    public float simChange = 0;

    public GameObject Bush;
    
    void Start()
    {
        if (posGrabbed ==false && bushesCreated < 35)
        {
            GetRandomPos();
            Instantiate(Bush,new Vector3(-randx, 0, -randz), Quaternion.identity);
        }
        

        

    }

    void Update()
    {
        if (posGrabbed == false && bushesCreated < 25)
        {
            GetRandomPos();
            Instantiate(Bush, new Vector3(randx, 0, randz), Quaternion.identity);
        }


        if (simChange == 1)
        {
            GetRandomPos();
           
        }


    }



    void GetRandomPos()
    {
        randx = Random.Range(39, -40);
        randz = Random.Range(-40, 39);
        bushesCreated += 1;
    }



}
