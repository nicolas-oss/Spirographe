using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;

public class InputFieldInterface : MonoBehaviour
{
	public float ValeurInitialeIF,ValeurSortieIF;
	GameObject ActiveObjectInScene;
	public static SpiroFormule SelectedLine;
	public float FacteurDiv = 100.0f;
	public string InputID;
	public bool Clamp;
	public float Precision;
	public Vector3 CurrentMousePos,MousePosInitiale,DeltaMousePos;
	public InputField MainInputField;
		
	public void Start()
	{
		GetActiveLine();
		MainInputField=gameObject.GetComponent<InputField>();
		MainInputField.onEndEdit.AddListener(delegate {AjusteWithEnter(); });
		MainInputField.onEndEdit.AddListener(delegate {SetActiveEvent(); });
		Spirographe.onRefreshInputField += RefreshContent; //on souscrit à l'event onRefreshInputField
		//Spirographe.onSelectionLine += RefreshContent;		//on souscrit à l'event onSelection
	}
	
	public void InitFromTextBouton()
	{
		Start();
		SetActiveEvent();
	}

	public void UnsubscribeRefreshEvent()
	{
		Spirographe.onDestroyRefreshInputFieldEvent -= RefreshContent;
	}

	public void GetActiveLine()
	{
		ActiveObjectInScene = Spirographe.GetActiveObject();
		SelectedLine = Spirographe.GetActiveSpiroFormule();
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
		ValeurInitialeIF = (float)SelectedLine.GetType().GetField(InputID).GetValue(SelectedLine);
		MousePosInitiale = Input.mousePosition;
	}
	
	public void AjusteWithDrag()
	{
		//Debug.Log("Dragging");
		CurrentMousePos = Input.mousePosition;
		DeltaMousePos = CurrentMousePos-MousePosInitiale;
		ValeurSortieIF = ValeurInitialeIF + DeltaMousePos.x/FacteurDiv;
		if (Clamp) {ValeurSortieIF=(float)Math.Floor((ValeurSortieIF/Precision))*Precision;}
		GetComponent<InputField>().text = ValeurSortieIF.ToString();
		SelectedLine.GetType().GetField(InputID).SetValue(SelectedLine,ValeurSortieIF);
	}
	
	public void AjusteWithEnter()
	{
		ValeurSortieIF = float.Parse(GetComponent<InputField>().text);
		SelectedLine.GetType().GetField(InputID).SetValue(SelectedLine,ValeurSortieIF);
	}
	
	public void RefreshContent()
	{
		float ValeurCourante;
		GetActiveLine();
		ValeurCourante = (float)SelectedLine.GetType().GetField(InputID).GetValue(SelectedLine); 	//on lit la valeur courante de l'inputID
		GetComponent<InputField>().text = ValeurCourante.ToString(); 								//on l'écrit dans le champ text de l'IF
	}
}
