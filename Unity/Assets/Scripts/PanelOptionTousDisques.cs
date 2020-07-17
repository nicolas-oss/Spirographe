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
			//SelectBackgroundActivation(i);   			//on sélectionne le background lignes 1 à profondeur-1
			GetBackgroundActivation(i).SetActive(true);	//on l'affiche
		}
		//if (profondeur>0)
		//{
			Debug.Log("in prof<0");
			//GetBackgroundActivation(0);   			//on sélectionne le background ligne 0
			GetBackgroundActivation(0).SetActive(false);	//on le cache
			//ToggleActivationCurr.SetActive(false);	//on le cache
			//SelectBackgroundActivation(profondeur-1);   	//on sélectionne le background ligne profondeur
			GetBackgroundActivation(profondeur-1).SetActive(false);	//on le cache
			//ToggleActivationCurr.SetActive(false);	//on le cache
		//}
	}
	
	GameObject GetBackgroundActivation(int i)
	{
		GameObject LC,TAC,BAC;
		LC = PanelLignes.transform.GetChild(i).gameObject;
		TAC = LC.transform.Find("ToggleActivation").gameObject;
		//Debug.Log("ToggleAct="+TAC.name);
		BAC = TAC.transform.Find("Background").gameObject;
		//Debug.Log("BackgroundAct="+BAC.name);
		//BAC.SetActive(false);
		return BAC;
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
