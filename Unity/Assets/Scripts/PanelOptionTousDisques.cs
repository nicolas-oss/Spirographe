using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOptionTousDisques : MonoBehaviour
{
    public GameObject LigneSpiro,TextNumeroDisque,IFR,IFA,IFV,IFP,IFF,ToggleActiveDisque,ToggleAnimation,PanelLignes;
	SpiroFormule SelectedLine;
	GameObject SpiroParametrableActive;
	
	void Start()
	{
		Spirographe.onInitialisation += BuildPanel;
		//Spirographe.onRefreshInputField += BuildPanel;
		//Spirographe.onRefreshInputFieldPanelDisques += RefreshPanel;
		//Spirographe.onSelectionLine += BuildPanel;		//on souscrit à l'event onSelection
	}
	
	void ResetPanel()
	{
		foreach (Transform child in PanelLignes.transform)
		{
			Destroy(child.gameObject);
			Debug.Log("Line big panel Destroyed");
		}
	}
	
	/*public void BuildPanel()
	{
		ResetPanel();
		SpiroParametrableActive=GetActiveObject();
		SelectedLine=GetActiveSpiroFormule();
		int profondeur = SelectedLine.profondeur;
		for (int i=0;i<profondeur;i++)
		{
			TextNumeroDisque.GetComponent<Text>().text=(i+1).ToString();
			IFR.GetComponent<InputField>().GetComponent<InputFieldRayon>().index=i;
			IFR.GetComponent<InputField>().GetComponent<InputFieldRayon>().RefreshContent();
			IFR.GetComponent<InputField>().GetComponent<InputFieldRayon>().Start();
			IFA.GetComponent<InputField>().GetComponent<InputFieldAmplitude>().index=i;
			IFA.GetComponent<InputField>().GetComponent<InputFieldAmplitude>().RefreshContent();
			IFV.GetComponent<InputField>().GetComponent<InputFieldVitesse>().index=i;
			IFV.GetComponent<InputField>().GetComponent<InputFieldVitesse>().RefreshContent();
			IFP.GetComponent<InputField>().GetComponent<InputFieldPhase>().index=i;
			IFP.GetComponent<InputField>().GetComponent<InputFieldPhase>().RefreshContent();
			IFF.GetComponent<InputField>().GetComponent<InputFieldFacteur>().index=i;
			IFF.GetComponent<InputField>().GetComponent<InputFieldFacteur>().RefreshContent();
			//Debug.Log(ToggleActiveDisque.name);//GetComponent<Toggle>().GetComponent<ToggleAnimRayon>().index.ToString);
			/*ToggleActiveDisque.GetComponent<Toggle>().GetComponent<ToggleAnimRayon>().index=i;
			ToggleAnimation.GetComponent<Toggle>().GetComponent<ToggleRotAxe>().index=i;*/
			/*GameObject NewLine=Instantiate(LigneSpiro);
			NewLine.name="Ligne"+(i).ToString();
			NewLine.SetActive(true);
			NewLine.transform.SetParent(PanelLignes.transform,false);
		}
	}*/
	
	public void BuildPanel()
	{
		Debug.Log("beginning Building Panel big");
		GameObject LineCurr;
		SpiroParametrableActive=Spirographe.GetActiveObject();
		SelectedLine=Spirographe.GetActiveSpiroFormule();
		int profondeur = SelectedLine.profondeur;
		int NbLignesCrees = PanelLignes.transform.childCount;
		Debug.Log("NbLignesCrees="+NbLignesCrees.ToString());
		Debug.Log("profondeur="+profondeur.ToString());
		for (int i=0;i<profondeur;i++)
		{
			if (i<NbLignesCrees)
			{
				LineCurr = PanelLignes.transform.GetChild(i).gameObject;
				LineCurr.SetActive(true);
				Debug.Log("Ligne affichée");
			}
			else
			{
				TextNumeroDisque.GetComponent<Text>().text=(i+1).ToString();
				InitField(ref IFR,i);
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
				Debug.Log("Ligne Panel big copiée");
			}
		}
		Debug.Log("Builded Panel big");
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
