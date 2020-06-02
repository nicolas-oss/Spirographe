using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SaveSpiro : MonoBehaviour
{
	public GameObject Interface;
	public GameObject Root;
	
	public static string SpiroName() 
	{
		return string.Format("{0}/Spiro/Spiro_{1}.prefab", Application.dataPath, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
	
	public void SaveSpirographe()
	{
		//GameObject Spiro;
		SpiroFormule SpiroToSave;
		string Name;
		//SpiroToSave = Interface.GetComponent<Interface>().SelectedLine;
		//Spiro = SpiroToSave.GameObject;
		Name = SpiroName();
		PrefabUtility.SaveAsPrefabAsset(Root, SpiroName());
		Debug.Log("Saved " + Name);
	}
}
