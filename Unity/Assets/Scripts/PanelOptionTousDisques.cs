using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOptionTousDisques : Spirographe
{
    public GameObject LigneSpiro,TextNumeroDisque,IFR,IFA,IFV,IFP,IFF,ToggleActiveDisque,ToggleAnimation,PanelLignes;
	SpiroFormule SelectedLine;
	GameObject SpiroParametrableActive;
	
	void Start()
	{
		Spirographe.onRefreshInputField += BuildPanel;
	}
	
	void ResetPanel()
	{
		foreach (Transform child in PanelLignes.transform)
		{
			Destroy(child.gameObject);
			Debug.Log("Line Destroyed");
		}
	}
	
	public void BuildPanel()
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
			GameObject NewLine=Instantiate(LigneSpiro);
			NewLine.name="Ligne"+(i).ToString();
			NewLine.SetActive(true);
			NewLine.transform.SetParent(PanelLignes.transform,false);
		}
	}
	
	public void RefreshPanel()
	{
		InputFieldRayon[] ZOB;
		
        ZOB = GetComponentsInChildren<InputFieldRayon>();
        foreach (InputFieldRayon IF in ZOB)
		{
            IF.gameObject.GetComponent<InputFieldRayon>().RefreshContent();
			//ZOB.RefreshContent();
		}
    }
		
		
		
		/*SpiroParametrableActive=GetActiveObject();
		SelectedLine=GetActiveSpiroFormule();
		
		
		
		
		int profondeur = SelectedLine.profondeur;
		for (int i=0;i<profondeur;i++)
		{
			//TextNumeroDisque.GetComponent<Text>().text=(i+1).ToString();
			IFR.GetComponent<InputField>().GetComponent<InputFieldRayon>().index=i;
			IFR.GetComponent<InputField>().GetComponent<InputFieldRayon>().RefreshContent();
			IFA.GetComponent<InputField>().GetComponent<InputFieldAmplitude>().index=i;
			IFA.GetComponent<InputField>().GetComponent<InputFieldAmplitude>().RefreshContent();
			IFV.GetComponent<InputField>().GetComponent<InputFieldVitesse>().index=i;
			IFV.GetComponent<InputField>().GetComponent<InputFieldVitesse>().RefreshContent();
			IFP.GetComponent<InputField>().GetComponent<InputFieldPhase>().index=i;
			IFP.GetComponent<InputField>().GetComponent<InputFieldPhase>().RefreshContent();
			IFF.GetComponent<InputField>().GetComponent<InputFieldFacteur>().index=i;
			IFF.GetComponent<InputField>().GetComponent<InputFieldFacteur>().RefreshContent();
			GameObject NewLine=Instantiate(LigneSpiro);
			NewLine.name="Ligne"+(i).ToString();
			NewLine.SetActive(true);
			NewLine.transform.SetParent(PanelLignes.transform,false);
		}*/
}
