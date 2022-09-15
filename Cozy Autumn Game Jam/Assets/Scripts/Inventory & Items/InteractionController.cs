using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StarterAssets
{
    public class InteractionController : MonoBehaviour
    {

        public bool hasBag = false;

        private static Item emptyItem;

        public Item leftHandItem;
        public Item rightHandItem;
        public Item bagItem;

        private StarterAssetsInputs inputs;

        enum side {Left, Right};

        void Start()
        {
            emptyItem = ScriptableObject.CreateInstance<Item>(); // Establishing a permanent emptyItem to use when a spot in the player's inventory is freed up
            AssetDatabase.CreateAsset(emptyItem, "Assets/Items/blankItem.asset");

            emptyItem.type = "NONE";

            leftHandItem = emptyItem;
            rightHandItem = emptyItem;
            bagItem = emptyItem;

            inputs = GetComponent<StarterAssetsInputs>();
        }

        void Update()
        {
            // See if the player is pressing any of the relavent buttons
            if (inputs.interactR)
            {
                inputs.interactR = false;
                testInteraction(side.Right);
            }
            if (inputs.interactL)
            {
                inputs.interactL = false;
                testInteraction(side.Left);
            }
            if (inputs.useL)
            {
                inputs.useL = false;
                useItem(leftHandItem);
            }
            if (inputs.useR)
            {
                inputs.useR = false;
                useItem(rightHandItem);
            }
        }

        private void useItem(Item item)
        {
            switch (item.type)
            {
                case "STONE":
                    // Throw the rock
                    rightHandItem = emptyItem;
                    Debug.Log("hi1");
                    break;
                case "LOUDTOY":
                    // Deploy distraction toy
                    item = emptyItem;
                    break;
            }
        }

        private void testInteraction(side hand)
        {
            Transform cam = GameObject.Find("PlayerCameraRoot").transform;
            Vector3 cameraDirection = cam.rotation * new Vector3(0f, 0f, 1f); // Where is the player looking?
            RaycastHit hit;

            if (Physics.Raycast(cam.position, cameraDirection, out hit))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    // You are looking at an item

                    if (Vector3.Distance(hit.collider.transform.position, transform.position) > interactable.usableRadius)
                    {
                        //You are looking at an item, but it is too far away to use
                        Debug.Log("Item is too far");
                    }
                    else
                    {
                        //You are looking at an item and are close enough to pick it up
                        if (hand == side.Right && rightHandItem.type == "NONE")
                        {
                            rightHandItem = interactable.item;
                            Destroy(hit.collider.gameObject);
                            if (rightHandItem.type == "ACTIVESTONE")
                                rightHandItem.type = "STONE";
                        }
                        if (hand == side.Left && leftHandItem.type == "NONE")
                        {
                            leftHandItem = interactable.item;
                            Destroy(hit.collider.gameObject);
                            if (leftHandItem.type == "ACTIVESTONE")
                                leftHandItem.type = "STONE";
                        }
                    }
                }
                else
                {
                    // You aren't looking at an item
                    Debug.Log("No interactable item detected");
                }
            }
        }
    }
}