using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldFacteur1 : MonoBehaviour
{
    public GameObject Interface;

    public void AjusteFacteur1(string arg)
    {
		var NouveauFacteur1 = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.V3 = NouveauFacteur1;
    }
}
