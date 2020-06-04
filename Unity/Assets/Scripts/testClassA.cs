using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

//[XmlRoot("TestClassACollection")]
public class testClassA {
    // Start is called before the first frame update
       
    //[XmlArray("ArrayClassA"),XmlArrayItem("ClassA")]
	public int variableA;
	public float abscisse;
	public string nom,prenom,adresse;
	

    public void Start() 
    {
        //Hero knight = new Hero();
        nom = "Knight of Solamnia";
        prenom = "Alfred";
        adresse = "rue du pont";
        abscisse = 2.501f;
 
        /*XmlSerializer serializer = new XmlSerializer(typeof(testClassA));
		using(FileStream stream = new FileStream(Application.dataPath, FileMode.Create)){serializer.Serialize(stream, this);}
        /*StreamWriter writer = new StreamWriter("Personnage.xml");
        serializer.Serialize(writer.BaseStream, gameObject);
        writer.Close();
		//this.Save(Application.dataPath + "/personnages.xml");
		/*Debug.Log("writer closed");*/
    }
	

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public static void fonctionA()
	{
		Debug.Log("fonctionA");
	}
}
