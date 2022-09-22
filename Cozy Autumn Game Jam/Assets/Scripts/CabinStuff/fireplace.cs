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
        float flamedecreasetime = .3f;

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
            if(time > flamedecreasetime){
                flamelevel -= .05f;
                if(flamelevel < 0){
                    flamelevel = 0;
                }
                time = 0f;
            }
            playerDistance = Vector3.Distance(transform.position, player.transform.position);
            if(playerDistance < 5f){
                Debug.Log("you're warm");
                player.GetComponent<PlayerSanity>().additionPossible = true;
                player.GetComponent<PlayerSanity>().subtractionPossible = false;
            }
            else{
                player.GetComponent<PlayerSanity>().additionPossible = false;
                player.GetComponent<PlayerSanity>().subtractionPossible = true;
            }
        }
    }
}
