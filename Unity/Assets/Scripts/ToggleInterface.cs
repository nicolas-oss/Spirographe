using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInterface : Spirographe
{
	GameObject ActiveObjectInScene;
	SpiroFormule SelectedLine;
	Toggle m_Toggle;
	public string InputID;
	
	void Start()
	{
		GetActiveLine();
        m_Toggle = GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener(delegate {ToggleValueChanged();});
		Spirographe.onRefreshInputField += RefreshContent;
	}

    public void GetActiveLine()
	{
		ActiveObjectInScene = GetActiveObject();
		SelectedLine = GetActiveSpiroFormule();
	}
	
	public void ClicFormTextBouton()
	{
		bool Check = GetComponent<Toggle>().isOn;	
		Check=!Check;
		GetComponent<Toggle>().isOn=Check;
		ToggleValueChanged();
	}
	
	public void ToggleValueChanged()
    {
		GetActiveLine();
		bool Check = GetComponent<Toggle>().isOn;
		SelectedLine.GetType().GetField(InputID).SetValue(SelectedLine,Check);
    }
	
	public void RefreshContent()
	{
		bool Check;
		GetActiveLine();
		Check = (bool)SelectedLine.GetType().GetField(InputID).GetValue(SelectedLine);
		GetComponent<Toggle>().isOn = Check;
	}
}