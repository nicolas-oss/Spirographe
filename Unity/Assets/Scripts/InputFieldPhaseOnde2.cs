using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldPhaseOnde2 : MonoBehaviour
{
    public GameObject Interface;

    public void AjustePhaseOnde2(string arg)
    {
		var NouvellePhaseOnde2 = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.P2 = NouvellePhaseOnde2;
    }
}
