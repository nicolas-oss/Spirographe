using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleDisque3 : MonoBehaviour
{
    public GameObject Interface;

    public void ToggleR3()
    {
		bool Check = GetComponent<Toggle>().isOn;
		Interface.GetComponent<Interface>().SelectedLine.RotationAxeB = Check;
    }
}