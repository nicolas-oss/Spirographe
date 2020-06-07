using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOptionTousDisques : MonoBehaviour
{
    public GameObject LigneSpiro,TextNumeroDisque,IFR,IFRO,IFA,IFV,IFP,IFF,ToggleActiveDisque,ToggleAnimation,PanelLignes;
	SpiroFormule SelectedLine;
	GameObject SpiroParametrableActive;
	
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
		GameObject LineCurr;
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
