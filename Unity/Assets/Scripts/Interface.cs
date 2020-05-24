using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class Interface : MonoBehaviour
{
    public SpiroFormule SelectedLine;
	
	public Color UnselectedColor,SelectedColor;
	
	public UnityEvent MainDragEvent,FirstDragEvent;
	public GameObject ButtonEchelle,ButtonRotation,ButtonDisque1,ButtonDisque2,ButtonDisque3,ButtonCrayon,ButtonDuplication;
	public GameObject PanelOptionButtonEchelle,PanelOptionButtonRotation,PanelOptionButtonDisque1,PanelOptionButtonDisque2,PanelOptionButtonDisque3,PanelOptionButtonCrayonX,PanelOptionButtonCrayonY,PanelOptionButtonDuplication;
	public GameObject Surface;
	public GameObject InputEchelle,InputRotation,InputR1,InputR2,InputR3,InputCX,InputCY;
	public Vector3 DeltaMousePos;
	GameObject ActiveButton;
	//public GameObject PreviousSelectedButton;
	public float CurrentEchelle,CurrentRotation,CurrentSizeDisque1,CurrentSizeDisque2,CurrentSizeDisque3,CurrentSizeCX,CurrentSizeCY;
	
    void Start()
    {
		//Création évenement principal et ajout listener
		if (MainDragEvent == null) 
		{
			MainDragEvent = new UnityEvent();
		}
		if (FirstDragEvent == null) 
		{
			FirstDragEvent = new UnityEvent();
		}
		FirstDragEvent.AddListener(CliqueSurfaceDeTravail);
		MainDragEvent.AddListener(CliqueSurfaceDeTravail);
    }

    void Update()
    {
    }
	
	public void RefreshPanelOptionEchelle()
	{
		//InputEchelle.GetComponent<InputField>().value = SelectedLine.Echelle;
	}
	
	/////////////////////////////////////////////// Select Button //////////////////////////////////////////////////////
	
	public void SelectButton(GameObject PressedButton)
	{
		DeselectAllButton();
		HideAllOptionPanels();
		SetColorActive(PressedButton);
		ActiveButton = PressedButton;
		ShowCurrentOptionPanel();
		SetActiveEvent();
	}
	
	public void DeselectAllButton()
	{
		GameObject[] AllButtons;
		AllButtons = GameObject.FindGameObjectsWithTag("Button");
		foreach(GameObject InspectedButton in AllButtons)
		{
			var InspectedMyButton=InspectedButton.GetComponent<Button>();
			var InspectedColors = InspectedMyButton.colors;
			InspectedColors.normalColor = UnselectedColor;
			InspectedMyButton.colors = InspectedColors;
		}
	}
	
	public void HideAllOptionPanels()
	{
		GameObject[] AllOptionPanels;
		AllOptionPanels = GameObject.FindGameObjectsWithTag("PanelOption");
		foreach(GameObject InspectedPanel in AllOptionPanels)
		{
			InspectedPanel.active=false;
		}	
	}
	
	public void SetColorActive(GameObject PressedButton)
	{
		var MyButton=PressedButton.GetComponent<Button>();
		var colors = MyButton.colors;
        colors.normalColor = SelectedColor;
        MyButton.colors = colors;
	}
	
	public void ShowCurrentOptionPanel()
	{
		if (ActiveButton==ButtonEchelle) {PanelOptionButtonEchelle.active=true;}
		if (ActiveButton==ButtonRotation) {PanelOptionButtonRotation.active=true;}
		if (ActiveButton==ButtonDisque1) {PanelOptionButtonDisque1.active=true;}
		if (ActiveButton==ButtonDisque2) {PanelOptionButtonDisque2.active=true;}
		if (ActiveButton==ButtonDisque3) {PanelOptionButtonDisque3.active=true;}
		if (ActiveButton==ButtonCrayon) {PanelOptionButtonCrayonX.active=true; PanelOptionButtonCrayonY.active=true;}
		if (ActiveButton==ButtonDuplication) {PanelOptionButtonDuplication.active=true;}
	}
	
	public void SetActiveEvent()
	{
		MainDragEvent.RemoveAllListeners();
		FirstDragEvent.RemoveAllListeners();
		if (ActiveButton==ButtonEchelle) {FirstDragEvent.AddListener(BeginAjusteEchelleWithDrag); MainDragEvent.AddListener(AjusteEchelleWithDrag);}
		if (ActiveButton==ButtonRotation) {FirstDragEvent.AddListener(BeginAjusteRotationWithDrag); MainDragEvent.AddListener(AjusteRotationWithDrag);}
		if (ActiveButton==ButtonDisque1) {FirstDragEvent.AddListener(BeginAjusteDisque1WithDrag); MainDragEvent.AddListener(AjusteDisque1WithDrag);}
		if (ActiveButton==ButtonDisque2) {FirstDragEvent.AddListener(BeginAjusteDisque2WithDrag); MainDragEvent.AddListener(AjusteDisque2WithDrag);}
		if (ActiveButton==ButtonDisque3) {FirstDragEvent.AddListener(BeginAjusteDisque3WithDrag); MainDragEvent.AddListener(AjusteDisque3WithDrag);}
		if (ActiveButton==ButtonCrayon) {FirstDragEvent.AddListener(BeginAjusteCXYWithDrag); MainDragEvent.AddListener(AjusteCXYWithDrag);}
	}
	
	////////////////////////////////////////////////////////////////////////////  Events  ////////////////////////////////
	
	public void EventR1()
	{
		Debug.Log("EventR1");
	}
	
	public void EventR2()
	{
		Debug.Log("EventR2");
	}
	
	public void CliqueSurfaceDeTravail()
	{
		Debug.Log(DeltaMousePos);
	}
	
	public void BeginAjusteEchelleWithDrag()
	{
		CurrentEchelle = SelectedLine.Echelle;
	}
	
	public void AjusteEchelleWithDrag()
	{
		SelectedLine.Echelle = CurrentEchelle + DeltaMousePos.x/100.0f;
		InputEchelle.GetComponent<InputField>().text = SelectedLine.Echelle.ToString();
	}
	
	public void BeginAjusteRotationWithDrag()
	{
		CurrentRotation = SelectedLine.Rotation;
	}
	
	public void AjusteRotationWithDrag()
	{
		SelectedLine.Rotation = CurrentRotation + DeltaMousePos.x/10.0f;
		InputRotation.GetComponent<InputField>().text = SelectedLine.Rotation.ToString();
	}
	
	public void BeginAjusteDisque1WithDrag()
	{
		CurrentSizeDisque1 = SelectedLine.R1;
	}
	
	public void AjusteDisque1WithDrag()
	{
		SelectedLine.R1 = (float)Math.Floor(10.0f*(CurrentSizeDisque1 + DeltaMousePos.x/100.0f))/10.0f;
		InputR1.GetComponent<InputField>().text = SelectedLine.R1.ToString();
	}
	
	public void BeginAjusteDisque2WithDrag()
	{
		CurrentSizeDisque2 = SelectedLine.R2;
	}
	
	public void AjusteDisque2WithDrag()
	{
		SelectedLine.R2 = (float)Math.Floor(10.0f*(CurrentSizeDisque2 + DeltaMousePos.x/100.0f))/10.0f;
		InputR2.GetComponent<InputField>().text = SelectedLine.R2.ToString();
	}
		
	public void BeginAjusteDisque3WithDrag()
	{
		CurrentSizeDisque3 = SelectedLine.R3;
	}
	
	public void AjusteDisque3WithDrag()
	{
		SelectedLine.R3 = (float)Math.Floor(10.0f*(CurrentSizeDisque3 + DeltaMousePos.x/100.0f))/10.0f;
		InputR3.GetComponent<InputField>().text = SelectedLine.R3.ToString();
	}
		
	public void BeginAjusteCXYWithDrag()
	{
		CurrentSizeCX = SelectedLine.CX;
		CurrentSizeCY = SelectedLine.CY;
	}
	
	public void AjusteCXYWithDrag()
	{
		SelectedLine.CX = (float)Math.Floor(10.0f*(CurrentSizeCX + DeltaMousePos.x/100.0f))/10.0f;
		SelectedLine.CY = (float)Math.Floor(10.0f*(CurrentSizeCY + DeltaMousePos.y/100.0f))/10.0f;
		InputCX.GetComponent<InputField>().text = SelectedLine.CX.ToString();
		InputCY.GetComponent<InputField>().text = SelectedLine.CY.ToString();
	}
			
	public void BeginAjusteCYWithDrag()
	{
		CurrentSizeCY = SelectedLine.CY;
	}
	
	public void AjusteCYWithDrag()
	{
		
		
	}
}
