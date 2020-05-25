using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldFacteur2 : MonoBehaviour
{
    public GameObject Interface;

    public void AjusteFacteur2(string arg)
    {
		var NouveauFacteur2 = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.facteur2 = NouveauFacteur2;
    }
}
