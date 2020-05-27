using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldPhaseOnde3 : MonoBehaviour
{
    public GameObject Interface;

    public void AjustePhaseOnde3(string arg)
    {
		var NouvellePhaseOnde3 = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.P3 = NouvellePhaseOnde3;
    }
}
