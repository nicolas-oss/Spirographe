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
		VidangeContentPanel();
		GenerationLignes();
		transform.GetChild(0).gameObject.SetActive(true);
	}
	
	void VidangeContentPanel()
	{
		/*while (ViewportContent.transform.childCount>0)
		{
			GameObject LigneEnCours;
			LigneEnCours = ViewportContent.transform.GetChild(0).gameObject;
			Destroy(LigneEnCours);
		}*/
		foreach (Transform child in ViewportContent.transform)
		{
			Destroy(child.gameObject);
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
				NewLine.transform.SetSiblingIndex(0); //on remonte la dernière ligne créée en haut
				NewLine.SetActive(true);
			}
	}
	
	public void SelectionLigne(string NomLigne)
	{
		Debug.Log(sourceDirectory+NomLigne+" reçu in Panel");
		//this.transform.GetChild(0).gameObject.SetActive(false);
		FermeturePanneau();
		SaveData.Load(Application.dataPath+"/Spiro/"+NomLigne);
		
		//return NomLigne;
	}
	
	public void FermeturePanneau()
	{
		Debug.Log("Closing...");
		transform.GetChild(0).gameObject.SetActive(false);
	}
}
