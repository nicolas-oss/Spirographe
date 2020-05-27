using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldVitesseRotation : MonoBehaviour
{
    public GameObject Interface;

    public void AjusteVitesseRotation(string arg)
    {
		var NouvelleVitesseRotation = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.VitesseRotation = NouvelleVitesseRotation;
    }
}
