using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTrainee : MonoBehaviour
{
    public GameObject Interface;

    public void ToggleDuplication()
    {
		bool Check = GetComponent<Toggle>().isOn;
		Interface.GetComponent<Interface>().SelectedLine.Duplication = Check;
    }
}