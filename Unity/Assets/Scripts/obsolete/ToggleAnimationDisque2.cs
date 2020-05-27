using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAnimationDisque2 : MonoBehaviour
{
    public GameObject Interface;

    public void ToggleAR2()
    {
		bool Check = GetComponent<Toggle>().isOn;
		Interface.GetComponent<Interface>().SelectedLine.OndeR2 = Check;
    }
}