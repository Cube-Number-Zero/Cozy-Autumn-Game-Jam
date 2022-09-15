using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace StarterAssets
{
    public class UIController : MonoBehaviour
    {

        public enum itemID { Toy, Stone, Loudtoy, Wood, NoneR, NoneL, NoneB };


        private bool inventoryOpen;
        public InteractionController interactionController;
        public PlayerManager manager;

        #region initializing text components
        public TextMeshProUGUI tabInventoryTextItem;
        public TextMeshProUGUI leftHandItemName;
        public TextMeshProUGUI rightHandItemName;
        public TextMeshProUGUI bagItemName;
        public TextMeshProUGUI rightCornerControls;
        #endregion


        void Start()
        {
            manager = GameObject.Find("GameManager").GetComponent<PlayerManager>();
            interactionController = GameObject.Find("PlayerCapsule").GetComponent<InteractionController>();

            #region assigning text components
            tabInventoryTextItem = GameObject.Find("constantControlsText").GetComponent<TextMeshProUGUI>();
            leftHandItemName = GameObject.Find("leftHandItemName").GetComponent<TextMeshProUGUI>();
            rightHandItemName = GameObject.Find("rightHandItemName").GetComponent<TextMeshProUGUI>();
            bagItemName = GameObject.Find("bagItemName").GetComponent<TextMeshProUGUI>();
            rightCornerControls = GameObject.Find("rightCornerControls").GetComponent<TextMeshProUGUI>();
            #endregion


        }

        void Update()
        {
            inventoryOpen = interactionController.inventoryOpen;
            if (inventoryOpen)
            {
                tabInventoryTextItem.text = "[TAB] close";
                rightCornerControls.text = "[LEFT/RIGHT CLICK] drop item";
                leftHandItemName.text = getNameFromID(getIDForItemL());
                rightHandItemName.text = getNameFromID(getIDForItemR());
                bagItemName.text = interactionController.hasBag ? getNameFromID(getIDForItemB()) : "";
            }
            else
            {
                tabInventoryTextItem.text = "[TAB] inventory";
                leftHandItemName.text = "";
                rightHandItemName.text = "";
                bagItemName.text = "";
                if(interactionController.isLookingAtItem)
                {
                    if (interactionController.leftHandItem.type == "NONE" && interactionController.rightHandItem.type == "NONE")
                        rightCornerControls.text = "[Q/E] pick up";
                    else if (interactionController.leftHandItem.type == "NONE")
                        rightCornerControls.text = "[Q] pick up";
                    else if (interactionController.rightHandItem.type == "NONE")
                        rightCornerControls.text = "[E] pick up";
                    else
                        rightCornerControls.text = "hands full";
                }
                else
                    rightCornerControls.text = "";
            }
        }

        public string getNameFromID(itemID ID)
        {
            switch (ID)
            {
                case itemID.Toy: return "Toy Piece";
                case itemID.Stone: return "Stone";
                case itemID.Loudtoy: return "Loud Toy";
                case itemID.Wood: return "Firewood";
                case itemID.NoneR: return "None";
                case itemID.NoneL: return "None";
                case itemID.NoneB: return "None";
            }
            return null;
        }

        public string getDescFromID(itemID ID)
        {
            switch (ID)
            {
                case itemID.Toy:     return "A piece of your beloved toy. Bring it back to the cabin to attach it.";
                case itemID.Stone:   return "A stone. Using it will throw it.";
                case itemID.Loudtoy: return "A toy that makes quite a racket. Use it to turn it on and drop it.";
                case itemID.Wood:    return "Some twigs that can be used for firewood. Bring them back to the cabin to place in the fire.";
                case itemID.NoneR:   return "Your right hand is empty.";
                case itemID.NoneL:   return "Your left hand is empty.";
                case itemID.NoneB:   return "This empty bag can be used to store items.";
            }
            return null;
        }

        itemID getIDForItemL()
        {
            switch(interactionController.leftHandItem.type)
            {
                case "TOY1":    return itemID.Toy;
                case "TOY2":    return itemID.Toy;
                case "STONE":   return itemID.Stone;
                case "LOUDTOY": return itemID.Loudtoy;
                case "WOOD":    return itemID.Wood;
                case "NONE":    return itemID.NoneL;
            }
            Debug.Log("WARNING: NO ITEM DETECTED IN LEFT HAND  -UIController");
            return itemID.Toy;
        }

        itemID getIDForItemR()
        {
            switch (interactionController.rightHandItem.type)
            {
                case "TOY1": return itemID.Toy;
                case "TOY2": return itemID.Toy;
                case "STONE": return itemID.Stone;
                case "LOUDTOY": return itemID.Loudtoy;
                case "WOOD": return itemID.Wood;
                case "NONE": return itemID.NoneR;
            }
            Debug.Log("WARNING: NO VALID ITEM DETECTED IN RIGHT HAND  -UIController");
            return itemID.Toy;
        }

        itemID getIDForItemB()
        {
            switch (interactionController.bagItem.type)
            {
                case "TOY1": return itemID.Toy;
                case "TOY2": return itemID.Toy;
                case "STONE": return itemID.Stone;
                case "LOUDTOY": return itemID.Loudtoy;
                case "WOOD": return itemID.Wood;
                case "NONE": return itemID.NoneB;
            }
            Debug.Log("WARNING: NO VALID ITEM DETECTED IN BAG  -UIController");
            return itemID.Toy;
        }
    }

}