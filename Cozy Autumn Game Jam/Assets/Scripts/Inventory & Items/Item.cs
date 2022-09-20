using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
     * ACTIVELOUDTOY - Distraction, whilst distracting
     * NONE - No item
     * BAG - The permanent backpack upgrade
     * WOOD - Firewood
     */
    [Tooltip("TOY1, TOY2, STONE, ACTIVESTONE, LOUDTOY, ACTIVELOUDTOY, BAG, WOOD, NONE")]
    public string type = "NONE";
    [Tooltip("How long, in seconds, the item will exist before changing state")]
    // Only used by ACTIVESTONE and ACTIVELOUDTOY
    public float maxTimer = 0f;
    [Tooltip("What this object will turn into once its timer runs out")]
    public Item turnInto;
}
