using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEditor;

public class SaveData
{
    public static SpiroContainer spiroContainer = new SpiroContainer();
	
	public delegate void SerializeAction();
	public static event SerializeAction OnLoaded;
	public static event SerializeAction OnBeforeSave;
	public GameController GC;
	
	public static void Load(string path)
	{
		path=EditorUtility.OpenFilePanel("Chargement",path,"xml");
		spiroContainer=LoadSpiros(path);
		//GameController gameController = GameController();
		
		
		foreach (SpiroData data in spiroContainer.spiros)
		{
			GameController GClocal = new GameController();
			GClocal.CreateSpiro(data,GameController.SpiroBasePath);
		}
		OnLoaded();
	}
	
	public static string SpiroName() 
	{
		return string.Format("{0}/Spiro/spiro_{1}.xml", Application.dataPath, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));	
    }
	
	public static void Save(string path, SpiroContainer spiros)
	{
		string localPath;
		OnBeforeSave();
		Debug.Log("Saving1...");
		localPath=SpiroName();
		Debug.Log(localPath);
		SaveSpiros(localPath,spiros);
		ClearSpiros();
	}
	
	public static void AddSpiroData(SpiroData data)
	{
		spiroContainer.spiros.Add(data);
	}
	
	public static void ClearSpiros()
	{
		spiroContainer.spiros.Clear();
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
