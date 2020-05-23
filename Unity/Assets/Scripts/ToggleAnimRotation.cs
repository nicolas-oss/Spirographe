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
		float RotationInitiale = Interface.GetComponent<Interface>().SelectedLine.Rotation;
		float Vitesse = Interface.GetComponent<Interface>().SelectedLine.VitesseRotation;
		//float OffsetRotation = Interface.GetComponent<Interface>().SelectedLine.OffsetRotation;
		float Offset;
		if (Check)
		{
			Offset = RotationInitiale-Vitesse*Time.time;
		}
		else
		{
			Offset = 0.0f;
		}
		Interface.GetComponent<Interface>().SelectedLine.OffsetRotation = Offset;
    }
}