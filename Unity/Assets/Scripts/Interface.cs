using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Interface : MonoBehaviour
{
    public SpiroFormule SelectedLine;
	
	public Color UnselectedColor,SelectedColor;
	
	public UnityEvent MainDragEvent,FirstDragEvent;
	public GameObject ButtonEchelle,ButtonRotation,ButtonDisque1,ButtonDisque2,ButtonDisque3,ButtonCrayon;
	public GameObject PanelOptionButtonEchelle,PanelOptionButtonRotation,PanelOptionButtonDisque1,PanelOptionButtonDisque2,PanelOptionButtonDisque3,PanelOptionButtonCrayonX,PanelOptionButtonCrayonY;
	public GameObject Surface;
	public GameObject InputEchelle,InputRotation;
	public Vector3 DeltaMousePos;
	GameObject ActiveButton;
	//public GameObject PreviousSelectedButton;
	public float CurrentEchelle,CurrentRotation;
	
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
	}
	
	public void SetActiveEvent()
	{
		MainDragEvent.RemoveAllListeners();
		FirstDragEvent.RemoveAllListeners();
		if (ActiveButton==ButtonEchelle) {FirstDragEvent.AddListener(BeginAjusteEchelleWithDrag); MainDragEvent.AddListener(AjusteEchelleWithDrag);}
		if (ActiveButton==ButtonRotation) {FirstDragEvent.AddListener(BeginAjusteRotationWithDrag); MainDragEvent.AddListener(AjusteRotationWithDrag);}
		if (ActiveButton==ButtonDisque1) {PanelOptionButtonDisque1.active=true;}
		if (ActiveButton==ButtonDisque2) {PanelOptionButtonDisque2.active=true;}
		if (ActiveButton==ButtonDisque3) {PanelOptionButtonDisque3.active=true;}
		if (ActiveButton==ButtonCrayon) {PanelOptionButtonCrayonX.active=true; PanelOptionButtonCrayonY.active=true;}
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
}
