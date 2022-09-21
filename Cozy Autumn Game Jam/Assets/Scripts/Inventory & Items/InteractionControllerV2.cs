using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionControllerV2 : MonoBehaviour
{
    public GameObject lefthand;
    public GameObject righthand;
    public Transform self;

    bool hasBag = true;
    bool interactL;

    public void OnInteractL(InputValue value)
	{
        Debug.Log("gaming");
        Debug.Log(interactL);
	}


    public void InteractLInput(bool newInteractLState)
    {
        interactL = newInteractLState;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
