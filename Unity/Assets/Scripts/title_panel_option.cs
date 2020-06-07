using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class title_panel_option : MonoBehaviour
{
    public GameObject PanelOption;

	public void HidePanel()
	{
		bool activation;
		activation=PanelOption.active;
		//Spirographe.RefreshInputField();
		PanelOption.SetActive(!activation);
		Spirographe.RefreshInputField();
		//transform.parent.GetComponent<RectTransform>().ForceUpdateRectTransforms(); //force refresh
		//transform.Translate(Vector3.up);											//force refresh
	}
	
	public void ShowPanel()
	{
		PanelOption.SetActive(true);
	}
}
