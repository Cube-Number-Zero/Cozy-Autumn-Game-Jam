using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

using Debug = UnityEngine.Debug;

namespace StarterAssets
{
    public class PotentialTarget : MonoBehaviour
    {

        public bool persist; // True: Burt will investigate this target for a while // False: Burt will move on after he sees the target
        public bool soundDecay; // True: The sound gets quieter over time // False: The sound stays at the original volume
        public bool common; // True: The sound will overwrite previous sounds of the same type // False: Multiple instances of this type of object can exist simultaneously
        public float volume; // How far away burt can hear this from
        public float volumeDecayRate; // If soundDecay is on, this is how quickly the volume decreases
                                      // If soundDecay isn't, this is the duration of the sound
        public float distanceToBurt; // How far Burt is
        public float priority = 0f; // How interested Burt is in this target

        public enum targetType { footstep, seenPlayer, thrown };
        public targetType sourceType; // Only used if "common" is enabled

        private Vector3 burtLoc;

        // Start is called before the first frame update
        void Start()
        {
            burtLoc = PlayerManager.burt.transform.position;
            distanceToBurt = Vector3.Distance(transform.position, burtLoc);
            if (sourceType == targetType.footstep)
            {
                if (distanceToBurt > volume && !persist)
                    Destroy(this.gameObject);
                else
                {
                    Destroy(PlayerManager.target.GetComponent<TargetController>().mostRecentFootstepHeard);
                    PlayerManager.target.GetComponent<TargetController>().mostRecentFootstepHeard = this.gameObject;
                }
            }
            else if (sourceType == targetType.seenPlayer)
            {
                if(PlayerManager.target.GetComponent<TargetController>().lastSeenPlayerLoc != null)
                    priority = Mathf.Min(100f, PlayerManager.target.GetComponent<TargetController>().lastSeenPlayerLoc.GetComponent<PotentialTarget>().priority + 8f * Time.deltaTime);
                Destroy(PlayerManager.target.GetComponent<TargetController>().lastSeenPlayerLoc);
                PlayerManager.target.GetComponent<TargetController>().lastSeenPlayerLoc = this.gameObject;
            }
            else if (sourceType == targetType.thrown)
            {
                TargetController.targetCandidates.Add(this.gameObject);
            }

        }

        // Update is called once per frame
        void Update()
        {
            
            burtLoc = PlayerManager.burt.transform.position;
            if (sourceType == targetType.seenPlayer)
            {
                priority = Mathf.Max(0f, priority - 4f * Time.deltaTime);
            }
        }
    }
}