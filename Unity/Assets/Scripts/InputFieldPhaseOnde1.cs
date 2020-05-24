using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldPhaseOnde1 : MonoBehaviour
{
    public GameObject Interface;

    public void AjustePhaseOnde1(string arg)
    {
		var NouvellePhaseOnde1 = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.P1 = NouvellePhaseOnde1;
    }
}
