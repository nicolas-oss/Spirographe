using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInterface : MonoBehaviour
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
		Spirographe.onRefreshInputFieldPanelDisques += RefreshContent;
		//Spirographe.onSelectionLine += RefreshContent;
	}

    public void GetActiveLine()
	{
		ActiveObjectInScene = Spirographe.GetActiveObject();
		SelectedLine = Spirographe.GetActiveSpiroFormule();
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
		Spirographe.ValueChange(); //Call ValueChange Event
    }
	
	public void RefreshContent()
	{
		bool Check;
		GetActiveLine();
		if (SelectedLine==null)
		{
			GetComponent<Toggle>().interactable = false;
		}
		else
		{
			GetComponent<Toggle>().interactable = true;
			Check = (bool)SelectedLine.GetType().GetField(InputID).GetValue(SelectedLine);
			GetComponent<Toggle>().isOn = Check;
		}
	}
}