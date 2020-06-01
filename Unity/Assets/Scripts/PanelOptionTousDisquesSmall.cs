using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOptionTousDisquesSmall : Spirographe
{
    public GameObject LigneSpiro,TextNumeroDisque,IFR,IFA,IFV,IFP,IFF,ToggleActiveDisque,ToggleAnimation,PanelLignes,PanelOptionTousDisques;
	SpiroFormule SelectedLine;
	GameObject SpiroParametrableActive;
	
	void Start()
	{
		Spirographe.onRefreshInputField += BuildPanel;
	}
	
	public void RemoveEventsFromLine(GameObject LigneEnCours)
	{
		GameObject IFREnCours,IFFEnCours;
		//LigneEnCours = PanelLignes.transform.GetChild(0).gameObject;
			IFREnCours =  LigneEnCours.transform.Find(IFR.name).gameObject;
			IFFEnCours =  LigneEnCours.transform.Find(IFF.name).gameObject;
			IFREnCours.GetComponent<InputFieldRayon>().UnsubscribeRefreshEvent();
			IFFEnCours.GetComponent<InputFieldFacteur>().UnsubscribeRefreshEvent();
			Debug.Log("Line small panel destroyed - Events unSubscribed");
	}
	
	void ResetPanel()
	{
		//GameObject LigneEnCours,IFREnCours,IFFEnCours;
		foreach (Transform child in PanelLignes.transform)
		//while (PanelLignes.transform.childCount>1)
		{
			/*LigneEnCours = PanelLignes.transform.GetChild(0).gameObject;
			IFREnCours =  LigneEnCours.transform.Find(IFR.name).gameObject;
			IFFEnCours =  LigneEnCours.transform.Find(IFF.name).gameObject;
			IFREnCours.GetComponent<InputFieldRayon>().UnsubscribeRefreshEvent();
			IFFEnCours.GetComponent<InputFieldFacteur>().UnsubscribeRefreshEvent();
			Debug.Log("Event unSubscribed");*/
			//RemoveEventsFromLine(child.gameObject);
			Destroy(child.gameObject);
			//Debug.Log("Line Destroyed");
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
			IFR.GetComponent<InputField>().GetComponent<InputFieldRayon>().Start();
			/*IFA.GetComponent<InputField>().GetComponent<InputFieldAmplitude>().index=i;
			IFA.GetComponent<InputField>().GetComponent<InputFieldAmplitude>().RefreshContent();
			IFV.GetComponent<InputField>().GetComponent<InputFieldVitesse>().index=i;
			IFV.GetComponent<InputField>().GetComponent<InputFieldVitesse>().RefreshContent();
			IFP.GetComponent<InputField>().GetComponent<InputFieldPhase>().index=i;
			IFP.GetComponent<InputField>().GetComponent<InputFieldPhase>().RefreshContent();*/
			IFF.GetComponent<InputField>().GetComponent<InputFieldFacteur>().index=i;
			IFF.GetComponent<InputField>().GetComponent<InputFieldFacteur>().RefreshContent();
			//IFF.GetComponent<InputField>().GetComponent<InputFieldRayon>().Start();
			//Debug.Log(ToggleActiveDisque.name);//GetComponent<Toggle>().GetComponent<ToggleAnimRayon>().index.ToString);
			/*ToggleActiveDisque.GetComponent<Toggle>().GetComponent<ToggleAnimRayon>().index=i;
			ToggleAnimation.GetComponent<Toggle>().GetComponent<ToggleRotAxe>().index=i;*/
			GameObject NewLine=Instantiate(LigneSpiro);
			NewLine.name="Ligne"+(i).ToString();
			NewLine.SetActive(true);
			NewLine.transform.SetParent(PanelLignes.transform,false);
		}
		GetComponent<RectTransform>().ForceUpdateRectTransforms();
		Debug.Log("Building Panel");
	}
	
	public void AddLine()
	{
		int i;
		SelectedLine=GetActiveSpiroFormule();
		SelectedLine.profondeur++;
		i=SelectedLine.profondeur-1; //avant dernière ligne avant le crayon
		TextNumeroDisque.GetComponent<Text>().text=(i+1).ToString();
			IFR.GetComponent<InputField>().GetComponent<InputFieldRayon>().index=i;
			IFR.GetComponent<InputField>().GetComponent<InputFieldRayon>().RefreshContent();
			/*IFA.GetComponent<InputField>().GetComponent<InputFieldAmplitude>().index=i;
			IFA.GetComponent<InputField>().GetComponent<InputFieldAmplitude>().RefreshContent();
			IFV.GetComponent<InputField>().GetComponent<InputFieldVitesse>().index=i;
			IFV.GetComponent<InputField>().GetComponent<InputFieldVitesse>().RefreshContent();
			IFP.GetComponent<InputField>().GetComponent<InputFieldPhase>().index=i;
			IFP.GetComponent<InputField>().GetComponent<InputFieldPhase>().RefreshContent();*/
			IFF.GetComponent<InputField>().GetComponent<InputFieldFacteur>().index=i;
			IFF.GetComponent<InputField>().GetComponent<InputFieldFacteur>().RefreshContent();
			//Debug.Log(ToggleActiveDisque.name);//GetComponent<Toggle>().GetComponent<ToggleAnimRayon>().index.ToString);
			/*ToggleActiveDisque.GetComponent<Toggle>().GetComponent<ToggleAnimRayon>().index=i;
			ToggleAnimation.GetComponent<Toggle>().GetComponent<ToggleRotAxe>().index=i;*/
			GameObject NewLine=Instantiate(LigneSpiro);
			NewLine.name="Ligne"+(i).ToString();
			NewLine.SetActive(true);
			NewLine.transform.SetParent(PanelLignes.transform,false);
			//PanelOptionTousDisques.GetComponent<PanelOptionTousDisques>().BuildPanel();  //refresh panel tous disques too
	}
	
	public void DeleteLine()
	{
		GameObject LastLine;
		SelectedLine=GetActiveSpiroFormule();
		if (SelectedLine.profondeur>2) 
		{
			SelectedLine.profondeur--;
			LastLine = PanelLignes.transform.GetChild(SelectedLine.profondeur).gameObject;
			RemoveEventsFromLine(LastLine);
			Destroy(LastLine);
		}
		//PanelOptionTousDisques.GetComponent<PanelOptionTousDisques>().BuildPanel(); //refresh panel tous disques too
	}
	
	public void LateUpdate()
	{
		transform.parent.GetComponent<RectTransform>().ForceUpdateRectTransforms();
		transform.Translate(Vector3.up); //force refresh		
	}
}
