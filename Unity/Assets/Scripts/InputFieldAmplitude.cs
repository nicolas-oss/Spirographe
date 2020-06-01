using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;

public class InputFieldAmplitude : Spirographe
{
    public int index;
	float ValeurIn,ValeurOut,Value;
	GameObject ActiveObjectInScene;
	SpiroFormule SelectedLine;
	public float FacteurDiv = 100.0f;
	public string InputID;
	public bool Clamp;
	public float Precision;
	public Vector3 CurrentMousePos,MousePosInitiale,DeltaMousePos;
	InputField MainInputField;
	
	public void InitFromTextBouton()
	{
		Start();
		SetActiveEvent();
	}
	
	void Start()
	{
		GetActiveLine();
		MainInputField=gameObject.GetComponent<InputField>();
		MainInputField.onEndEdit.AddListener(delegate {AjusteWithEnter(); });
		MainInputField.onEndEdit.AddListener(delegate {SetActiveEvent(); });
		Spirographe.onRefreshInputField += RefreshContent;  //on souscrit à l'event onRefreshInputField
	}

	public void GetActiveLine()
	{
		ActiveObjectInScene = GetActiveObject();
		SelectedLine = GetActiveSpiroFormule();
	}
	
	public void SetActiveEvent()
	{
		ClicPanelSurface.DestroyEvent();	
		ClicPanelSurface.FirstDragEvent += BeginAjusteWithDrag;
		ClicPanelSurface.MainDragEvent += AjusteWithDrag;	
	}
	
	public void BeginAjusteWithDrag()
	{
		GetActiveLine();
		ValeurIn = SelectedLine.AA[index];
		MousePosInitiale = Input.mousePosition;
	}
	
	public void AjusteWithDrag()
	{
		CurrentMousePos = Input.mousePosition;
		DeltaMousePos = CurrentMousePos-MousePosInitiale;
		ValeurOut = ValeurIn + DeltaMousePos.x/FacteurDiv;
		if (Clamp) {ValeurOut=(float)Math.Floor((ValeurOut/Precision))*Precision;}
		GetComponent<InputField>().text = ValeurOut.ToString();
		SelectedLine.AA[index]=ValeurOut;
	}
	
	public void AjusteWithEnter()
	{
		ValeurOut = float.Parse(GetComponent<InputField>().text);
		SelectedLine.AA[index]=ValeurOut;
	}
	
	public void RefreshContent()
	{
		GetActiveLine();
		Value=(float)SelectedLine.AA[index];
		GetComponent<InputField>().text = Value.ToString();
	}
}
