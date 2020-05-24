using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAnimationCY : MonoBehaviour
{
    public GameObject Interface;

    public void ToggleCY()
    {
		bool Check = GetComponent<Toggle>().isOn;
		Interface.GetComponent<Interface>().SelectedLine.OndeCY = Check;
    }
}