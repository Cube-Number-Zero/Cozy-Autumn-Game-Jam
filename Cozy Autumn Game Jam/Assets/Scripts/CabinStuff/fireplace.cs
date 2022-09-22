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
        public float flamelevel = 100f;
        float fakeflamelvl;
        float playerDistance;
        float time = 0f;
        float timeB = 0f;
        float flamedecreasetime = 0.3f;
        float flameflickertime = .1f;

        public float sanity;

        // Start is called before the first frame update
        void Start()
        {
            me = gameObject.transform;
            myfire = me.GetComponentInChildren<Light>();
        }

        // Update is called once per frame
        void Update()
        {
            sanity = PlayerSanity.sanityLevel;
            time += Time.deltaTime;
            timeB += Time.deltaTime;
            if(time > flamedecreasetime)
            {
                flamelevel -= .5f;
                if (flamelevel < 0f)
                {
                    flamelevel = 0f;
                }
                else
                {
                    time -= flamedecreasetime;
                }
            }
            if(timeB > flameflickertime){
                fakeflamelvl = flamelevel*.2f;
                myfire.intensity = Random.Range(flamelevel-fakeflamelvl,flamelevel+fakeflamelvl);
                timeB = 0f;
            }

        }
    }
}
