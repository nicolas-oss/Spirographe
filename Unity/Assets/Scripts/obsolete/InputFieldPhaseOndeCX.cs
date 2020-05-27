using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldPhaseOndeCX : MonoBehaviour
{
    public GameObject Interface;

    public void AjustePhaseOndeCX(string arg)
    {
		var NouvellePhaseOndeCX = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.PX = NouvellePhaseOndeCX;
    }
}
