using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace StarterAssets
{
    public class Squaker : MonoBehaviour
    {
        public AudioSource call1;
        public AudioSource call2;
        public AudioSource paranoia1;
        public AudioSource paranoia2;

        public float callCooldown = 5f;
        public float paranoiaCooldown = 0.45f;

        // Update is called once per frame
        void Update()
        {
            callCooldown -= Time.deltaTime;
            paranoiaCooldown -= Time.deltaTime * (1f - PlayerSanity.sanityLevel * 0.01f);
            if(callCooldown < 0f)
            {
                if (Random.Range(0, 19) == 0) // 5% chance to play a sound every 5 seconds
                {
                    float pitch = Random.Range(0.67f, 1f); // Sets the pitch to a random value from 67% to 100%
                    if (Random.value > 0.5f * PlayerSanity.sanityLevel * 0.005f)
                        pitch *= -1f;
                    call1.pitch = pitch;
                    call2.pitch = pitch;
                    if (Random.Range(0, 1) == 0)
                        call1.Play();
                    else
                        call2.Play();
                }
                callCooldown += 1f;
            }
            if (paranoiaCooldown < 0f)
            {
                if (Random.Range(0, 1) == 0) // 50% chance to play a sound every 45+ seconds
                {
                    float pitch = Random.Range(0.67f, 1f); // Sets the pitch to a random value from 67% to 100%
                    if (Random.Range(0, 3) == 1)
                        pitch *= -1f;
                    paranoia1.pitch = pitch;
                    paranoia2.pitch = pitch;
                    float angle = Random.Range(0f, 2f * Mathf.PI);
                    paranoia1.transform.position = PlayerManager.player.transform.position + new Vector3(10f * Mathf.Cos(angle), 5f, 10f * Mathf.Sin(angle));
                    paranoia2.transform.position = PlayerManager.player.transform.position + new Vector3(10f * Mathf.Cos(angle), 5f, 10f * Mathf.Sin(angle));
                    if (Random.Range(0, 1) == 0)
                        paranoia1.Play();
                    else
                        paranoia2.Play();
                }
                paranoiaCooldown += 45f;
            }
        }
    }
}
