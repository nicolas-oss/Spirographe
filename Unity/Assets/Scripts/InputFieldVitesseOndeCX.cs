using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldVitesseOndeCX : MonoBehaviour
{
    public GameObject Interface;

    public void AjusteVitesseOndeCX(string arg)
    {
		var NouvelleVitesseOndeCX = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.VX = NouvelleVitesseOndeCX;
    }
}
