using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAnimRotation : MonoBehaviour
{
    public GameObject Interface;

    public void ToggleRotation()
    {
		bool Check = GetComponent<Toggle>().isOn;
		Interface.GetComponent<Interface>().SelectedLine.AnimRotation = Check;
		float Vitesse = Interface.GetComponent<Interface>().SelectedLine.VitesseRotation;
		float OffsetRotation = Interface.GetComponent<Interface>().SelectedLine.OffsetRotation;
		float Offset;
		if (Check)
		{
			Offset = (-1.0f)*Vitesse*Time.time;
		}
		else
		{
			Offset = Vitesse*Time.time;
		}
		Interface.GetComponent<Interface>().SelectedLine.OffsetRotation = Offset+OffsetRotation;
    }
}