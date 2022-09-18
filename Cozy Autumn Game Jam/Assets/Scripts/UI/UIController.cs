using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UIElements;

namespace StarterAssets
{
    public class UIController : MonoBehaviour
    {

        public enum itemID { Toy, Stone, Loudtoy, Wood, NoneR, NoneL, NoneB };


        private bool usingController = false;

        #region initializing text components
        public TextMeshProUGUI leftCornerControls;
        public TextMeshProUGUI leftHandItemName;
        public TextMeshProUGUI rightHandItemName;
        public TextMeshProUGUI bagItemName;
        public TextMeshProUGUI leftHandItemDesc;
        public TextMeshProUGUI rightHandItemDesc;
        public TextMeshProUGUI bagItemDesc;
        public TextMeshProUGUI rightCornerControls;
        public TextMeshProUGUI helpTextR;
        public TextMeshProUGUI helpTextL;
        #endregion
        public GameObject bg;


        void Start()
        {

            #region assigning text components
            leftCornerControls = GameObject.Find("leftCornerControls").GetComponent<TextMeshProUGUI>();
            leftHandItemName = GameObject.Find("leftHandItemName").GetComponent<TextMeshProUGUI>();
            rightHandItemName = GameObject.Find("rightHandItemName").GetComponent<TextMeshProUGUI>();
            bagItemName = GameObject.Find("bagItemName").GetComponent<TextMeshProUGUI>();
            leftHandItemDesc = GameObject.Find("leftHandItemDesc").GetComponent<TextMeshProUGUI>();
            rightHandItemDesc = GameObject.Find("rightHandItemDesc").GetComponent<TextMeshProUGUI>();
            bagItemDesc = GameObject.Find("bagItemDesc").GetComponent<TextMeshProUGUI>();
            rightCornerControls = GameObject.Find("rightCornerControls").GetComponent<TextMeshProUGUI>();
            helpTextR = GameObject.Find("helpMenuTextRight").GetComponent<TextMeshProUGUI>();
            helpTextL = GameObject.Find("helpMenuTextLeft").GetComponent<TextMeshProUGUI>();
            #endregion


        }

        void Update()
        {
            usingController = StarterAssetsInputs.analogMovement;
            if (InteractionController.inventoryOpen)
            {
                bg.SetActive(true);

                leftCornerControls.text = usingController ? "[NORTH BUTTON] close" : "[TAB] close";
                rightCornerControls.text = usingController ? "[LEFT/RIGHT TRIGGER] drop item" : "[LEFT/RIGHT CLICK] drop item";
                leftHandItemName.text = getNameFromID(getIDForItemL());
                rightHandItemName.text = getNameFromID(getIDForItemR());
                bagItemName.text = InteractionController.hasBag ? getNameFromID(getIDForItemB()) : "";
                leftHandItemDesc.text = getDescFromID(getIDForItemL());
                rightHandItemDesc.text = getDescFromID(getIDForItemR());
                bagItemDesc.text = InteractionController.hasBag ? getDescFromID(getIDForItemB()) : "";
                helpTextR.text = "";
                helpTextL.text = "";
            }
            else if (InteractionController.helpMenuOpen)
            {
                bg.SetActive(true);

                leftCornerControls.text = usingController ? "[SELECT] close" : "[H] close";
                leftHandItemName.text = "";
                rightHandItemName.text = "";
                bagItemName.text = "";
                leftHandItemDesc.text = "";
                rightHandItemDesc.text = "";
                bagItemDesc.text = "";
                helpTextR.text = usingController ?
@"CONTROLS:
Left Joystick: Move
Right Joystick: Look
Left Joystick Press: Sprint
East Button: Sneak
Left/Right Triggers: Use held item
Left/Right Bumpers: Pick up item
Left/Right Bumpers: Interact with object
Select: Toggle this menu
North Button: Toggle inventory overlay
IN INVENTORY:
Left/Right Bumpers: Drop item
Left/Right Triggers: Swap item into bag
(if available)" :
@"CONTROLS:
W/A/S/D or Arrows: Move
Mouse: Look
Shift: Sprint
Ctrl or C: Sneak
Left/Right Click: Use held item
Q/E: Pick up item
Q/E: Interact with object
H: Toggle this menu
Tab: Toggle inventory overlay
IN INVENTORY:
Left/Right Click: Drop item
Q/E: Swap item into bag
(if available)";
                helpTextL.text =
@"OVERVIEW:
- Find every piece of your toy, assemble it, and return to your bed to complete the game.
- Staying in the cabin with the fire lit will help to prevent you from losing your mind.
- The fire will burn out eventually. Bring firewood from outside to keep the fire burning.

Made in 14 days for the Cozy Autumn Game Jam";
            }
            else
            {
                bg.SetActive(false);

                leftCornerControls.text = usingController ? "[SELECT] help" : "[H] help";
                leftHandItemName.text = "";
                rightHandItemName.text = "";
                bagItemName.text = "";
                leftHandItemDesc.text = "";
                rightHandItemDesc.text = "";
                bagItemDesc.text = "";
                helpTextR.text = "";
                helpTextL.text = "";
                if (InteractionController.isLookingAtItem)
                {
                    if (InteractionController.leftHandItem.type == "NONE" && InteractionController.rightHandItem.type == "NONE")
                        rightCornerControls.text = "[Q/E] pick up";
                    else if (InteractionController.leftHandItem.type == "NONE")
                        rightCornerControls.text = "[Q] pick up";
                    else if (InteractionController.rightHandItem.type == "NONE")
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
                case itemID.NoneR: return "";
                case itemID.NoneL: return "";
                case itemID.NoneB: return "";
            }
            return null;
        }

        public string getDescFromID(itemID ID)
        {
            switch (ID)
            {
                case itemID.Toy: return "A piece of your beloved toy. Bring it back\nto the cabin\nto attach it.";
                case itemID.Stone: return "A stone. Using it will throw it.";
                case itemID.Loudtoy: return "A toy that makes quite a racket. Use it to turn it on and drop it.";
                case itemID.Wood: return "Some twigs that can be used for firewood. Bring them back to the cabin to place\nin the fire.";
                case itemID.NoneR: return "Your right hand is empty.";
                case itemID.NoneL: return "Your left hand is empty.";
                case itemID.NoneB: return "Your bag is empty.";
            }
            return null;
        }

        itemID getIDForItemL()
        {
            switch (InteractionController.leftHandItem.type)
            {
                case "TOY1": return itemID.Toy;
                case "TOY2": return itemID.Toy;
                case "STONE": return itemID.Stone;
                case "LOUDTOY": return itemID.Loudtoy;
                case "WOOD": return itemID.Wood;
                case "NONE": return itemID.NoneL;
            }
            Debug.Log("WARNING: NO ITEM DETECTED IN LEFT HAND  -UIController");
            return itemID.Toy;
        }

        itemID getIDForItemR()
        {
            switch (InteractionController.rightHandItem.type)
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
            switch (InteractionController.bagItem.type)
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