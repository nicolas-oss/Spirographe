using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DeleteLineButton : MonoBehaviour
{
	public SpiroFormule SelectedLine;
	public Color selected = Color.black;
	public Color unselected = Color.gray;
	Text NameLine;
	public GameObject PreviousLine,ActiveObjectInScene;
	
	public void GetActiveLine()
	{
		ActiveObjectInScene = Spirographe.GetActiveObject();
		SelectedLine = Spirographe.GetActiveSpiroFormule();
	}
	
	public void DeleteLine(GameObject TextNameLineToDelete)
	{
		int NbChildren,LineToActivate;
		GameObject NewTextNameLine;
		bool isSelected;
		
		GetActiveLine();
		
		NbChildren = GameObject.Find("ListSpiro").transform.childCount;
		if (NbChildren>1)
		{
			NameLine = TextNameLineToDelete.GetComponent<Text>();
			isSelected=(NameLine.color==selected);
			if (TextNameLineToDelete.transform.GetSiblingIndex()==(NbChildren-1))
			{
				LineToActivate = NbChildren-2;
			}
			else
			{
				LineToActivate = NbChildren-1;
			}
			SelectedLine=GameObject.Find(GameObject.Find("ListSpiro").transform.GetChild(NbChildren-1).gameObject.GetComponent<Text>().text).GetComponent<SpiroFormule>();
			Destroy(GameObject.Find(NameLine.text));
			Destroy(TextNameLineToDelete);
			NewTextNameLine = GameObject.Find("ListSpiro").transform.GetChild(LineToActivate).gameObject;
			//SelectLine(NewTextNameLine);
			if (isSelected) NewTextNameLine.transform.Find("SelectButton").gameObject.GetComponent<SelectButton>().SelectLine();
		}
	}
}
