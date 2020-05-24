using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldR1 : MonoBehaviour
{
    public GameObject Interface;
	
    public void AjusteR1(string arg)
    {
		var NouveauR1 = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.R1 = NouveauR1;
    }
}
