﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;

public class InputFieldFacteur : Spirographe
{
    public int index;
	float ValeurInitiale,ValeurSortie,Value;
	GameObject ActiveObjectInScene;
	SpiroFormule SelectedLine;
	public SpiroParametrable SelectedSpiroParam;
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
	}

	public void GetActiveLine()
	{
		ActiveObjectInScene = GetActiveObject();
		SelectedLine = GetActiveSpiroFormule();
		Debug.Log(ActiveObjectInScene.name);
		SelectedSpiroParam=GetActiveObject().GetComponent<SpiroParametrable>();
	}
	
	public void SetActiveEvent()
	{
		/*SelectedLine = Interface.GetComponent<Interface>().SelectedLine;
		Interface.GetComponent<Interface>().MainDragEvent.RemoveAllListeners();
		Interface.GetComponent<Interface>().FirstDragEvent.RemoveAllListeners();
		Interface.GetComponent<Interface>().FirstDragEvent.AddListener(BeginAjusteWithDrag);
		Interface.GetComponent<Interface>().MainDragEvent.AddListener(AjusteWithDrag);
		SelectedLine.GetType().GetField(InputID).GetValue(SelectedLine);*/
		/*If (ClicPanelSurface.FirstDragEvent != null))
		{
			Debug.Log("Non Vide");
		}*/
		ClicPanelSurface.DestroyEvent();	
		ClicPanelSurface.FirstDragEvent += BeginAjusteWithDrag;
		ClicPanelSurface.MainDragEvent += AjusteWithDrag;	
	}
	
	public void BeginAjusteWithDrag()
	{
		GetActiveLine();
		ValeurInitiale = SelectedSpiroParam.facteurT[index];
		MousePosInitiale = Input.mousePosition;
	}
	
	public void AjusteWithDrag()
	{
		CurrentMousePos = Input.mousePosition;
		DeltaMousePos = CurrentMousePos-MousePosInitiale;
		ValeurSortie = ValeurInitiale + DeltaMousePos.x/FacteurDiv;
		if (Clamp) {ValeurSortie=(float)Math.Floor((ValeurSortie/Precision))*Precision;}
		GetComponent<InputField>().text = ValeurSortie.ToString();
		SelectedSpiroParam.facteurT[index]=ValeurSortie;
	}
	
	public void AjusteWithEnter()
	{
		ValeurSortie = float.Parse(GetComponent<InputField>().text);
		SelectedSpiroParam.facteurT[index]=ValeurSortie;
	}
	
	public void RefreshContent()
	{
		GetActiveLine();
		Value=(float)SelectedSpiroParam.facteurT[index];
		GetComponent<InputField>().text = Value.ToString();
	}
}
