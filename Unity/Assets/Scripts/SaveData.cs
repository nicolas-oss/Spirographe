using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

public class SaveData
{
    public static SpiroContainer spiroContainer = new SpiroContainer();
	
	public delegate void SerializeAction();
	public static event SerializeAction OnLoaded;
	//public static event SerializeAction OnBeforeSave;
	//public GameController GC;
	
	public static void Load(string path)
	{
		spiroContainer=LoadSpiros(path);
		
		foreach (SpiroData data in spiroContainer.spiros)
		{
			GameController.CreateSpiro(data,GameController.SpiroBasePath);
		}
		
		foreach (MultiSpiroData data in spiroContainer.multiSpiros)
		{
			GameController.CreateMultiSpiro(data,GameController.SpiroBasePath);
			//GameObject.Find("MultiSpiro").GetComponent<MultiSpiro>().data=data;
			//GameObject.Find("MultiSpiro").GetComponent<MultiSpiro>().LoadData();
		}
		//OnLoaded();
	}
	
	public static string SpiroName() 
	{
		return string.Format("{0}/Spiro/spiro_{1}.xml", Application.dataPath, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));	
    }
	
	public static void SaveAllDatas()
	{
		GameObject RootList,LineToSave;
		string NameLine;
		RootList = GameObject.Find("ListSpiro");
		
		/*foreach (Transform child in RootList.transform)
		{
			NameLine = child.gameObject.transform.Find("TextName").GetComponent<Text>().text;
			LineToSave = GameObject.Find(NameLine);
			LineToSave.GetComponent<SpiroFormule>().StoreData();
		}*/
		
		for (int j=0; j<RootList.transform.childCount; j++)
		{
			NameLine = RootList.transform.GetChild(j).transform.Find("TextName").GetComponent<Text>().text;
			//Debug.Log(NameLine);
			LineToSave = GameObject.Find(NameLine);
			if (LineToSave.GetComponent<SpiroFormule>()!=null) 
			{
				LineToSave.GetComponent<SpiroFormule>().StoreData();
				AddSpiroData(LineToSave.GetComponent<SpiroFormule>().data);
			}
		}
		//GameObject.Find("MultiSpiro").GetComponent<MultiSpiro>().StoreData();
		AddMultiSpiroData(GameObject.Find("MultiSpiro").GetComponent<MultiSpiro>().data);
	}
	
	public static void Save(string path, SpiroContainer spiros)
	{
		ClearSpiros();
		SaveAllDatas();
		Debug.Log("Saving1...");
		SaveSpiros(path,spiros);
		ClearSpiros();
	}
	
	public static void AddSpiroData(SpiroData data)
	{
		Debug.Log("AddSpiroData Call");
		if (data.Master) spiroContainer.spiros.Add(data);
	}
	
	public static void AddMultiSpiroData(MultiSpiroData data)
	{
		Debug.Log("AddMultiSpiroData Call");
		spiroContainer.multiSpiros.Add(data);
	}
	
	public static void ClearSpiros()
	{
		spiroContainer.spiros.Clear();
		spiroContainer.multiSpiros.Clear();
		//Debug.Log("Container Empty");
		//Debug.Log(spiroContainer.spiros.Count.ToString());
	}

	public static SpiroContainer LoadSpiros(string path)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(SpiroContainer));
		FileStream stream = new FileStream(path, FileMode.Open);
		SpiroContainer spiros = serializer.Deserialize(stream) as SpiroContainer;
		stream.Close();
		return spiros;
	}
	
	public static void SaveSpiros(string path, SpiroContainer spiros)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(SpiroContainer));
		FileStream stream = new FileStream(path, FileMode.Create);
		serializer.Serialize(stream,spiros);
		stream.Close();
		Debug.Log("Saving2...");
	}
}
