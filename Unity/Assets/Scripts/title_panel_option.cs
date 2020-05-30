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
		PanelOption.SetActive(!activation);
	}
}
