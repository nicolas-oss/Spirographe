using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldAmplitudeOndeCY : MonoBehaviour
{
    public GameObject Interface;

    public void AjusteAmplitudeOndeCY(string arg)
    {
		var NouvelleAmplitudeOndeCY = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.AY = NouvelleAmplitudeOndeCY;
    }
}
