using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class buildabearworkshop : MonoBehaviour
    {
        public int piecesAttached = 0;
        public bool piece1Attached = false;
        public bool piece2Attached = false;

        // Update is called once per frame
        void Update()
        {
            if(piece1Attached && piece2Attached)
            {
                PlayerManager.hasPlayerWon = true;
            }
        }
    }
}
