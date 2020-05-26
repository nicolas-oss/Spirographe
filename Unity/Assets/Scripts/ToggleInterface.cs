using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInterface : MonoBehaviour
{
    public GameObject Interface;
	SpiroFormule SelectedLine;
	Toggle m_Toggle;
	public string InputID;
	
	void Start()
	{
		SelectedLine = Interface.GetComponent<Interface>().SelectedLine;
		//Fetch the Toggle GameObject
        m_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate {ToggleValueChanged();});
	}

    public void ClicFormTextBouton()
	{
		bool Check = GetComponent<Toggle>().isOn;	
		Check=!Check;
		GetComponent<Toggle>().isOn=Check;
		ToggleValueChanged();
		Debug.Log(Check);
	}
	
	public void ToggleValueChanged()
    {
		bool Check = GetComponent<Toggle>().isOn;
		SelectedLine.GetType().GetField(InputID).SetValue(SelectedLine,Check);
    }
}