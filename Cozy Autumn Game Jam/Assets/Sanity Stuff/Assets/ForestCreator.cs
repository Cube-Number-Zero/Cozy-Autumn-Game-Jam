using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StarterAssets
{
    public class ForestCreator : MonoBehaviour
    {
        public GameObject Forest;
        public GameObject forestPrefab;
        public GameObject forestPrefab2;
        [Range(0, 10)]
        public int randNum;




        //Grab ForestDestroyer Script
        public ForestDestroyer forestDestroyerScript;



        void Start()
        {
            forestPrefab = Instantiate(Forest, new Vector3(0f, 0f, 0f), Quaternion.identity);
            forestDestroyerScript = forestPrefab.GetComponent<ForestDestroyer>();
        }


        void Update()
        {
            if (forestDestroyerScript.changeForest == true && randNum == 3)
            {
                Instantiate(forestPrefab2, new Vector3(0f, 0f, 0f), Quaternion.identity);
                forestDestroyerScript.changeForest = false;
            }




        }




        void RandomChance()
        {
            randNum = Random.Range(0, 10);
        }




    }
}