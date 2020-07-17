using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class InputFieldInterface : MonoBehaviour
{
	public float ValeurInitialeIF,ValeurSortieIF;
	GameObject ActiveObjectInScene;
	//public static SpiroFormule Spirographe.SelectedLine;
	public float FacteurDiv = 100.0f;
	public string InputID;
	public bool Clamp;
	public float Precision;
	public Vector3 CurrentMousePos,MousePosInitiale,DeltaMousePos;
	public InputField MainInputField;
	//public bool activation=true;
		
	public void Start()
	{
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

	/*public void GetActiveLine()
	{
		ActiveObjectInScene = Spirographe.GetActiveObject();
		SelectedLine = Spirographe.GetActiveSpiroFormule();
	}*/
	
	public void SetActiveEvent()
	{
		ClicPanelSurface.DestroyEvent();	
		ClicPanelSurface.FirstDragEvent += BeginAjusteWithDrag; //on souscrit à l'event BeginAjusteWithDrag
		ClicPanelSurface.MainDragEvent += AjusteWithDrag;		//on souscrit à l'event AjusteWithDrag
	}
	
	public void BeginAjusteWithDrag()
	{
		ValeurInitialeIF = (float)Spirographe.SelectedLine.GetType().GetField(InputID).GetValue(Spirographe.SelectedLine);
		MousePosInitiale = Input.mousePosition;
	}
	
	public void AjusteWithDrag()
	{
		//Debug.Log("IF drag");
		CurrentMousePos = Input.mousePosition;
		DeltaMousePos = CurrentMousePos-MousePosInitiale;
		ValeurSortieIF = ValeurInitialeIF + DeltaMousePos.x/FacteurDiv;
		if (Clamp) {ValeurSortieIF=(float)Math.Floor((ValeurSortieIF/Precision))*Precision;}
		GetComponent<InputField>().text = ValeurSortieIF.ToString();
		Spirographe.SelectedLine.GetType().GetField(InputID).SetValue(Spirographe.SelectedLine,ValeurSortieIF);
		Spirographe.ValueChange(); //Call ValueChange Event
	}
	
	public void AjusteWithEnter()
	{
		ValeurSortieIF = float.Parse(GetComponent<InputField>().text);
		Spirographe.SelectedLine.GetType().GetField(InputID).SetValue(Spirographe.SelectedLine,ValeurSortieIF);
		Spirographe.ValueChange(); //Call ValueChange Event
	}
	
	public void RefreshContent()
	{
		float ValeurCourante;
		if (Spirographe.SelectedLine==null)
		{
			GetComponent<InputField>().interactable = false;
		}
		else
		{
			GetComponent<InputField>().interactable = true;
			ValeurCourante = (float)Spirographe.SelectedLine.GetType().GetField(InputID).GetValue(Spirographe.SelectedLine); 	//on lit la valeur courante de l'inputID
			GetComponent<InputField>().text = ValeurCourante.ToString(); 								//on l'écrit dans le champ text de l'IF
		}
	}
}
