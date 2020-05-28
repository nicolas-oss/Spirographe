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
	public SpiroParametrable SelectedSpiroParam;
	
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
		Debug.Log(ActiveObjectInScene.name);
		SelectedSpiroParam=GetActiveObject().GetComponent<SpiroParametrable>();
	}
	
	public void ToggleValueChanged()
    {
		GetActiveLine();
		bool Check = GetComponent<Toggle>().isOn;
		SelectedSpiroParam.RotAxe[index]=Check;
	}
}