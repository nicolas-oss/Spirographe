using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInterfaceMultiSpiro : MonoBehaviour
{
	GameObject ActiveObjectInScene;
	MultiSpiro SelectedMultiSpiro;
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
		SelectedMultiSpiro = Spirographe.GetActiveMultiSpiro();
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
		SelectedMultiSpiro.GetType().GetField(InputID).SetValue(SelectedMultiSpiro,Check);
		Spirographe.ValueChange(); //Call ValueChange Event
    }
	
	public void RefreshContent()
	{
		bool Check;
		GetActiveLine();
		if (SelectedMultiSpiro==null)
		{
			GetComponent<Toggle>().interactable = false;
		}
		else
		{
			GetComponent<Toggle>().interactable = true;
			Check = (bool)SelectedMultiSpiro.GetType().GetField(InputID).GetValue(SelectedMultiSpiro);
			GetComponent<Toggle>().isOn = Check;
		}
	}
}