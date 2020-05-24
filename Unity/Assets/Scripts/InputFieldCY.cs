using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldCY : MonoBehaviour
{
    public GameObject Interface;
	
    public void AjusteCY(string arg)
    {
		var NouveauCY = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.CY = NouveauCY;
    }
}
