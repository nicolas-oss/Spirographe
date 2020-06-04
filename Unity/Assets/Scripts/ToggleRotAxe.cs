using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleRotAxe : MonoBehaviour
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
		Spirographe.onRefreshInputField += RefreshContent;
		Spirographe.onRefreshInputFieldPanelDisques += RefreshContent;
		//Spirographe.onSelectionLine += RefreshContent;
	}

    public void GetActiveLine()
	{
		ActiveObjectInScene = Spirographe.GetActiveObject();
		SelectedLine = Spirographe.GetActiveSpiroFormule();
	}
	
	public void ToggleValueChanged()
    {
		GetActiveLine();
		bool Check = GetComponent<Toggle>().isOn;
		SelectedLine.RotAxe[index]=Check;
		Spirographe.RefreshInputFieldPanelDisques();
	}
	
	public void RefreshContent()
	{
		bool Check;
		GetActiveLine();
		Check = SelectedLine.RotAxe[index];
		GetComponent<Toggle>().isOn = Check;
	}
}