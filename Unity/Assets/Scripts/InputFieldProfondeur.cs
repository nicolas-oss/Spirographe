using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;

public class InputFieldProfondeur : Spirographe
{
	int ValeurProfondeur;
	//GameObject ActiveObjectInScene;
	public SpiroParametrable SelectedLine;
	public string InputID;
	InputField MainInputField;
	public GameObject PanelTD;
	
	void Start()
	{
		//GetActiveLine();
		MainInputField=gameObject.GetComponent<InputField>();
		MainInputField.onEndEdit.AddListener(delegate {AjusteWithEnter(); });
	}
	
	public void AjusteWithEnter()
	{
		ValeurProfondeur = int.Parse(GetComponent<InputField>().text);
		//Debug.Log(ValeurProfondeur.ToString());
		SelectedLine.profondeur=ValeurProfondeur;
		PanelTD.GetComponent<PanelOptionTousDisques>().BuildPanel();
	}
	
	/*public void RefreshContent()
	{
		GetActiveLine();
		Value=(float)SelectedLine.GetType().GetField(InputID).GetValue(SelectedLine);
		GetComponent<InputField>().text = Value.ToString();
	}*/
}
