﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;

public class InputFieldRayon : Spirographe
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
	
	public void Start()
	{
		GetActiveLine();
		MainInputField=gameObject.GetComponent<InputField>();
		MainInputField.onEndEdit.RemoveAllListeners();
		MainInputField.onEndEdit.AddListener(delegate {AjusteWithEnter(); });
		MainInputField.onEndEdit.AddListener(delegate {SetActiveEvent(); });
		//MainInputField.onEndEdit.AddListener(delegate {RefreshInputField(); });
		Spirographe.onRefreshInputField += RefreshContent;  //on souscrit à l'event onRefreshInputField /////MAIS IL FAUDRA LES DETRUIRE
		Spirographe.onRefreshInputFieldPanelDisques += RefreshContent;
	}
	
	void onDestroy()
	{
		Debug.Log("IFR destroyed");
		//UnsubscribeRefreshEvent();
		//MainInputField=gameObject.GetComponent<InputField>();
		//MainInputField.onEndEdit.RemoveAllListeners();
		//MainInputField.onEndEdit.AddListener(delegate {SetActiveEvent(); });
	}
	
	public void SubscribeRefreshEvent()
	{
		Spirographe.onRefreshInputFieldPanelDisques += RefreshContent;
	}
	
	public void UnsubscribeRefreshEvent()
	{
		Spirographe.onRefreshInputFieldPanelDisques -= RefreshContent;
		Spirographe.onRefreshInputField -= RefreshContent;
		Debug.Log("Event Refresh IFR unSubscribed");
		ClicPanelSurface.FirstDragEvent -= BeginAjusteWithDrag;
		ClicPanelSurface.MainDragEvent -= AjusteWithDrag;
	}

	public void InitFromTextBouton()
	{
		Start();
		SetActiveEvent();
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
		Debug.Log("Event Set");
	}
	
	public void BeginAjusteWithDrag()
	{
		GetActiveLine();
		ValeurIn = SelectedLine.RR[index];
		MousePosInitiale = Input.mousePosition;
	}
	
	public void AjusteWithDrag()
	{
		CurrentMousePos = Input.mousePosition;
		DeltaMousePos = CurrentMousePos-MousePosInitiale;
		ValeurOut = ValeurIn + DeltaMousePos.x/FacteurDiv;
		if (Clamp) {ValeurOut=(float)Math.Floor((ValeurOut/Precision))*Precision;}
		GetComponent<InputField>().text = ValeurOut.ToString();
		SelectedLine.RR[index]=ValeurOut;
		RefreshInputFieldPanelDisques(); // on rafraichi les autres champs IFR
		Debug.Log("ok");
	}
	
	public void AjusteWithEnter()
	{
		GetActiveLine();
		ValeurOut = float.Parse(GetComponent<InputField>().text);
		SelectedLine.RR[index]=ValeurOut;
		RefreshInputFieldPanelDisques();
	}
	
	public void RefreshContent()
	{
		GetActiveLine();
		Value=(float)SelectedLine.RR[index];
		GetComponent<InputField>().text = Value.ToString();
	}
}
