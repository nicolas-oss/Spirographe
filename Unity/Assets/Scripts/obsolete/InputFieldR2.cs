using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldR2 : MonoBehaviour
{
    public GameObject Interface;
	
    public void AjusteR2(string arg)
    {
		var NouveauR2 = float.Parse(GetComponent<InputField>().text);
		Interface.GetComponent<Interface>().SelectedLine.R2 = NouveauR2;
    }
}
