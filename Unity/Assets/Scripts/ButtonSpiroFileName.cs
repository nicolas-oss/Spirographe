using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpiroFileName : MonoBehaviour
{
	public GameObject PanelFileBrowser;
	
	void onEnable()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(SelectFile); 
	}
	
	void onDisable()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.RemoveListener(SelectFile); 
	}
	
	public void SelectFile()
	{
		string NameSelectedFile;
		NameSelectedFile=transform.GetChild(0).gameObject.GetComponent<Text>().text;
		PanelFileBrowser.GetComponent<FileBrowserPanel>().SelectionLigne(NameSelectedFile);
		//Debug.Log(NameSelectedFile+" émis in Bouton");
	}
}
