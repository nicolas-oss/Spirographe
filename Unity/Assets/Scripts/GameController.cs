﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static string dataPath;
	public static string SpiroBasePath;
	
	public Button loadButton,saveButton,newButton,duplicateButton,deletAllButton,fusionnerButton;
	public static Color selected = Color.black;
	public Color unselected = Color.gray;
	static Text NameLine;
	public static GameObject ActiveObjectInScene,PreviousTextLine,TextNameLine,NewLineName;
	public static GameObject NewLine;
	public SpiroFormule SelectedLine;
	//public int LineCount;
	public static int LineCount;
	public GameObject RootList;
	public SelectButton BoutonSelectionPremiereLigne;
	public GameObject SpiroFormuleToInstantiate,TextBaseToInstantiate,MultiSpiroToInstantiate;
	public GameObject FileBrowser;
	public GameObject SpiroParametrableRoot,MultiSpiroParametrableRoot; //papa de tous les spiroFormules + centre de rotation base
	string path;
	static GameObject BaseSpiro,SpiroRoot,TextBase,MultiSpiroRoot,BaseMultiSpiro;

    void Start()
    {
      dataPath=Application.dataPath+"/Spiro/";
	  SpiroBasePath=Application.dataPath+"/Prefabs/SpiroFormule.prefab";
	  BoutonSelectionPremiereLigne.SelectLine(); 	//on sélectionne la seule spiro de la scene
	  Spirographe.Selection(); 						//Send Initialisation event (to build panels)
	  GetActiveTextLine();
	  LineCount=2; 									//on comence avec une/deux ligne(s)
	  BaseSpiro = SpiroFormuleToInstantiate;
	  BaseMultiSpiro = MultiSpiroToInstantiate;
	  SpiroRoot = SpiroParametrableRoot;
	  MultiSpiroRoot = MultiSpiroParametrableRoot;
	  TextBase = TextBaseToInstantiate;
    }
	
	void OnEnable()
	{
		//saveButton.onClick.AddListener(delegate{SaveData.Save(dataPath,SaveData.spiroContainer);});
		loadButton.onClick.AddListener(delegate{LoadScene(path);});
		fusionnerButton.onClick.AddListener(delegate{MergeScene(path);});
		duplicateButton.onClick.AddListener(delegate{DuplicateCurrentSpiro();});
		deletAllButton.onClick.AddListener(delegate{DeleteAll();});
	}
	
	void OnDisable()
	{
		//saveButton.onClick.RemoveListener(delegate{SaveData.Save(dataPath,SaveData.spiroContainer);});
		loadButton.onClick.RemoveListener(delegate{LoadScene(path);});
		fusionnerButton.onClick.RemoveListener(delegate{MergeScene(path);});
		duplicateButton.onClick.RemoveListener(delegate{DuplicateCurrentSpiro();});
		deletAllButton.onClick.RemoveListener(delegate{DeleteAll();});
	}
	
	public void GetActiveLine()
	{
		ActiveObjectInScene = Spirographe.GetActiveObject();
		SelectedLine = Spirographe.GetActiveSpiroFormule();
	}
	
	public static void GetActiveTextLine() //recherche de la ligne active en scannant les enfants du GameObject contenant toutes les lignes
	{
		GameObject root = GameObject.Find("ListSpiro");
		PreviousTextLine=root.transform.GetChild(0).gameObject;
		for (int j=0; j<root.transform.childCount; j++)
		{
			if (root.transform.GetChild(j).transform.Find("TextName").GetComponent<Text>().color == selected)
			{
				PreviousTextLine=root.transform.GetChild(j).gameObject;
				Debug.Log("Text ligne active : n°"+j.ToString());
				Debug.Log("Text ligne active : nom"+root.transform.GetChild(j).transform.Find("TextName").GetComponent<Text>().text);
				break;
			}
		}
	}
	
	public static string CreateTextLine(string name)
	{
		//GetActiveTextLine();
		GameObject root = GameObject.Find("ListSpiro");
		NewLineName = Instantiate(TextBase);
		LineCount++;
		NameLine = NewLineName.transform.Find("TextName").GetComponent<Text>();
		NewLineName.transform.SetParent(root.transform,false);
		NameLine.text = (name+LineCount.ToString());
		NewLine.name = NameLine.text;
		NewLineName.transform.Find("DeleteButton").gameObject.SetActive(true);
		//NewLineName.transform.Find("SelectButton").gameObject.GetComponent<SelectButton>().SelectLine();
		return NewLine.name;
	}
	
	public void DuplicateCurrentSpiro()
	{
		GetActiveLine();
		NewLine = Instantiate(ActiveObjectInScene);
		NewLine.tag="Untagged";
		GetActiveTextLine();
		CreateTextLine("Spiro");
		Spirographe.RefreshInputField();
	}
	
	public static void NewSpiro()
	{
	}
	
	public void LoadScene(string path)
	{
		FileBrowser.GetComponent<FileBrowserPanel>().BuildLoadPanel();
	}
	
	public void MergeScene(string path)
	{
		FileBrowser.GetComponent<FileBrowserPanel>().BuildMergePanel();
	}
	
	public static void CreateSpiro(SpiroData data, string path)
	{
		//GetActiveLine();
		//GameObject prefab = Resources.Load<GameObject>(path);
		//GameObject go = GameObject.Instantiate(prefab) as GameObject;
		
		NewLine = Instantiate(BaseSpiro);
		NewLine.GetComponent<SpiroFormule>().data=data;
		NewLine.GetComponent<SpiroFormule>().LoadData();
		NewLine.GetComponent<SpiroFormule>().Centre=SpiroRoot;
		NewLine.transform.SetParent(SpiroRoot.transform,false);
		NewLine.tag="Untagged";
		NewLine.name=CreateTextLine("Spiro");
		//GetActiveTextLine();
		//NewLineName = Instantiate(PreviousTextLine);
		//NewLineName.transform.SetParent(PreviousTextLine.transform.parent,false);
	}
	
	public static void CreateMultiSpiro(MultiSpiroData data, string path)
	{
		NewLine = Instantiate(BaseMultiSpiro);
		NewLine.GetComponent<MultiSpiro>().data=data;
		NewLine.GetComponent<MultiSpiro>().LoadData();
		//NewLine.GetComponent<SpiroFormule>().Centre=SpiroRoot;
		NewLine.transform.SetParent(MultiSpiroRoot.transform,false);
		NewLine.tag="Untagged";
		NewLine.name=CreateTextLine("MultiSpiro");
		//GetActiveTextLine();
		//NewLineName = Instantiate(PreviousTextLine);
		//NewLineName.transform.SetParent(PreviousTextLine.transform.parent,false);
	}
	
	public static void DeleteAll()
	{
		GameObject TextNameLineToDelete;
		GameObject root = GameObject.Find("ListSpiro");
		Text NameLine;
		Debug.Log(root.transform.childCount.ToString());
		for (int j=root.transform.childCount; j>0; j--)
		{
			TextNameLineToDelete = root.transform.GetChild(j-1).gameObject;
			NameLine = TextNameLineToDelete.transform.Find("TextName").GetComponent<Text>();
			Destroy(GameObject.Find(NameLine.text));
			Destroy(TextNameLineToDelete);
		}	
	}
}
