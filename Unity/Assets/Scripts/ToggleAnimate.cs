using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAnimate : MonoBehaviour
{
    public GameObject Interface;

    public void ToggleAnimateDuplication()
    {
		bool Check = GetComponent<Toggle>().isOn;
		Interface.GetComponent<Interface>().SelectedLine.Animate = Check;
    }
}