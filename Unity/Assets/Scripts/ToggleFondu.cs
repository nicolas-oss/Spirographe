using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleFondu : MonoBehaviour
{
    public GameObject Interface;

    public void ToggleFonduTrainee()
    {
		bool Check = GetComponent<Toggle>().isOn;
		Interface.GetComponent<Interface>().SelectedLine.Fondu = Check;
    }
}