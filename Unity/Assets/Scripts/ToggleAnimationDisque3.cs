using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAnimationDisque3 : MonoBehaviour
{
    public GameObject Interface;

    public void ToggleAR3()
    {
		bool Check = GetComponent<Toggle>().isOn;
		Interface.GetComponent<Interface>().SelectedLine.OndeR3 = Check;
    }
}