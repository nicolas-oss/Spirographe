using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;

public class InputFieldFacteur : Spirographe
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
	
	void Start()
	{
		GetActiveLine();
		MainInputField=gameObject.GetComponent<InputField>();
		MainInputField.onEndEdit.RemoveAllListeners();
		MainInputField.onEndEdit.AddListener(delegate {AjusteWithEnter(); });
		MainInputField.onEndEdit.AddListener(delegate {SetActiveEvent(); });
		Spirographe.onRefreshInputField += RefreshContent;  //on souscrit à l'event onRefreshInputField
		Spirographe.onRefreshInputFieldPanelDisques += RefreshContent;
	}

	public void UnsubscribeRefreshEvent()
	{
		Spirographe.onRefreshInputFieldPanelDisques -= RefreshContent;
		Spirographe.onRefreshInputField -= RefreshContent;
		Debug.Log("Event Refresh IFF unSubscribed");
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
		ValeurIn = SelectedLine.facteurT[index];
		MousePosInitiale = Input.mousePosition;
	}
	
	public void AjusteWithDrag()
	{
		CurrentMousePos = Input.mousePosition;
		DeltaMousePos = CurrentMousePos-MousePosInitiale;
		ValeurOut = ValeurIn + DeltaMousePos.x/FacteurDiv;
		if (Clamp) {ValeurOut=(float)Math.Floor((ValeurOut/Precision))*Precision;}
		GetComponent<InputField>().text = ValeurOut.ToString();
		SelectedLine.facteurT[index]=ValeurOut;
		RefreshInputFieldPanelDisques();
	}
	
	public void AjusteWithEnter()
	{
		ValeurOut = float.Parse(GetComponent<InputField>().text);
		SelectedLine.facteurT[index]=ValeurOut;
		RefreshInputFieldPanelDisques();
	}
	
	public void RefreshContent()
	{
		GetActiveLine();
		Value=(float)SelectedLine.facteurT[index];
		GetComponent<InputField>().text = Value.ToString();
	}
}
