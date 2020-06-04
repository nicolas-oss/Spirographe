using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Spirographe
{
    string dataPath;
	
	public Button loadButton,saveButton,newButton,duplicateButton;
	public Color selected = Color.black;
	public Color unselected = Color.gray;
	Text NameLine;
	public GameObject ActiveObjectInScene,PreviousTextLine,TextNameLine,NewLineName,NewLine;
	public SpiroFormule SelectedLine;
	public int LineCount;
	public GameObject RootList;
	public SelectButton BoutonSelectionPremiereLigne;

	
    void Start()
    {
      dataPath=Application.dataPath+"/Spiro/trytosave.xml";
	  BoutonSelectionPremiereLigne.SelectLine(); //on sélectionne la seule spiro de la scene
	  Initialisation(); //Send Initialisation event (to build panels)
	  
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
	}
	
	public void GetActiveLine()
	{
		ActiveObjectInScene = GetActiveObject();
		SelectedLine = GetActiveSpiroFormule();
	}
	
	public void GetActiveTextLine() //recherche de la ligne active en scannant les enfants du GameObject contenant toutes les lignes
	{
		PreviousTextLine=RootList.transform.GetChild(0).gameObject;
		for (int j=0; j<RootList.transform.childCount; j++)
		{
			if (RootList.transform.GetChild(j).GetComponent<Text>().color == selected)
			{
				PreviousTextLine=RootList.transform.GetChild(j).gameObject;
				break;
			}
		}
	}
	
	public void DuplicateCurrentSpiro()
	{
		bool Visibility;
		//GameObject BoutonDelete;
		
		GetActiveLine();
		NewLine = Instantiate(ActiveObjectInScene);
		NewLine.tag="Untagged";
		GetActiveTextLine();
		NewLineName = Instantiate(PreviousTextLine);
		LineCount++;
		NameLine = NewLineName.GetComponent<Text>();
		NewLineName.transform.SetParent(PreviousTextLine.transform.parent,false);
		NameLine.text = ("SpiroFormule"+LineCount.ToString());
		NewLine.name = NameLine.text;
		NewLineName.transform.Find("DeleteButton").gameObject.SetActive(true);
		NewLineName.transform.Find("SelectButton").gameObject.GetComponent<SelectButton>().SelectLine();
		RefreshInputField();
	}
	
	public static void NewSpiro()
	{
	}
	
	public void LoadSpiro(string path)
	{
	}
	
	public void CreateSpiro(SpiroData data)
	{
		DuplicateCurrentSpiro();
		GetActiveLine();
		SelectedLine.GetComponent<SpiroFormule>().data=data;
		SelectedLine.GetComponent<SpiroFormule>().LoadData();
	}
}
