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
	
	public UnityEvent MainEvent;
	public GameObject ButtonEchelle,ButtonRotation,ButtonDisque1,ButtonDisque2,ButtonDisque3,ButtonCrayon;
	public GameObject PanelOptionButtonEchelle,PanelOptionButtonRotation,PanelOptionButtonDisque1,PanelOptionButtonDisque2,PanelOptionButtonDisque3,PanelOptionButtonCrayonX,PanelOptionButtonCrayonY;
	public GameObject Surface;
	public GameObject InputEchelle,InputRotation;
	GameObject ActiveButton;
	//public GameObject PreviousSelectedButton;
	
    void Start()
    {
		//Création évenement principal et ajout listener
		if (MainEvent == null) 
		{
			MainEvent = new UnityEvent();
		}
		//MainEvent.AddListener(CliqueSurfaceDeTravail);
    }

    void Update()
    {
        if (Input.anyKeyDown && MainEvent != null)
        {
            //MainEvent.Invoke();
        }
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
		//SetActiveEvent();
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
		if (ActiveButton==ButtonEchelle) {PanelOptionButtonEchelle.active=true;}
		if (ActiveButton==ButtonRotation) {PanelOptionButtonRotation.active=true;}
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
		RaycastHit  hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit)) 
		{
			Debug.Log(hit.transform.name);
        }
		if (ActiveButton == ButtonDisque1) {}
	}
}
