using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class InputFieldInterface : MonoBehaviour
{
    public GameObject Interface;
	float ValeurInitiale,ValeurSortie;
	//public GameObject InputFieldToRefresh;
	SpiroFormule SelectedLine;
	public float FacteurDiv = 100.0f;
	public string InputID;
	public bool Clamp;
	public float Precision;
	InputField MainInputField;
	
	public void InitFromTextBouton()
	{
		Start();
		SetActiveEvent();
	}
	
	void Start()
	{
		SelectedLine = Interface.GetComponent<Interface>().SelectedLine;
		MainInputField=gameObject.GetComponent<InputField>();
		MainInputField.onEndEdit.AddListener(delegate {AjusteWithEnter(); });
		MainInputField.onEndEdit.AddListener(delegate {SetActiveEvent(); });
		Debug.Log("Start");
	}
	
	void onPointerClick()
	{
		SetActiveEvent();
	}
	
	void onMouseDown()
	{
		SetActiveEvent();
	}
	
	void TaskOnClick()
	{
		//Interface.GetComponent<Interface>().SelectButton(gameObject);
		SetActiveEvent();
	}

	public void SetActiveEvent()
	{
		SelectedLine = Interface.GetComponent<Interface>().SelectedLine;
		Interface.GetComponent<Interface>().MainDragEvent.RemoveAllListeners();
		Interface.GetComponent<Interface>().FirstDragEvent.RemoveAllListeners();
		Interface.GetComponent<Interface>().FirstDragEvent.AddListener(BeginAjusteWithDrag);
		Interface.GetComponent<Interface>().MainDragEvent.AddListener(AjusteWithDrag);
		SelectedLine.GetType().GetField(InputID).GetValue(SelectedLine);
		//Debug.Log(SelectedLine.GetType().GetField(InputID).GetValue(SelectedLine));
		/*switch (InputID)
		{
			case "A1": ValeurSortie = SelectedLine.GetComponent<SpiroFormule>().A1; break;
			case "V1": ValeurSortie = SelectedLine.V1; break;
			case "P1": ValeurSortie = SelectedLine.P1; break;
			case "A2": ValeurSortie = SelectedLine.GetComponent<SpiroFormule>().A2; break;
			case "V2": ValeurSortie = SelectedLine.V2; break;
			case "P2": ValeurSortie = SelectedLine.P2; break;
			case "A3": ValeurSortie = SelectedLine.GetComponent<SpiroFormule>().A3; break;
			case "V3": ValeurSortie = SelectedLine.V3; break;
			case "P3": ValeurSortie = SelectedLine.P3; break;
			case "AX": ValeurSortie = SelectedLine.GetComponent<SpiroFormule>().AX; break;
			case "VX": ValeurSortie = SelectedLine.VX; break;
			case "PX": ValeurSortie = SelectedLine.PX; break;
			case "AY": ValeurSortie = SelectedLine.GetComponent<SpiroFormule>().AY; break;
			case "VY": ValeurSortie = SelectedLine.VY; break;
			case "PY": ValeurSortie = SelectedLine.PY; break;
			case "Echelle": ValeurSortie = SelectedLine.Echelle; break;
			case "Rotation": ValeurSortie = SelectedLine.Rotation; break;
			case "VitesseRotation": ValeurSortie = SelectedLine.VitesseRotation; break;
			case "R1": ValeurSortie = SelectedLine.R1; break;
			case "R2": ValeurSortie = SelectedLine.R2; break;
			case "R3": ValeurSortie = SelectedLine.R3; break;
			case "CX": ValeurSortie = SelectedLine.CX; break;
			case "CY": ValeurSortie = SelectedLine.CY; break;
			case "X1": ValeurSortie = SelectedLine.facteur1; break;
			case "X2": ValeurSortie = SelectedLine.facteur2; break;
		}*/
	}
	
	public void BeginAjusteWithDrag()
	{
		ValeurInitiale = (float)SelectedLine.GetType().GetField(InputID).GetValue(SelectedLine); 
	}
	
	public void AjusteWithDrag()
	{
		ValeurSortie = ValeurInitiale + Interface.GetComponent<Interface>().DeltaMousePos.x/FacteurDiv;
		if (Clamp) {ValeurSortie=(float)Math.Floor((ValeurSortie/Precision))*Precision;}
		GetComponent<InputField>().text = ValeurSortie.ToString();
		SelectedLine.GetType().GetField(InputID).SetValue(SelectedLine,ValeurSortie);
		//Debug.Log(SelectedLine.GetType().GetField(InputID).GetValue(SelectedLine));
		/*switch (InputID)
		{
			case "A1": SelectedLine.A1 = ValeurSortie; break;
			case "V1": SelectedLine.V1 = ValeurSortie; break;
			case "P1": SelectedLine.P1 = ValeurSortie; break;
			case "A2": SelectedLine.A2 = ValeurSortie; break;
			case "V2": SelectedLine.V2 = ValeurSortie; break;
			case "P2": SelectedLine.P2 = ValeurSortie; break;
			case "A3": SelectedLine.A3 = ValeurSortie; break;
			case "V3": SelectedLine.V3 = ValeurSortie; break;
			case "P3": SelectedLine.P3 = ValeurSortie; break;
			case "AX": SelectedLine.AX = ValeurSortie; break;
			case "VX": SelectedLine.VX = ValeurSortie; break;
			case "PX": SelectedLine.PX = ValeurSortie; break;
			case "AY": SelectedLine.AY = ValeurSortie; break;
			case "VY": SelectedLine.VY = ValeurSortie; break;
			case "PY": SelectedLine.PY = ValeurSortie; break;
			case "Echelle": SelectedLine.Echelle = ValeurSortie; break;
			case "Rotation": SelectedLine.Rotation = ValeurSortie; break;
			case "VitesseRotation": SelectedLine.VitesseRotation = ValeurSortie; break;
			case "R1": SelectedLine.R1 = (float)Math.Floor(10.0f*(ValeurSortie/100.0f))/10.0f; break;
			case "R2": SelectedLine.R2 = (float)Math.Floor(10.0f*(ValeurSortie/100.0f))/10.0f; break;
			case "R3": SelectedLine.R3 = (float)Math.Floor(10.0f*(ValeurSortie/100.0f))/10.0f; break;
			case "CX": SelectedLine.CX = (float)Math.Floor(10.0f*(ValeurSortie/100.0f))/10.0f; break;
			case "CY": SelectedLine.CY = (float)Math.Floor(10.0f*(ValeurSortie/100.0f))/10.0f; break;
			case "X1": SelectedLine.facteur1 = (float)Math.Floor(10.0f*(ValeurSortie/100.0f))/10.0f; break;
			case "X2": SelectedLine.facteur2 = (float)Math.Floor(10.0f*(ValeurSortie/100.0f))/10.0f; break;
		}*/
	}
	
	public void AjusteWithEnter()
	{
		ValeurSortie = float.Parse(GetComponent<InputField>().text);
		SelectedLine.GetType().GetField(InputID).SetValue(SelectedLine,ValeurSortie);
	}
}
