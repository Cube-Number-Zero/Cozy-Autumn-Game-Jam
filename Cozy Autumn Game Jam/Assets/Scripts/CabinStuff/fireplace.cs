using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StarterAssets
{
    public class fireplace : MonoBehaviour
    {

        Light myfire;
        public GameObject player;
        Transform me;
        public float flamelevel;
        float playerDistance;
        float time = 0f;
        float flamedecreasetime = 0.3f;

        // Start is called before the first frame update
        void Start()
        {
            flamelevel = 20f;
            me = gameObject.transform;
            myfire = me.GetComponentInChildren<Light>();
        }

        // Update is called once per frame
        void Update()
        {
            myfire.intensity = flamelevel;
            time += Time.deltaTime;
            if(time > flamedecreasetime)
            {
                flamelevel -= .01f;
                if (flamelevel < 0f)
                {
                    flamelevel = 0f;
                }
                else
                {
                    time -= flamedecreasetime;
                }
                time = 0f;
            }
            /*
            playerDistance = Vector3.Distance(transform.position, player.transform.position);
            if(playerDistance < 5f){
                player.GetComponent<PlayerSanity>().additionPossible = true;
                player.GetComponent<PlayerSanity>().subtractionPossible = false;
            }
            else{
                player.GetComponent<PlayerSanity>().additionPossible = false;
                player.GetComponent<PlayerSanity>().subtractionPossible = true;
            }
            */
        }
    }
}
