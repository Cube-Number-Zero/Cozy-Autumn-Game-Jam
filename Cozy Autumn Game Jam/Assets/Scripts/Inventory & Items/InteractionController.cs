using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using System.Diagnostics;

using Debug = UnityEngine.Debug;

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

        public Item inactiveRock;

        public static bool helpMenuOpen = false;
        public static bool inventoryOpen = false;
        public static bool isLookingAtItem = false;

        public static GameObject playerVisionTarget;

        public float useDistance = 3f;

        public enum side {Left, Right};

        [Header("Prefabs")]
        public GameObject prefab_activeDistractionToy;
        public GameObject prefab_activeStone;
        public GameObject prefab_distractionToy;
        public GameObject prefab_firewood;
        public GameObject prefab_stone;
        public GameObject prefab_toyPiece1;
        public GameObject prefab_toyPiece2;




        void Start()
        {
            emptyItem = ScriptableObject.CreateInstance<Item>(); // Establishing a permanent emptyItem to use when a spot in the player's inventory is freed up
            
            AssetDatabase.CreateAsset(emptyItem, "Assets/Scripts/Inventory & Items/Items/blankItem.asset");

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
                    dropItem(leftHandItem.type);
                    leftHandItem = emptyItem;
                }
                if (inputs.useR)
                {
                    inputs.useR = false;
                    dropItem(rightHandItem.type);
                    rightHandItem = emptyItem;
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
                    leftHandItem = (leftHandItem.type == "STONE" || leftHandItem.type == "LOUDTOY") ? emptyItem : leftHandItem;
                }
                if (inputs.useR)
                {
                    inputs.useR = false;
                    useItem(rightHandItem);
                    rightHandItem = (rightHandItem.type == "STONE" || rightHandItem.type == "LOUDTOY") ? emptyItem : rightHandItem;
                }
            }
        }

        private void testIfLookingAtObject()
        {
            Transform cam = GameObject.Find("PlayerCameraRoot").transform;
            Vector3 cameraDirection = cam.rotation * new Vector3(0f, 0f, 1f); // Where is the player looking?
            RaycastHit hit;
            if (Physics.Raycast(cam.position, cameraDirection, out hit, useDistance))
            {
                playerVisionTarget = hit.collider.gameObject;
                if (playerVisionTarget.GetComponent<Interactable>() == null)
                    isLookingAtItem = false;
                else
                {
                    isLookingAtItem = true;
                }
            }
            else
                isLookingAtItem = false;
        }
        
        private void dropItem(string type)
        {
            switch (type)
            {
                case "STONE":
                    Instantiate(prefab_stone, transform.position, Quaternion.identity);
                    break;
                case "TOY1":
                    Instantiate(prefab_toyPiece1, transform.position, Quaternion.identity);
                    break;
                case "TOY2":
                    Instantiate(prefab_toyPiece2, transform.position, Quaternion.identity);
                    break;
                case "LOUDTOY":
                    Instantiate(prefab_distractionToy, transform.position, Quaternion.identity);
                    break;
                case "WOOD":
                    Instantiate(prefab_firewood, transform.position, Quaternion.identity);
                    break;
            }
        }

        private void useItem(Item item)
        {
            Vector3 lookDirection = GameObject.Find("PlayerCameraRoot").transform.rotation * new Vector3(0f, 0f, 1f);
            switch (item.type)
            {
                case "STONE":
                    // Throw the rock
                    GameObject thrownStone = Instantiate(prefab_activeStone, transform.position + new Vector3(lookDirection.x, 0f, lookDirection.z).normalized + new Vector3(0f, 1f, 0f), Quaternion.identity);
                    thrownStone.GetComponent<Rigidbody>().velocity = new Vector3(lookDirection.x * 32f, lookDirection.y * 16f + 8f, lookDirection.z * 32f);
                    break;
                case "LOUDTOY":
                    // Deploy distraction toy
                    Instantiate(prefab_activeDistractionToy, transform.position + new Vector3(lookDirection.x, 0f, lookDirection.z).normalized + new Vector3(0f, 1f, 0f), Quaternion.identity);
                    break;
            }
        }

        private void testInteraction(side hand)
        {
            Interactable interactable;
            if(isLookingAtItem && playerVisionTarget != null)
            {
                interactable = playerVisionTarget.GetComponent<Interactable>();
                if (interactable != null && playerVisionTarget.GetComponent<Interactable>().item.type != "ACTIVELOUDTOY")
                {
                    // You are looking at an item and it's not an active distraction toy

                    if (Vector3.Distance(playerVisionTarget.transform.position, transform.position) > useDistance)
                    {
                        //You are looking at an item, but it is too far away to use
                        //Debug.Log("Item is too far");
                    }
                    else
                    {
                        //You are looking at an item and are close enough to pick it up
                        if (interactable.item.type == "BAG" && !hasBag)
                        {
                            // If it's a bag, wear it instead of picking it up
                            hasBag = true;
                            Destroy(playerVisionTarget);
                        }
                        else
                        {
                            if (hand == side.Right && rightHandItem.type == "NONE")
                            {
                                rightHandItem = interactable.item;
                                Destroy(playerVisionTarget);
                                if (rightHandItem.type == "ACTIVESTONE")
                                    rightHandItem = inactiveRock;
                            }
                            if (hand == side.Left && leftHandItem.type == "NONE")
                            {
                                leftHandItem = interactable.item;
                                Destroy(playerVisionTarget);
                                if (leftHandItem.type == "ACTIVESTONE")
                                    leftHandItem = inactiveRock;
                            }
                        }
                    }
                }
            }
        }
    }
}