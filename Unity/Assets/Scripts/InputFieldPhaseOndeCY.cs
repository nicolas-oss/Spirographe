using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldPhaseOndeCY : MonoBehaviour
{
    public GameObject Interface;

    public void AjustePhaseOndeCY(string arg)
    {
		var NouvellePhaseOndeCY = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.PY = NouvellePhaseOndeCY;
    }
}
