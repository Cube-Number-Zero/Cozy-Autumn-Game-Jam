using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace StarterAssets
{
    public class InteractionController : MonoBehaviour
    {

        public static bool hasBag = false;

        private static Item emptyItem;

        public static Item leftHandItem;
        public static Item rightHandItem;
        public static Item bagItem;

        private StarterAssetsInputs inputs;

        public static bool helpMenuOpen = false;
        public static bool inventoryOpen = false;
        public static bool isLookingAtItem = false;

        public GameObject playerVisionTarget;

        public enum side {Left, Right};

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
            testIfLookingAtObject();
            if (inventoryOpen)
            {
                //The player does have the inventory open, so the controls do different things
                if (inputs.interactR)
                {
                    inputs.interactR = false;
                    if (hasBag)
                    {
                        Item tempItem = rightHandItem;
                        rightHandItem = bagItem;
                        bagItem = tempItem;
                    }
                    else
                        Debug.Log("You just tried to switch right hand item, but you don't have a bag");
                }
                if (inputs.interactL)
                {
                    inputs.interactL = false;
                    if (hasBag)
                    {
                        Item tempItem = leftHandItem;
                        leftHandItem = bagItem;
                        bagItem = tempItem;
                    }
                    else
                        Debug.Log("You just tried to switch left hand item, but you don't have a bag");
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
            else
            {
                // The player does not have the inventory open, and can interact and use normally
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
        }

        private void testIfLookingAtObject()
        {
            Transform cam = GameObject.Find("PlayerCameraRoot").transform;
            Vector3 cameraDirection = cam.rotation * new Vector3(0f, 0f, 1f); // Where is the player looking?
            RaycastHit hit;
            if (Physics.Raycast(cam.position, cameraDirection, out hit))
            {
                playerVisionTarget = hit.collider.gameObject;
                if (playerVisionTarget.GetComponent<Interactable>() == null)
                    isLookingAtItem = false;
                else
                    isLookingAtItem = true;
            }
            else
                isLookingAtItem = false;
        }
        
        private void useItem(Item item)
        {
            switch (item.type)
            {
                case "STONE":
                    // Throw the rock
                    rightHandItem = emptyItem;
                    //Debug.Log("hi1");
                    break;
                case "LOUDTOY":
                    // Deploy distraction toy
                    item = emptyItem;
                    break;
            }
        }

        private void testInteraction(side hand)
        {
            Interactable interactable;
            if(isLookingAtItem && playerVisionTarget != null)
            {
                interactable = playerVisionTarget.GetComponent<Interactable>();
                if (interactable != null)
                {
                    // You are looking at an item

                    if (Vector3.Distance(playerVisionTarget.transform.position, transform.position) > interactable.usableRadius)
                    {
                        //You are looking at an item, but it is too far away to use
                        //Debug.Log("Item is too far");
                    }
                    else
                    {
                        //You are looking at an item and are close enough to pick it up
                        if (interactable.item.type == "BAG")
                        {
                            // If it's a bag, wear it instead of picking it up
                            hasBag = true;
                            //Destroy(playerVisionTarget);
                        }
                        else
                        {
                            if (hand == side.Right && rightHandItem.type == "NONE")
                            {
                                rightHandItem = interactable.item;
                                Destroy(playerVisionTarget);
                                if (rightHandItem.type == "ACTIVESTONE")
                                    rightHandItem.type = "STONE";
                            }
                            if (hand == side.Left && leftHandItem.type == "NONE")
                            {
                                leftHandItem = interactable.item;
                                Destroy(playerVisionTarget);
                                if (leftHandItem.type == "ACTIVESTONE")
                                    leftHandItem.type = "STONE";
                            }
                        }
                    }
                }
            }
        }
    }
}