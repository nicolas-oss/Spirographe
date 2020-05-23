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
	
	void Update()
	{
		//?
	}

    // Update is called once per frame
    public void AjusteEchelle(string arg)
    {
		//var input = gameObject.GetComponent<InputField>();
        //var arg = input.text;
		var NouvelleEchelle = float.Parse(arg);
		Debug.Log(NouvelleEchelle);
		Interface.GetComponent<Interface>().SelectedLine.Echelle = NouvelleEchelle;
    }
}
