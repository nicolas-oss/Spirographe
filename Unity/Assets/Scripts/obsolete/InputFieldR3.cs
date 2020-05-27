using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldR3 : MonoBehaviour
{
    public GameObject Interface;
	
    public void AjusteR3(string arg)
    {
		var NouveauR3 = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.R3 = NouveauR3;
    }
}
