using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsSystemSlider : MonoBehaviour, IPointerUpHandler
{
    /// <summary>
    /// Slider on pointer
    /// </summary>
    public void OnPointerUp(PointerEventData data)
    {
        OptionsSystem.Save();
    }
}
