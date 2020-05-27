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
	}

    public void GetActiveLine()
	{
		ActiveObjectInScene = GameObject.FindWithTag("Selected");
		SelectedLine = ActiveObjectInScene.GetComponent<SpiroFormule>();
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
		GetActiveLine();
		bool Check = GetComponent<Toggle>().isOn;
		SelectedLine.GetType().GetField(InputID).SetValue(SelectedLine,Check);
    }
}