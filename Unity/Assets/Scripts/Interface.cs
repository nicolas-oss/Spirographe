using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interface : MonoBehaviour
{
    public Color UnselectedColor,SelectedColor;
	
	public UnityEvent MainEvent;
	public GameObject ButtonDisque1,ButtonDisque2,ButtonDisque3;
	//public GameObject PreviousSelectedButton;
	
    void Start()
    {
		//Création évenement principal et ajout listener
		if (MainEvent == null) 
		{
			MainEvent = new UnityEvent();
		}
		MainEvent.AddListener(EventR1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && MainEvent != null)
        {
            MainEvent.Invoke();
        }
    }
	
	public void SelectButton(GameObject PressedButton)
	{
		DeselectAllButton();
		SetColorActive(PressedButton);
		if (PressedButton==ButtonDisque1)
		{
				Debug.Log("ButtonDisque1");
		}
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
	
	public void SetColorActive(GameObject PressedButton)
	{
		var MyButton=PressedButton.GetComponent<Button>();
		var colors = MyButton.colors;
        colors.normalColor = SelectedColor;
        MyButton.colors = colors;
	}
	
	public void EventR1()
	{
		Debug.Log("EventR1");
	}
	
	public void EventR2()
	{
		Debug.Log("EventR2");
	}
}
