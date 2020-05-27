using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldVitesseOnde2 : MonoBehaviour
{
    public GameObject Interface;

    public void AjusteVitesseOnde2(string arg)
    {
		var NouvelleVitesseOnde2 = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.V2 = NouvelleVitesseOnde2;
    }
}
