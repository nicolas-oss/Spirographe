using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;

public class InputFieldProfondeurSmall : Spirographe
{
	int ValeurProfondeur;
	SpiroFormule SelectedLine;
	public string InputID;
	InputField MainInputField;
	public GameObject PanelTD;
	
	void Start()
	{		
		MainInputField=gameObject.GetComponent<InputField>();
		MainInputField.onEndEdit.AddListener(delegate {AjusteWithEnter(); });
	}
	
	public void AjusteWithEnter()
	{
		SelectedLine=GetActiveSpiroFormule();
		ValeurProfondeur = int.Parse(GetComponent<InputField>().text);
		Debug.Log(SelectedLine.profondeur.ToString());
		SelectedLine.GetComponent<SpiroFormule>().profondeur=ValeurProfondeur;
		PanelTD.GetComponent<PanelOptionTousDisquesSmall>().BuildPanel();
	}
	
	/*public void RefreshContent()
	{
		GetActiveLine();
		Value=(float)SelectedLine.GetType().GetField(InputID).GetValue(SelectedLine);
		GetComponent<InputField>().text = Value.ToString();
	}*/
}
