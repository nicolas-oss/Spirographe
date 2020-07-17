using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Concerne les boutons liés à la classe SpiroFormule

public class ButtonAspectInterface : MonoBehaviour
{
	void Start()
	{
		Spirographe.onRefreshInputField += RefreshContent;
		Spirographe.onRefreshInputFieldPanelDisques += RefreshContent;
		Spirographe.onSelectionLine += RefreshContent;
	}
	
	public void RefreshContent()
	{
		bool Check;
		if (Spirographe.SelectedAspect==null)
		{
			GetComponent<Button>().interactable = false;
		}
		else
		{
			GetComponent<Button>().interactable = true;
		}
	}
}