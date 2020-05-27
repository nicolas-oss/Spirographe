using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldAmplitudeOnde3 : MonoBehaviour
{
    public GameObject Interface;

    public void AjusteAmplitudeOnde3(string arg)
    {
		var NouvelleAmplitudeOnde3 = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.A3 = NouvelleAmplitudeOnde3;
    }
}
