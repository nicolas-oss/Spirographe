using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldEchelle : MonoBehaviour
{
    public GameObject Interface;
	
    void Start()
    {
		var input = gameObject.GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        se.AddListener(AjusteEchelle);
        input.onEndEdit = se;
    }

    public void AjusteEchelle(string arg)
    {
		var NouvelleEchelle = float.Parse(arg);
		Interface.GetComponent<Interface>().SelectedLine.Echelle = NouvelleEchelle;
    }
}
