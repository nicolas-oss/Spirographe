using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldAmplitudeOndeCX : MonoBehaviour
{
    public GameObject Interface;

    public void AjusteAmplitudeOndeCX(string arg)
    {
		var NouvelleAmplitudeOndeCX = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.AX = NouvelleAmplitudeOndeCX;
    }
}
