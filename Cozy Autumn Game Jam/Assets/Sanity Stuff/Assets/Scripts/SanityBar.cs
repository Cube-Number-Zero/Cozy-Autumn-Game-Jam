using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour
{
    public Slider slider;

    public void SetSanity(float sanityLevel)
    {
        slider.value = sanityLevel;
    }

}
