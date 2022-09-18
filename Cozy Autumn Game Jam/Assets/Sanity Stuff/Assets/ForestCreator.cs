using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestCreator : MonoBehaviour
{
    public GameObject Forest;
    public GameObject forestPrefab;
    public GameObject forestPrefab2;
    public float randNum;




    //Grab ForestDestroyer Script
    public ForestDestroyer forestDestroyerScript;



    void Start()
    { 
        forestPrefab = Instantiate(Forest, new Vector3(0, 0, 0), Quaternion.identity);
        forestDestroyerScript = forestPrefab.GetComponent<ForestDestroyer>();
    }

   
    void Update()
    {
        if (forestDestroyerScript.changeForest == true && randNum == 3)
        {
            Instantiate(forestPrefab2, new Vector3(0, 0, 0), Quaternion.identity);
            forestDestroyerScript.changeForest = false;
        }




    }




    void RandomChance()
    {
        randNum = Random.Range(0, 10);
    }




}
