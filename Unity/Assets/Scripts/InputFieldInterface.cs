using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;

public class InputFieldInterface : Spirographe
{
	float ValeurInitiale,ValeurSortie;
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
		Spirographe.onRefreshInputField += RefreshContent; //on souscrit à l'event onRefreshInputField
	}
	
	public void UnsubscribeRefreshEvent()
	{
		Spirographe.onDestroyRefreshInputFieldEvent -= RefreshContent;
	}

	public void GetActiveLine()
	{
		ActiveObjectInScene = GetActiveObject();
		SelectedLine = GetActiveSpiroFormule();
	}
	
	public void SetActiveEvent()
	{
		//Debug.Log("Event Activated");
		ClicPanelSurface.DestroyEvent();	
		ClicPanelSurface.FirstDragEvent += BeginAjusteWithDrag; //on souscrit à l'event BeginAjusteWithDrag
		ClicPanelSurface.MainDragEvent += AjusteWithDrag;		//on souscrit à l'event AjusteWithDrag
	}
	
	public void BeginAjusteWithDrag()
	{
		GetActiveLine();
		ValeurInitiale = (float)SelectedLine.GetType().GetField(InputID).GetValue(SelectedLine);
		MousePosInitiale = Input.mousePosition;
	}
	
	public void AjusteWithDrag()
	{
		Debug.Log("Dragging");
		CurrentMousePos = Input.mousePosition;
		DeltaMousePos = CurrentMousePos-MousePosInitiale;
		ValeurSortie = ValeurInitiale + DeltaMousePos.x/FacteurDiv;
		if (Clamp) {ValeurSortie=(float)Math.Floor((ValeurSortie/Precision))*Precision;}
		GetComponent<InputField>().text = ValeurSortie.ToString();
		SelectedLine.GetType().GetField(InputID).SetValue(SelectedLine,ValeurSortie);
	}
	
	public void AjusteWithEnter()
	{
		ValeurSortie = float.Parse(GetComponent<InputField>().text);
		SelectedLine.GetType().GetField(InputID).SetValue(SelectedLine,ValeurSortie);
	}
	
	public void RefreshContent()
	{
		float ValeurCourante;
		GetActiveLine();
		ValeurCourante = (float)SelectedLine.GetType().GetField(InputID).GetValue(SelectedLine); 	//on lit la valeur courante de l'inputID
		GetComponent<InputField>().text = ValeurCourante.ToString(); 								//on l'écrit dans le champ text de l'IF
	}
}
