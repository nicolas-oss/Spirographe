using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileSaverPanel : MonoBehaviour
{
	public InputField IFNomFichier;
	
	public void ShowPanel()
	{	
		transform.GetChild(0).gameObject.SetActive(true);
	}

	public void ClosePanel()
	{	
		transform.GetChild(0).gameObject.SetActive(false);
	}

	public void CallSaveFile()
	{
		string fileName = IFNomFichier.text;
		foreach (char c in System.IO.Path.GetInvalidFileNameChars())
		{
			fileName = fileName.Replace(c, '_');
		}
		fileName+=".xml";
		SaveData.Save(GameController.dataPath+fileName,SaveData.spiroContainer);
		Debug.Log("fichier "+fileName+" enregistré.");
		ClosePanel();
	}
	
}
