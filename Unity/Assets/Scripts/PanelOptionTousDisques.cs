using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOptionTousDisques : MonoBehaviour
{
    public GameObject LigneSpiro,TextNumeroDisque,IFR,IFRO,IFA,IFV,IFP,IFF,ToggleActiveDisque,ToggleAnimation,PanelLignes;
	SpiroFormule SelectedLine;
	GameObject SpiroParametrableActive;
	GameObject LineCurr,ToggleActivationCurr,BackgroundActivationCurr;
	
	void Start()
	{
		Spirographe.onSelectionLine += BuildPanel;
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
		int profondeur;
		SpiroParametrableActive=Spirographe.GetActiveObject();
		SelectedLine=Spirographe.GetActiveSpiroFormule();
		if (SelectedLine==null) {profondeur=0;} else {profondeur = SelectedLine.profondeur;}
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
				InitField(ref IFR,i);
				InitField(ref IFRO,i);
				InitField(ref IFA,i);
				InitField(ref IFV,i);
				InitField(ref IFP,i);
				InitField(ref IFF,i);
				ToggleAnimation.GetComponent<ToggleAnimRayon>().index=i;
				ToggleAnimation.GetComponent<ToggleAnimRayon>().RefreshContent();
				ToggleActiveDisque.GetComponent<ToggleRotAxe>().index=i;
				ToggleActiveDisque.GetComponent<ToggleRotAxe>().RefreshContent();
				GameObject NewLine=Instantiate(LigneSpiro);
				NewLine.name="Ligne"+(i).ToString();
				NewLine.SetActive(true);
				NewLine.transform.SetParent(PanelLignes.transform,false);
			}
		}
		//Reste à cacher les lignes restantes éventuelles :
		if (profondeur<=NbLignesCrees)
		{
			for (int i= profondeur;i<NbLignesCrees;i++)
			{
				PanelLignes.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
		
		//Gestion affichage des coches RotAxe
		
		for (int i=1;i<profondeur-1;i++)
		{
			GetBackgroundActivation(i).SetActive(true);	//on l'affiche
		}
			GetBackgroundActivation(0).SetActive(false);	//on le cache
			GetBackgroundActivation(profondeur-1).SetActive(false);	//on le cache
			
		RefreshFacteurTransmission();
	}
	
	GameObject GetBackgroundActivation(int i)
	{
		GameObject LC,TAC,BAC;
		LC = PanelLignes.transform.GetChild(i).gameObject;
		TAC = LC.transform.Find("ToggleActivation").gameObject;
		BAC = TAC.transform.Find("Background").gameObject;
		return BAC;
	}
	
	public void RefreshFacteurTransmission()
	{
			GameObject IFtoModify;
			for (int i=1;i<Spirographe.SelectedLine.profondeur-1;i++)
			{
				IFtoModify = GetInputFieldTransmission(i);
				IFtoModify.GetComponent<InputField>().interactable = true;	//on le cache
				IFtoModify.GetComponent<InputFieldPanelDisques>().activation = true;
			}
				IFtoModify = GetInputFieldTransmission(0);
				IFtoModify.GetComponent<InputField>().interactable = false;	//on le cache
				IFtoModify.GetComponent<InputFieldPanelDisques>().activation = false;
				IFtoModify = GetInputFieldTransmission(Spirographe.SelectedLine.profondeur-1);
				IFtoModify.GetComponent<InputField>().interactable = false;	//on le cache
				IFtoModify.GetComponent<InputFieldPanelDisques>().activation = false;
	}
	
	GameObject GetInputFieldTransmission(int i)
	{
		GameObject LC,IFFT,BAC;
		LC = PanelLignes.transform.GetChild(i).gameObject;
		IFFT = LC.transform.Find("InputField_Transmission").gameObject;
		//BAC = TAC.transform.Find("Background").gameObject;
		return IFFT;
	}

	public void InitField(ref GameObject IFX,int n)
	{
		IFX.GetComponent<InputFieldPanelDisques>().index=n;
		IFX.GetComponent<InputFieldPanelDisques>().RefreshContent();
		IFX.GetComponent<InputFieldPanelDisques>().Start();
	}
	
	public void RefreshPanel()
	{
		InputField[] ZOB;
		
        ZOB = GetComponentsInChildren<InputField>();
        foreach (InputField IF in ZOB)
		{
            IF.gameObject.GetComponent<InputFieldPanelDisques>().RefreshContent();
		}
    }
}
