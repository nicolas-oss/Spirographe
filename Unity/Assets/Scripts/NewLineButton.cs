using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewLineButton : MonoBehaviour
{
	public SpiroFormule SelectedLine;//,SpiroFormule; //CurrentLine,
	public Color selected = Color.black;
	public Color unselected = Color.gray;
	Text NameLine;
	public GameObject ActiveObjectInScene,PreviousTextLine,TextNameLine,NewLineName,NewLine;
	public int LineCount;
	public GameObject RootList;

	public void GetActiveLine()
	{
		ActiveObjectInScene = Spirographe.GetActiveObject();
		SelectedLine = Spirographe.GetActiveSpiroFormule();
	}
	
	public void GetActiveTextLine() //recherche de la ligne active en scannant les enfants du GameObject contenant toutes les lignes
	{
		PreviousTextLine=RootList.transform.GetChild(0).gameObject;
		for (int j=0; j<RootList.transform.childCount; j++)
		{
			if (RootList.transform.GetChild(j).transform.Find("TextName").GetComponent<Text>().color == selected)
			{
				PreviousTextLine=RootList.transform.GetChild(j).gameObject;
				break;
			}
		}
	}
	
	public void NewLineRenderer()
	{
		//bool Visibility;
		//GameObject BoutonDelete;
		
		GetActiveLine();
		NewLine = Instantiate(ActiveObjectInScene);
		NewLine.tag="Untagged";
		GetActiveTextLine();
		NewLineName = Instantiate(PreviousTextLine);
		LineCount++;
		NameLine = NewLineName.transform.Find("TextName").GetComponent<Text>();
		NewLineName.transform.SetParent(PreviousTextLine.transform.parent,false);
		NameLine.text = ("SpiroFormule"+LineCount.ToString());
		NewLine.name = NameLine.text;
		NewLineName.transform.Find("DeleteButton").gameObject.SetActive(true);
		NewLineName.transform.Find("SelectButton").gameObject.GetComponent<SelectButton>().SelectLine();
		Spirographe.RefreshInputField();
	}
}

//NewLineName.transform.SetParent(GameObject.Find("ListSpiro").transform,false);
