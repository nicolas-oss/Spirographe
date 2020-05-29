using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleRotAxe : Spirographe
{
    public int index;
	GameObject ActiveObjectInScene;
	SpiroFormule SelectedLine;
	Toggle m_Toggle;
	
	void Start()
	{
		GetActiveLine();
        m_Toggle = GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener(delegate {ToggleValueChanged();});
	}

    public void GetActiveLine()
	{
		ActiveObjectInScene = GetActiveObject();
		SelectedLine = GetActiveSpiroFormule();
	}
	
	public void ToggleValueChanged()
    {
		GetActiveLine();
		bool Check = GetComponent<Toggle>().isOn;
		SelectedLine.RotAxe[index]=Check;
	}
}