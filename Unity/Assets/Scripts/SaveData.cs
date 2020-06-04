using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class SaveData
{
    public static SpiroContainer spiroContainer = new SpiroContainer();
	
	public delegate void SerializeAction();
	public static event SerializeAction OnLoaded;
	public static event SerializeAction OnBeforeSave;
	public GameController GC;
	
	public static void Load(string path)
	{
		spiroContainer=LoadSpiros(path);
		//GameController gameController = GameController();
		
		foreach (SpiroData data in spiroContainer.spiros)
		{
			//GameController GClocal = new GameController();
			GameController.CreateSpiro(data,GameController.SpiroBasePath);
		}
		OnLoaded();
	}
	
	public static void Save(string path, SpiroContainer spiros)
	{
		OnBeforeSave();
		Debug.Log("Saving1...");
		SaveSpiros(path,spiros);
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
		FileStream stream = new FileStream(path, FileMode.Truncate);
		serializer.Serialize(stream,spiros);
		stream.Close();
		Debug.Log("Saving2...");
	}
}
