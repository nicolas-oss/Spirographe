using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldVitesseOnde1 : MonoBehaviour
{
    public GameObject Interface;

    public void AjusteVitesseOnde1(string arg)
    {
		var NouvelleVitesseOnde1 = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.V1 = NouvelleVitesseOnde1;
    }
}
