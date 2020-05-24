using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAnimationCX : MonoBehaviour
{
    public GameObject Interface;

    public void ToggleCX()
    {
		bool Check = GetComponent<Toggle>().isOn;
		Interface.GetComponent<Interface>().SelectedLine.OndeCX = Check;
    }
}