using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRotate : MonoBehaviour
{
    float randY;

    // Start is called before the first frame update
    void Start()
    {
        randY = Random.Range(0f, 100f);
        transform.Rotate(new Vector3(0f, randY, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
