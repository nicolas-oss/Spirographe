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
		SaveData.Load(GameController.dataPath+NomLigne);
	}
	
	public void FermeturePanneau()
	{
		//Debug.Log("Closing...");
		transform.GetChild(0).gameObject.SetActive(false);
	}
}
