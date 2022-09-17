using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRot : MonoBehaviour
{
    public float randy;

    //Get PlayerSanity Script

    PlayerSanity playerSanityScript;

    // Start is called before the first frame update
    void Start()
    {
       playerSanityScript = GameObject.Find("PlayerCapsule").GetComponent<PlayerSanity>();

        randy = Random.Range(0, 100);
        transform.Rotate(new Vector3(0, randy, 0));
    }

    // Update is called once per frame
    void Update()
    {

        if (playerSanityScript.bushrotatescene == true)
        {
            transform.Rotate(new Vector3(0, 1, 0));
        }



    }



    void GetRandomPos()
    {
       
       
    }


}
