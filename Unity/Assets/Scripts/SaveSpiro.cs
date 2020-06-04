using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("ClasseACollection")]
public class SaveSpiro : MonoBehaviour
{
	public GameObject Interface;
	[XmlArray("TableauClasseA"),XmlArrayItem("ClasseA")]
	public testClassA[] TableauClasseA;
	//TableauClasseA = new ClasseA[5];
	
	public static string SpiroName() 
	{
		return string.Format("{0}/Spiro/Spiro_{1}.prefab", Application.dataPath, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
	
	public void SaveSpirographe()
	{
		//TableauClasseA=new testClassA[5];
		//testClassA ClasseAObject = new testClassA();
		//ClasseAObject.nom="toto";
		//testClassA[] TableauClasseA= new testClassA[5];
		//GameObject Spiro;
		//SpiroFormule SpiroToSave;
		//SpiroToSave=GetActiveSpiroFormule();
		//string Name;
		//SpiroToSave = Interface.GetComponent<Interface>().SelectedLine;
		//Spiro = SpiroToSave.GameObject;
		/*Name = SpiroName();
		PrefabUtility.SaveAsPrefabAsset(Root, SpiroName());*/
		
		//public testClassA ClasseAA;
		//classeAA=ClasseA;
		
		
		
		XmlSerializer serializer = new XmlSerializer(typeof(testClassA));
        StreamWriter writer = new StreamWriter("Formule2.xml");
        serializer.Serialize(writer.BaseStream, this);
        writer.Close();
		Debug.Log("Saved ");
	}
}
