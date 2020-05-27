using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonInterface : MonoBehaviour
{
    public GameObject OptionPanel;
	public Color UnselectedColor,SelectedColor;

    void Start()
    {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick); 
    }
	
	void TaskOnClick()
	{
		SelectButton(gameObject);
	}
	
	public void SelectButton(GameObject PressedButton)
	{
		DeselectAllButton();
		HideAllOptionPanels();
		SetColorActive();
		//ActiveButton = PressedButton;
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
	
	public void SetColorActive()
	{
		var MyButton=GetComponent<Button>();
		var colors = MyButton.colors;
        colors.normalColor = SelectedColor;
        MyButton.colors = colors;
	}
	
	public void ShowCurrentOptionPanel()
	{
		OptionPanel.active=true;
	}
	
	public void SetActiveEvent()
	{
		/*MainDragEvent.RemoveAllListeners();
		FirstDragEvent.RemoveAllListeners();
		if (ActiveButton==ButtonEchelle) {FirstDragEvent.AddListener(BeginAjusteEchelleWithDrag); MainDragEvent.AddListener(AjusteEchelleWithDrag);}
		if (ActiveButton==ButtonRotation) {FirstDragEvent.AddListener(BeginAjusteRotationWithDrag); MainDragEvent.AddListener(AjusteRotationWithDrag);}
		if (ActiveButton==ButtonDisque1) {FirstDragEvent.AddListener(BeginAjusteDisque1WithDrag); MainDragEvent.AddListener(AjusteDisque1WithDrag);}
		if (ActiveButton==ButtonDisque2) {FirstDragEvent.AddListener(BeginAjusteDisque2WithDrag); MainDragEvent.AddListener(AjusteDisque2WithDrag);}
		if (ActiveButton==ButtonDisque3) {FirstDragEvent.AddListener(BeginAjusteDisque3WithDrag); MainDragEvent.AddListener(AjusteDisque3WithDrag);}
		if (ActiveButton==ButtonCrayon) {FirstDragEvent.AddListener(BeginAjusteCXYWithDrag); MainDragEvent.AddListener(AjusteCXYWithDrag);}*/
	}
}
