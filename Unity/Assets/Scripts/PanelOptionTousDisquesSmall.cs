﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOptionTousDisquesSmall : MonoBehaviour
{
    public GameObject LigneSpiro,TextNumeroDisque,IFR,IFA,IFV,IFP,IFF,ToggleActiveDisque,ToggleAnimation,PanelLignes,PanelOptionTousDisques;
	SpiroFormule SelectedLine;
	GameObject SpiroParametrableActive;
	
	void Start()
	{
		Spirographe.onSelectionLine += BuildPanel;
	}
	
	public void RemoveEventsFromLine(GameObject LigneEnCours)
	{
		GameObject IFREnCours,IFFEnCours;
		//LigneEnCours = PanelLignes.transform.GetChild(0).gameObject;
			IFREnCours =  LigneEnCours.transform.Find(IFR.name).gameObject;
			IFFEnCours =  LigneEnCours.transform.Find(IFF.name).gameObject;
			IFREnCours.GetComponent<InputFieldPanelDisques>().UnsubscribeRefreshEvent();
			IFFEnCours.GetComponent<InputFieldPanelDisques>().UnsubscribeRefreshEvent();
			Debug.Log("Line small panel destroyed - Events unSubscribed");
	}
	
	void ResetPanel()
	{
		foreach (Transform child in PanelLignes.transform)
		{
			Destroy(child.gameObject);
		}
	}
	
	public void BuildPanel()
	{
		GameObject LineCurr; //
		SpiroParametrableActive=Spirographe.GetActiveObject();
		SelectedLine=Spirographe.GetActiveSpiroFormule();
		int profondeur = SelectedLine.profondeur;
		int NbLignesCrees = PanelLignes.transform.childCount;
		for (int i=0;i<profondeur;i++)
		{
			if (i<NbLignesCrees)
			{
				LineCurr = PanelLignes.transform.GetChild(i).gameObject;
				LineCurr.SetActive(true);
			}
			else
			{
				TextNumeroDisque.GetComponent<Text>().text=(i+1).ToString();
				IFR.GetComponent<InputFieldPanelDisques>().index=i;
				IFR.GetComponent<InputFieldPanelDisques>().RefreshContent();
				IFR.GetComponent<InputFieldPanelDisques>().Start();
				IFF.GetComponent<InputFieldPanelDisques>().index=i;
				IFF.GetComponent<InputFieldPanelDisques>().RefreshContent();
				IFF.GetComponent<InputFieldPanelDisques>().Start();
				ToggleActiveDisque.GetComponent<ToggleRotAxe>().index=i;
				ToggleActiveDisque.GetComponent<ToggleRotAxe>().RefreshContent();
				GameObject NewLine=Instantiate(LigneSpiro);
				NewLine.name="Ligne"+(i).ToString();
				NewLine.SetActive(true);
				NewLine.transform.SetParent(PanelLignes.transform,false);
			}
		}
		GetComponent<RectTransform>().ForceUpdateRectTransforms();
		//Debug.Log("Building Panel big");
		//Reste à cacher les lignes restantes éventuelles :
		if (profondeur<=NbLignesCrees)
		{
			for (int i= profondeur;i<NbLignesCrees;i++)
			{
				PanelLignes.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}
	
	public void AddLine()
	{
		int i;
		SelectedLine=Spirographe.GetActiveSpiroFormule();
		SelectedLine.profondeur++;
		i=SelectedLine.profondeur-1; //avant dernière ligne avant le crayon
		TextNumeroDisque.GetComponent<Text>().text=(i+1).ToString();
		int NbLignesCrees = PanelLignes.transform.childCount;
		if (NbLignesCrees<SelectedLine.profondeur)
		{
			IFR.GetComponent<InputField>().GetComponent<InputFieldPanelDisques>().index=i;
			IFR.GetComponent<InputField>().GetComponent<InputFieldPanelDisques>().RefreshContent();
			IFF.GetComponent<InputField>().GetComponent<InputFieldPanelDisques>().index=i;
			IFF.GetComponent<InputField>().GetComponent<InputFieldPanelDisques>().RefreshContent();
			ToggleActiveDisque.GetComponent<ToggleRotAxe>().index=i;
			ToggleActiveDisque.GetComponent<ToggleRotAxe>().RefreshContent();
			GameObject NewLine=Instantiate(LigneSpiro);
			NewLine.name="Ligne"+(i).ToString();
			NewLine.SetActive(true);
			NewLine.transform.SetParent(PanelLignes.transform,false);
		}
		else
		{
			PanelLignes.transform.GetChild(i).gameObject.SetActive(true);
		}
		RefreshPanel();
		PanelOptionTousDisques.GetComponent<PanelOptionTousDisques>().BuildPanel(); //refresh panel tous disques too
	}
	
	public void DeleteLine()
	{
		GameObject LastLine;
		SelectedLine=Spirographe.GetActiveSpiroFormule();
		if (SelectedLine.profondeur>3) 
		{
			SelectedLine.profondeur--;
			LastLine = PanelLignes.transform.GetChild(SelectedLine.profondeur).gameObject;
			//RemoveEventsFromLine(LastLine);
			//Destroy(LastLine);
			LastLine.SetActive(false);
		}
		RefreshPanel();
		PanelOptionTousDisques.GetComponent<PanelOptionTousDisques>().BuildPanel(); //refresh panel tous disques too
	}
	
	public void RefreshPanel()
	{
		SelectedLine=Spirographe.GetActiveSpiroFormule();
		InputFieldPanelDisques[] ZOB;	
        ZOB = PanelLignes.GetComponentsInChildren<InputFieldPanelDisques>(); //Get all InputFieldPanelDisques in PanelLignes
        foreach (InputFieldPanelDisques IF in ZOB)
		{
            IF.gameObject.GetComponent<InputFieldPanelDisques>().RefreshContent();  //RefreshContent of all InputFieldPanelDisques in PanelLignes
		}
		int profondeur = SelectedLine.profondeur;
		int NbLignesCrees = PanelLignes.transform.childCount;
		GameObject LineCurr=PanelLignes.transform.GetChild(0).gameObject;
		for (int i=0;i<NbLignesCrees;i++)									 // on n'affiche que les lignes d'indice inférieur à profondeur
		{
			LineCurr = PanelLignes.transform.GetChild(i).gameObject;
			if (i<profondeur)
			{
				LineCurr.SetActive(true);
			}
			else
			{
				LineCurr.SetActive(false);
			}
		}
    }
	
	public void LateUpdate()
	{
		transform.parent.GetComponent<RectTransform>().ForceUpdateRectTransforms();
		transform.Translate(Vector3.up); //force refresh		
	}
}
