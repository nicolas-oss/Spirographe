using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldRotation : MonoBehaviour
{
    public GameObject Interface;
	
    void Start()
    {
		var input = gameObject.GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        se.AddListener(AjusteRotation);
        input.onEndEdit = se;
    }
	
	void Update()
	{
		if (Interface.GetComponent<Interface>().SelectedLine.AnimRotation)
		{
			GetComponent<InputField>().text = Interface.GetComponent<Interface>().SelectedLine.Rotation.ToString();
		}
	}

    public void AjusteRotation(string arg)
    {
		var NouvelleRotation = float.Parse(arg);
		Interface.GetComponent<Interface>().SelectedLine.Rotation = NouvelleRotation;
		Interface.GetComponent<Interface>().SelectedLine.OffsetRotation = 0.0f;
    }
}
