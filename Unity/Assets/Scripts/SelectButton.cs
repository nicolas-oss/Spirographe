using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SelectButton : Spirographe
{
	public SpiroFormule SelectedLine;
	public GameObject ActiveObjectInScene;
	public GameObject TextNameLine;
	public Color selected = Color.black;
	public Color unselected = Color.gray;
	public Text NameLine;
	public GameObject PreviousTextLine;
	
	void Start()
	{
		Spirographe.onSelectionLine += SelectLine;		
	}
	
	public void GetActiveLine()
	{
		ActiveObjectInScene = GetActiveObject();
		SelectedLine = GetActiveSpiroFormule();
	}
	
	public void GetActiveTextLine() //recherche de la ligne active en scannant les enfants du GameObject contenant toutes les lignes
	{
		GameObject RootList;
		RootList = transform.parent.parent.gameObject;
		for (int j=0; j<RootList.transform.childCount; j++)
		{
			if (RootList.transform.GetChild(j).GetComponent<Text>().color == selected)
			{
				PreviousTextLine=RootList.transform.GetChild(j).gameObject;
				break;
			}
		}
	}
	
	public void SelectLine()
	{
		GetActiveLine();
		GetActiveTextLine();
		TextNameLine=transform.parent.gameObject;
		//if (PreviousTextLine==null) {PreviousTextLine=TextNameLine;}
		NameLine = TextNameLine.GetComponent<Text>();
		ActiveObjectInScene.tag="Untagged";
		PreviousTextLine.GetComponent<Text>().color = unselected;
		TextNameLine.GetComponent<Text>().color = selected;
		ActiveObjectInScene = GameObject.Find(NameLine.text);
		ActiveObjectInScene.tag="Selected";
		PreviousTextLine = TextNameLine;
		//SelectionLine();
		RefreshInputField();
	}
}
