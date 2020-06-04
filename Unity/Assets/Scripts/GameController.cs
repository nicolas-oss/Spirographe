using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Spirographe
{
    string dataPath;
	public static string SpiroBasePath;
	
	public Button loadButton,saveButton,newButton,duplicateButton;
	public Color selected = Color.black;
	public Color unselected = Color.gray;
	Text NameLine;
	public GameObject ActiveObjectInScene,PreviousTextLine,TextNameLine,NewLineName,NewLine;
	public SpiroFormule SelectedLine;
	public int LineCount;
	public GameObject RootList;
	public SelectButton BoutonSelectionPremiereLigne;
	public GameObject SpiroFormuleToInstantiate;

	
    void Start()
    {
      dataPath=Application.dataPath+"/Spiro/trytosave.xml";
	  SpiroBasePath=Application.dataPath+"/Prefabs/SpiroFormule.prefab";
	  BoutonSelectionPremiereLigne.SelectLine(); //on sélectionne la seule spiro de la scene
	  Initialisation(); //Send Initialisation event (to build panels)
	  GetActiveTextLine();
    }
	
	void OnEnable()
	{
		saveButton.onClick.AddListener(delegate{SaveData.Save(dataPath,SaveData.spiroContainer);});
		loadButton.onClick.AddListener(delegate{SaveData.Load(dataPath);});
		duplicateButton.onClick.AddListener(delegate{DuplicateCurrentSpiro();});
	}
	
	void OnDisable()
	{
		saveButton.onClick.RemoveListener(delegate{SaveData.Save(dataPath,SaveData.spiroContainer);});
		loadButton.onClick.RemoveListener(delegate{SaveData.Load(dataPath);});
		duplicateButton.onClick.RemoveListener(delegate{DuplicateCurrentSpiro();});
	}
	
	public void GetActiveLine()
	{
		ActiveObjectInScene = GetActiveObject();
		SelectedLine = GetActiveSpiroFormule();
	}
	
	public void GetActiveTextLine() //recherche de la ligne active en scannant les enfants du GameObject contenant toutes les lignes
	{
		GameObject root = GameObject.Find("ListSpiro");
		PreviousTextLine=root.transform.GetChild(0).gameObject;
		//GameObject root = new GameObject();
		//root=RootList;
		//Debug.Log(RootList.name+" "+RootList.transform.gameObject);
		for (int j=0; j<root.transform.childCount; j++)
		{
			if (root.transform.GetChild(j).gameObject.GetComponent<Text>().color == selected)
			{
				PreviousTextLine=root.transform.GetChild(j).gameObject;
				//Debug.Log("Text ligne active : n°"+j.ToString());
				break;
			}
		}
	}
	
	public string CreateTextLine()
	{
		GetActiveTextLine();
		NewLineName = Instantiate(PreviousTextLine);
		LineCount++;
		NameLine = NewLineName.GetComponent<Text>();
		NewLineName.transform.SetParent(PreviousTextLine.transform.parent,false);
		NameLine.text = ("SpiroFormule"+LineCount.ToString());
		NewLine.name = NameLine.text;
		NewLineName.transform.Find("DeleteButton").gameObject.SetActive(true);
		NewLineName.transform.Find("SelectButton").gameObject.GetComponent<SelectButton>().SelectLine();
		return NewLine.name;
	}
	
	public void DuplicateCurrentSpiro()
	{
		bool Visibility;
		//GameObject BoutonDelete;
		
		GetActiveLine();
		NewLine = Instantiate(ActiveObjectInScene);
		NewLine.tag="Untagged";
		GetActiveTextLine();
		CreateTextLine();
		RefreshInputField();
	}
	
	public static void NewSpiro()
	{
	}
	
	public void LoadSpiro(string path)
	{
	}
	
	public void CreateSpiro(SpiroData data, string path)
	{
		GetActiveLine();
		//GameObject prefab = Resources.Load<GameObject>(path);
		//GameObject go = GameObject.Instantiate(prefab) as GameObject;
		NewLine = Instantiate(ActiveObjectInScene);
		NewLine.GetComponent<SpiroFormule>().data=data;
		NewLine.GetComponent<SpiroFormule>().LoadData();
		NewLine.tag="Untagged";
		NewLine.name=CreateTextLine();
		//GetActiveTextLine();
		//NewLineName = Instantiate(PreviousTextLine);
		//NewLineName.transform.SetParent(PreviousTextLine.transform.parent,false);
	}
}
