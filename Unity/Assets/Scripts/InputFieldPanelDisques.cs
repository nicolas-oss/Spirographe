using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class InputFieldPanelDisques : MonoBehaviour
{
	GameObject ActiveObjectInScene;
	SpiroFormule SelectedLine;
	public float FacteurDiv = 100.0f;
	public bool Clamp;
	public float Precision;
	public Vector3 CurrentMousePos,MousePosInitiale,DeltaMousePos;
	InputField MainInputField;
	
	public int index;
	public string inputID;
	public float ValeurIn,ValeurOut;

	
	public void Start()
	{
		GetActiveLine();
		MainInputField=gameObject.GetComponent<InputField>();
		MainInputField.onEndEdit.AddListener(delegate {AjusteWithEnter(); });
		MainInputField.onEndEdit.AddListener(delegate {SetActiveEvent(); });
		Spirographe.onRefreshInputField += RefreshContent; //on souscrit à l'event onRefreshInputField
		Spirographe.onRefreshInputFieldPanelDisques += RefreshContent;
	}
	
	public void GetActiveLine()
	{
		ActiveObjectInScene = Spirographe.GetActiveObject();
		SelectedLine = Spirographe.GetActiveSpiroFormule();
	}

	void onDestroy()
	{
		Debug.Log("IFR destroyed");
		//UnsubscribeRefreshEvent();
		//MainInputField=gameObject.GetComponent<InputField>();
		//MainInputField.onEndEdit.RemoveAllListeners();
		//MainInputField.onEndEdit.AddListener(delegate {SetActiveEvent(); });
	}
	
		public void SetActiveEvent()
	{
		//Debug.Log("Event Activated");
		ClicPanelSurface.DestroyEvent();	
		ClicPanelSurface.FirstDragEvent += BeginAjusteWithDrag; //on souscrit à l'event BeginAjusteWithDrag
		ClicPanelSurface.MainDragEvent += AjusteWithDrag;		//on souscrit à l'event AjusteWithDrag
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
	
	public void BeginAjusteWithDrag()
	{
		GetActiveLine();
		ValeurIn = LectureValeur(); //SelectedLine.RR[index];
		MousePosInitiale = Input.mousePosition;
	}
	
	public void AjusteWithDrag()
	{
		CurrentMousePos = Input.mousePosition;
		DeltaMousePos = CurrentMousePos-MousePosInitiale;
		ValeurOut = ValeurIn + DeltaMousePos.x/FacteurDiv;
		if (Clamp) {ValeurOut=(float)Math.Floor((ValeurOut/Precision))*Precision;}
		GetComponent<InputField>().text = ValeurOut.ToString();
		EcritureValeur(ValeurOut);
		Spirographe.RefreshInputFieldPanelDisques(); // on rafraichi les autres champs IFR
		//Debug.Log("ok");
		Spirographe.ValueChange(); //Call ValueChange Event
	}
	
	public void AjusteWithEnter()
	{
		GetActiveLine();
		ValeurOut = float.Parse(GetComponent<InputField>().text);
		EcritureValeur(ValeurOut);
		Spirographe.RefreshInputFieldPanelDisques(); // on rafraichi les autres champs IFR
		Spirographe.ValueChange(); //Call ValueChange Event
	}
	
	void EcritureValeur(float ValeurOut)
	{
		switch (inputID)
		{
			case  "RR" : SelectedLine.RR[index]=ValeurOut; break;
			case  "RRO" : SelectedLine.RROffset[index]=ValeurOut; break;
			case  "AA" : SelectedLine.AA[index]=ValeurOut; break;
			case  "VV" : SelectedLine.VV[index]=ValeurOut; break;
			case  "PP" : SelectedLine.PP[index]=ValeurOut; break;
			case  "facteurT" : SelectedLine.facteurT[index]=ValeurOut; break;
		}
	}
	
	public float LectureValeur()
	{
		float Valeur=0.0f;
		switch (inputID)
		{
			case  "RR" : Valeur=SelectedLine.RR[index]; break;
			case  "RRO" : Valeur=SelectedLine.RROffset[index]; break;
			case  "AA" : Valeur=SelectedLine.AA[index]; break;
			case  "VV" : Valeur=SelectedLine.VV[index]; break;
			case  "PP" : Valeur=SelectedLine.PP[index]; break;
			case  "facteurT" : Valeur=SelectedLine.facteurT[index]; break;
		}
		return Valeur;
	}
	
	public void RefreshContent()
	{
		float Value;
		GetActiveLine();
		if (SelectedLine==null) {GetComponent<InputField>().interactable = false;}
		else
		{
			GetComponent<InputField>().interactable = true;
			Value=LectureValeur();
			GetComponent<InputField>().text = Value.ToString();
		}
	}
}
