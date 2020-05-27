using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldCX : MonoBehaviour
{
    public GameObject Interface;
	
    public void AjusteCX(string arg)
    {
		var NouveauCX = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.CX = NouveauCX;
    }
}
