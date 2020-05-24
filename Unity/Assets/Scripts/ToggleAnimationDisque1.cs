using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAnimationDisque1 : MonoBehaviour
{
    public GameObject Interface;

    public void ToggleAR1()
    {
		bool Check = GetComponent<Toggle>().isOn;
		Interface.GetComponent<Interface>().SelectedLine.OndeR1 = Check;
    }
}