using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordPanel : MonoBehaviour
{
	public InputField IFNomFichier;
	public bool record=false;
	public Button RecButton;
	public Camera MainCamera;
	public string fileName;
	public int index;
	
	public void ShowPanel()
	{	
		transform.GetChild(0).gameObject.SetActive(true);
	}

	public void ClosePanel()
	{	
		transform.GetChild(0).gameObject.SetActive(false);
		//record=false;
	}

	public void CallSaveSequence()
	{
		fileName = IFNomFichier.text;
		foreach (char c in System.IO.Path.GetInvalidFileNameChars())
		{
			fileName = fileName.Replace(c, '_');
		}
		//fileName+=".png";
		//SaveData.Save(GameController.dataPath+fileName,SaveData.spiroContainer);
		//Debug.Log("fichier "+fileName+" enregistré.");
		var colors = RecButton.colors;
        colors.normalColor = Color.red;
		RecButton.colors = colors;
		ClosePanel();
		index=1;
		record=true;
	}
	
	public void CallRecordPanel()
	{
		if (!record)
		{
			ShowPanel();
		}
		else
		{
			record=false;			
			ClosePanel();
			var colors = RecButton.colors;
			colors.normalColor = Color.white;
			RecButton.colors = colors;
		}
	}
	
	public void Update()
	{
		if (record)
		{
			MainCamera.GetComponent<CameraScreenShot>().Record(fileName+index.ToString());
			index++;
		}
	}
}
