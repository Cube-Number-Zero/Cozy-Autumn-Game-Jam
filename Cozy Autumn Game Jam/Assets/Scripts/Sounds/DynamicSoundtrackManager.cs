using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

namespace StarterAssets
{
    public class DynamicSoundtrackManager : MonoBehaviour
    {
        // Just get a bunch of objects here so I can reference them
        private GameObject player;
        private GameObject burt;
        private BurtController burtController;
        private float sanity = 1f;
        private GameObject ambience;

        public GameObject burtDrums;

        void Start()
        {
            player = GameObject.Find("PlayerCapsule");
            burt = GameObject.Find("Burt");
            burtDrums = GameObject.Find("Burt Drums");
            ambience = GameObject.Find("Ambience");
            burtController = burt.GetComponent<BurtController>();
        }

        void Update()
        {
            sanity = PlayerSanity.sanityLevel * 0.01f;

            ambience.GetComponent<AudioSource>().volume = 0.25f * (1f - sanity); // The background ambience is louder the lower sanity you have



            burtDrums.transform.position = player.transform.position + new Vector3(0f, insanityRandomize(Vector3.Distance(player.transform.position, burt.transform.position), 1f, 1f), 0f);
            // The war drums are placed at a point the same distance from the player as burt, but directly above the player's head
            //This was done because if I had attached the audio source to Burt, it would become directional and I didn't want that.



            burtDrums.GetComponent<AudioLowPassFilter>().cutoffFrequency = applyLowPassToDrums(insanityRandomize(burtController.seenPlayerCertainty, 2f)); // Apply a low pass to the drums when Burt doesn't know where the player is

        }





        float insanityRandomize(float intake, float offset)
        {
            // This takes an input float 0-1 and outputs a float 0-1 that is more random the lower sanity you have
            return (intake * sanity) + (Mathf.PerlinNoise(Time.time * 0.1f, offset * 10f) * (1f - sanity));
        }

        float insanityRandomize(float intake, float offset, float multiplier)
        {
            // This takes an input float 0-1 and outputs a float 0-1 that is more random the lower sanity you have
            return (intake * sanity) + (Mathf.PerlinNoise(Time.time * 0.1f, offset * 10f) * (1f - sanity) * multiplier);
        }

        float applyLowPassToDrums(float intake)
        {
            // Math for converting the certainty into a frequency in hertz
            return Mathf.Pow(2, Mathf.Lerp(Mathf.Log(80f, 2f), Mathf.Log(22000f, 2f), intake));

        }
    }
}