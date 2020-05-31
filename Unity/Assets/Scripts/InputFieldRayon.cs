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
	float ValeurInitiale,ValeurSortie,Value;
	GameObject ActiveObjectInScene;
	SpiroFormule SelectedLine;
	public float FacteurDiv = 100.0f;
	public string InputID;
	public bool Clamp;
	public float Precision;
	public Vector3 CurrentMousePos,MousePosInitiale,DeltaMousePos;
	InputField MainInputField;
	
	void Start()
	{
		GetActiveLine();
		MainInputField=gameObject.GetComponent<InputField>();
		MainInputField.onEndEdit.AddListener(delegate {AjusteWithEnter(); });
		MainInputField.onEndEdit.AddListener(delegate {SetActiveEvent(); });
		//MainInputField.onEndEdit.AddListener(delegate {RefreshInputField(); });
		Spirographe.onRefreshInputField += RefreshContent;  //on souscrit à l'event onRefreshInputField /////MAIS IL FAUDRA LES DETRUIRE
		Spirographe.onRefreshInputFieldPanelDisques += RefreshContent;
	}
	
	public void UnsubscribeRefreshEvent()
	{
		Spirographe.onDestroyRefreshInputFieldEvent -= RefreshContent;
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
	}
	
	public void BeginAjusteWithDrag()
	{
		GetActiveLine();
		ValeurInitiale = SelectedLine.RR[index];
		MousePosInitiale = Input.mousePosition;
	}
	
	public void AjusteWithDrag()
	{
		CurrentMousePos = Input.mousePosition;
		DeltaMousePos = CurrentMousePos-MousePosInitiale;
		ValeurSortie = ValeurInitiale + DeltaMousePos.x/FacteurDiv;
		if (Clamp) {ValeurSortie=(float)Math.Floor((ValeurSortie/Precision))*Precision;}
		GetComponent<InputField>().text = ValeurSortie.ToString();
		SelectedLine.RR[index]=ValeurSortie;
		RefreshInputFieldPanelDisques();
	}
	
	public void AjusteWithEnter()
	{
		GetActiveLine();
		ValeurSortie = float.Parse(GetComponent<InputField>().text);
		SelectedLine.RR[index]=ValeurSortie;
		RefreshInputFieldPanelDisques();
	}
	
	public void RefreshContent()
	{
		GetActiveLine();
		Value=(float)SelectedLine.RR[index];
		GetComponent<InputField>().text = Value.ToString();
	}
}
