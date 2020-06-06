using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class FileBrowserPanel : MonoBehaviour
{
    public string sourceDirectory,path;
	public GameObject ViewportContent;
	public GameObject Ligne;
	public int index;
	
	// Start is called before the first frame update
    void Start()
    {
        sourceDirectory=Application.dataPath+"/Spiro/";
		Debug.Log("SourceDir="+sourceDirectory);
		//GenerationLignes();
    }

	public void BuildPanel()
	{	
		//VidangeContentPanel();
		GenerationLignes();
		transform.GetChild(0).gameObject.SetActive(true);
	}
	
	void VidangeContentPanel()
	{
		while (ViewportContent.transform.childCount>0)
		{
			GameObject LigneEnCours;
			LigneEnCours = ViewportContent.transform.GetChild(0).gameObject;
			Destroy(LigneEnCours);
		}
	}
	
	public void GenerationLignes()
	{
		var txtFiles = Directory.EnumerateFiles(sourceDirectory, "*.xml");
		//int i=0;
		foreach (string currentFile in txtFiles)
			{
				//Debug.Log(currentFile);
				string fileName = currentFile.Substring(sourceDirectory.Length);
				GameObject NewLine=Instantiate(Ligne);									//on crée une nlle ligne
				NewLine.transform.SetParent(ViewportContent.transform,true);				//on la rattache au viewport
				NewLine.transform.GetChild(0).gameObject.GetComponent<Text>().text=fileName;	//on renomme le champ texte correspondant
				//i++;
			}
	}
	
	public void SelectionLigne(string NomLigne)
	{
		Debug.Log(sourceDirectory+NomLigne+" reçu in Panel");
		transform.GetChild(0).gameObject.SetActive(false);
		SaveData.Load(Application.dataPath+"/Spiro/"+NomLigne);
		FermeturePanneau();
		//return NomLigne;
	}
	
	public void FermeturePanneau()
	{
		transform.GetChild(0).gameObject.SetActive(false);
	}
}
