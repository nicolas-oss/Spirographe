using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOptionTousDisques : Spirographe
{
    public GameObject SpiroParametrableActive,LigneSpiro,TextNumeroDisque,IFR,IFA,IFV,IFP,IFF,ToggleActiveDisque,ToggleAnimation,PanelLignes;
	SpiroParametrable SpiroParametrableLine;
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void ResetPanel()
	{
		while (PanelLignes.transform.childCount>0)
		{
			Destroy(PanelLignes.transform.GetChild(0));
		}
	}
	
	public void BuildPanel()
	{
		ResetPanel();
		SpiroParametrableLine=SpiroParametrableActive.GetComponent<SpiroParametrable>();
		int profondeur = SpiroParametrableLine.profondeur;
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
			ToggleActiveDisque.GetComponent<Toggle>().GetComponent<ToggleAnimRayon>().index=i;
			ToggleAnimation.GetComponent<Toggle>().GetComponent<ToggleRotAxe>().index=i;
			GameObject NewLine=Instantiate(LigneSpiro);
			NewLine.name="Ligne"+(i).ToString();
			NewLine.SetActive(true);
			NewLine.transform.SetParent(PanelLignes.transform,false);
		}
	}
}
