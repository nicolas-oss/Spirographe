using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interface : MonoBehaviour
{
    public Color UnselectedColor,SelectedColor;
	
	public UnityEvent MainEvent;
	
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
		var MyButton=PressedButton.GetComponent<Button>();
		var colors = MyButton.colors;
        colors.normalColor = SelectedColor;
        MyButton.colors = colors;
		Debug.Log("Ayé");
	}
	
	public void DeselectAllButton()
	{
		
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
