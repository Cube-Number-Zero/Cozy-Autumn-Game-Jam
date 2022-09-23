using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StarterAssets
{
    public class RandomRot : MonoBehaviour
    {
        public float randy;

        //Get PlayerSanity Script

        PlayerSanity playerSanityScript;

        // Start is called before the first frame update
        void Start()
        {
            playerSanityScript = GameObject.Find("PlayerCapsule").GetComponent<PlayerSanity>();

            randy = Random.Range(0f, 100f);
            transform.Rotate(new Vector3(0f, randy, 0f));
        }

        // Update is called once per frame
        void Update()
        {
            if (playerSanityScript != null)
            {
                if (playerSanityScript.bushrotatescene)
                {
                    transform.Rotate(new Vector3(0f, 1f, 0f));
                }
            }



        }



        void GetRandomPos()
        {


        }


    }
}