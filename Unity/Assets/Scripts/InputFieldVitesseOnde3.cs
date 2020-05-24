using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldVitesseOnde3 : MonoBehaviour
{
    public GameObject Interface;

    public void AjusteVitesseOnde3(string arg)
    {
		var NouvelleVitesseOnde3 = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.V3 = NouvelleVitesseOnde3;
    }
}
