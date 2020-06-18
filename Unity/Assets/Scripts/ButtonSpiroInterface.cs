using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Concerne les boutons liés à la classe SpiroFormule

public class ButtonSpiroInterface : MonoBehaviour
{
	GameObject ActiveObjectInScene;
	SpiroFormule SelectedLine;
	//Button m_Button;
	//public string InputID;
	
	void Start()
	{
		//GetActiveLine();
        //m_Button = GetComponent<Button>();
        //m_Button.onValueChanged.AddListener(delegate {ToggleValueChanged();});
		Spirographe.onRefreshInputField += RefreshContent;
		Spirographe.onRefreshInputFieldPanelDisques += RefreshContent;
		Spirographe.onSelectionLine += RefreshContent;
	}

    public void GetActiveLine()
	{
		ActiveObjectInScene = Spirographe.GetActiveObject();
		SelectedLine = Spirographe.GetActiveSpiroFormule();
	}
	
	public void RefreshContent()
	{
		bool Check;
		GetActiveLine();
		if (SelectedLine==null)
		{
			GetComponent<Button>().interactable = false;
		}
		else
		{
			GetComponent<Button>().interactable = true;
		}
	}
}