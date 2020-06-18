using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class FileBrowserPanel : MonoBehaviour
{
	public GameObject ViewportContent;
	public GameObject Ligne;
	public int index;
	//string LigneSelectionnee;
	int Fonction;
	static int Load=0;
	static int Merge=0;

	public void BuildPanel()
	{	
		VidangeContentPanel();
		GenerationLignes();
		transform.GetChild(0).gameObject.SetActive(true);
	}
	
	void VidangeContentPanel()
	{
		foreach (Transform child in ViewportContent.transform)
		{
			Destroy(child.gameObject);
		}
	}
	
	public void GenerationLignes()
	{
		var txtFiles = Directory.EnumerateFiles(GameController.dataPath, "*.xml");
		foreach (string currentFile in txtFiles)
			{
				string fileName = currentFile.Substring(GameController.dataPath.Length);
				GameObject NewLine=Instantiate(Ligne);											//on crée une nlle ligne
				NewLine.transform.SetParent(ViewportContent.transform,true);					//on la rattache au viewport
				NewLine.transform.GetChild(0).gameObject.GetComponent<Text>().text=fileName;	//on renomme le champ texte correspondant
				NewLine.transform.SetSiblingIndex(0); 											//on remonte la dernière ligne créée en haut
				NewLine.SetActive(true);
			}
	}
	
	public void SelectionLigne(string NomLigne)
	{
		FermeturePanneau();
		if (Fonction==Load)
		{
			GameController.DeleteAll();
			GameController.LineCount=0;
		}
		SaveData.Load(GameController.dataPath+NomLigne);
		//LigneSelectionnee=NomLigne;
	}
	
	public void FermeturePanneau()
	{
		//Debug.Log("Closing...");
		transform.GetChild(0).gameObject.SetActive(false);
	}
	
	public void BuildLoadPanel()
	{
		Fonction = Load;
		BuildPanel();
	}
	
	public void BuildMergePanel()
	{
		Fonction = Merge;
		BuildPanel();
	}
}
