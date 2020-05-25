using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputFieldInterface : MonoBehaviour
{
    public GameObject Interface;
	float ValeurInitiale,ValeurSortie;
	//public GameObject InputFieldToRefresh;
	SpiroFormule SelectedLine;
	public float FacteurDiv = 100.0f;
	public string InputID;
	InputField MainInputField;
	
	void Start()
	{
		SelectedLine = Interface.GetComponent<Interface>().SelectedLine;
		MainInputField=gameObject.GetComponent<InputField>();
		MainInputField.onEndEdit.AddListener(delegate {SetActiveEvent(); });
	}
	
	void onPointerClick()
	{
		SetActiveEvent();
	}
	
	void onMouseDOwn()
	{
		BeginAjusteWithDrag();
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
		switch (InputID)
		{
			case "A1": ValeurSortie = SelectedLine.GetComponent<SpiroFormule>().A1; break;
			case "V1": ValeurSortie = SelectedLine.V1; break;
			case "P1": ValeurSortie = SelectedLine.P1; break;
		}
	}
	
	public void BeginAjusteWithDrag()
	{
		ValeurInitiale = ValeurSortie; 
	}
	
	public void AjusteWithDrag()
	{
		ValeurSortie = ValeurInitiale + Interface.GetComponent<Interface>().DeltaMousePos.x/FacteurDiv;
		GetComponent<InputField>().text = ValeurSortie.ToString();
		switch (InputID)
		{
			case "A1": SelectedLine.A1 = ValeurSortie; break;
			case "V1": SelectedLine.V1 = ValeurSortie; break;
			case "P1": SelectedLine.P1 = ValeurSortie; break;
		}
	}
}
