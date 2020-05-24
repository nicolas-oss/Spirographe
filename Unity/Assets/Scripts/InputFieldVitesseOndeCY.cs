using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldVitesseOndeCY : MonoBehaviour
{
    public GameObject Interface;

    public void AjusteVitesseOndeCY(string arg)
    {
		var NouvelleVitesseOndeCY = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.VY = NouvelleVitesseOndeCY;
    }
}
