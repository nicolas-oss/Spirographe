﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour
{
	public SpiroFormule SelectedLine;
	public GameObject ActiveObjectInScene;
	public GameObject TextNameLine;
	public Color unselectedpublic;
	public static Color selected = Color.black;
	public static Color unselected = new Color32(76,76,76,255);
	public Text NameLine;
	public GameObject PreviousTextLine;
	
	void Start()
	{
		//Spirographe.onSelectionLine += SelectLine;		
	}
	
	public void GetActiveLine()
	{
		ActiveObjectInScene = Spirographe.GetActiveObject();
		SelectedLine = Spirographe.GetActiveSpiroFormule();
	}
	
	public void GetActiveTextLine() //recherche de la ligne active en scannant les enfants du GameObject contenant toutes les lignes
	{
		GameObject RootList;
		RootList = transform.parent.parent.gameObject;
		for (int j=0; j<RootList.transform.childCount; j++)
		{
			if (RootList.transform.GetChild(j).transform.Find("TextName").GetComponent<Text>().color == selected)
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
		if (PreviousTextLine==null) {PreviousTextLine=TextNameLine;}
		NameLine = TextNameLine.transform.Find("TextName").GetComponent<Text>();
		ActiveObjectInScene.tag="Untagged";
		PreviousTextLine.transform.Find("TextName").GetComponent<Text>().color = unselected;
		TextNameLine.transform.Find("TextName").GetComponent<Text>().color = selected;
		ActiveObjectInScene = GameObject.Find(NameLine.text);
		ActiveObjectInScene.tag="Selected";
		PreviousTextLine = TextNameLine;
		Spirographe.Selection(); //call Selection Event
		Spirographe.RefreshInputField();
		EventSystem.current.SetSelectedGameObject(null); //deselect button
	}
}
