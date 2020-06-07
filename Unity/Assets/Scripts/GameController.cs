using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    string dataPath;
	public static string SpiroBasePath;
	
	public Button loadButton,saveButton,newButton,duplicateButton;
	public static Color selected = Color.black;
	public Color unselected = Color.gray;
	static Text NameLine;
	public static GameObject ActiveObjectInScene,PreviousTextLine,TextNameLine,NewLineName;
	public static GameObject NewLine;
	public SpiroFormule SelectedLine;
	//public int LineCount;
	public GameObject RootList;
	public SelectButton BoutonSelectionPremiereLigne;
	public GameObject SpiroFormuleToInstantiate;
	public GameObject FileBrowser;

	
    void Start()
    {
      dataPath=Application.dataPath+"/Spiro/";
	  SpiroBasePath=Application.dataPath+"/Prefabs/SpiroFormule.prefab";
	  BoutonSelectionPremiereLigne.SelectLine(); //on sélectionne la seule spiro de la scene
	  Spirographe.Selection(); //Send Initialisation event (to build panels)
	  GetActiveTextLine();
	  Spirographe.LineCount=1; //on comence avec une ligne
    }
	
	void OnEnable()
	{
		saveButton.onClick.AddListener(delegate{SaveData.Save(dataPath,SaveData.spiroContainer);});
		loadButton.onClick.AddListener(delegate{FileBrowser.GetComponent<FileBrowserPanel>().BuildPanel();});
		duplicateButton.onClick.AddListener(delegate{DuplicateCurrentSpiro();});
	}
	
	void OnDisable()
	{
		saveButton.onClick.RemoveListener(delegate{SaveData.Save(dataPath,SaveData.spiroContainer);});
		loadButton.onClick.RemoveListener(delegate{FileBrowser.GetComponent<FileBrowserPanel>().BuildPanel();});
		duplicateButton.onClick.RemoveListener(delegate{DuplicateCurrentSpiro();});
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
	
	public static string CreateTextLine()
	{
		GetActiveTextLine();
		NewLineName = Instantiate(PreviousTextLine);
		Spirographe.LineCount++;
		NameLine = NewLineName.GetComponent<Text>();
		NewLineName.transform.SetParent(PreviousTextLine.transform.parent,false);
		NameLine.text = ("Spiro"+Spirographe.LineCount.ToString());
		NewLine.name = NameLine.text;
		NewLineName.transform.Find("DeleteButton").gameObject.SetActive(true);
		NewLineName.transform.Find("SelectButton").gameObject.GetComponent<SelectButton>().SelectLine();
		return NewLine.name;
	}
	
	public void DuplicateCurrentSpiro()
	{
		//bool Visibility;
		//GameObject BoutonDelete;
		
		GetActiveLine();
		NewLine = Instantiate(ActiveObjectInScene);
		NewLine.tag="Untagged";
		GetActiveTextLine();
		CreateTextLine();
		Spirographe.RefreshInputField();
	}
	
	public static void NewSpiro()
	{
	}
	
	public void LoadSpiro(string path)
	{
	}
	
	public static void CreateSpiro(SpiroData data, string path)
	{
		//GetActiveLine();
		//GameObject prefab = Resources.Load<GameObject>(path);
		//GameObject go = GameObject.Instantiate(prefab) as GameObject;
		NewLine = Instantiate(Spirographe.ActiveObjectInScene);
		NewLine.GetComponent<SpiroFormule>().data=data;
		NewLine.GetComponent<SpiroFormule>().LoadData();
		NewLine.tag="Untagged";
		NewLine.name=CreateTextLine();
		//GetActiveTextLine();
		//NewLineName = Instantiate(PreviousTextLine);
		//NewLineName.transform.SetParent(PreviousTextLine.transform.parent,false);
	}
}
