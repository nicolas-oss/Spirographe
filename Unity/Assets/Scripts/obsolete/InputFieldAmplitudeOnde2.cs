using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldAmplitudeOnde2 : MonoBehaviour
{
    public GameObject Interface;

    public void AjusteAmplitudeOnde2(string arg)
    {
		var NouvelleAmplitudeOnde2 = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.A2 = NouvelleAmplitudeOnde2;
    }
}
