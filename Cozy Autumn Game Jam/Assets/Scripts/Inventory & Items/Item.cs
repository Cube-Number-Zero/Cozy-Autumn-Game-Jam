using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interactable Item", menuName = "Interactables (Custom)/Item")]

public class Item : ScriptableObject
{
    /* Here's a list of all current valid types:
     * TOY1 - The first part of the goal toy
     * TOY2 - The second part of the goal toy
     * STONE - Throwable rock
     * ACTIVESTONE - Throwable rock that hasn't hit anything yet
     * LOUDTOY - Loud single-use distraction
     * NONE - No item
     * BAG - The permanent backpack upgrade
     */
    [Tooltip("TOY1, TOY2, STONE, ACTIVESTONE, LOUDTOY, BAG, NONE")]
    public string type = "NONE";

    private void update()
    {
        switch(type)
        {
            case "STONE":
                updateStoneUniv();
                updateStoneDormant();
                return;
            case "ACTIVESTONE":
                updateStoneUniv();
                updateStoneActive();
                return;
            case "LOUDTOY":
                updateLoudToy();
                return;
            case "BAG":
                updateBag();
                return;
            case "NONE":
                return;
        }
        updateToyPiece();
    }

    private void updateToyPiece()
    {

    }
    private void updateStoneUniv()
    {

    }
    private void updateStoneActive()
    {

    }
    private void updateStoneDormant()
    {

    }
    private void updateLoudToy()
    {

    }
    private void updateBag()
    {

    }
}
