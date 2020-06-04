using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Button loadButton,saveButton;
	string dataPath;
	
	// Start is called before the first frame update
    void Start()
    {
      dataPath=Application.dataPath+"/Spiro/trytosave.xml";
	  //OnEnabled();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public static void CreateSpiro(SpiroData data)
	{
	}
	
	void OnEnable()
	{
		saveButton.onClick.AddListener(delegate{SaveData.Save(dataPath,SaveData.spiroContainer);});
		loadButton.onClick.AddListener(delegate{SaveData.Load(dataPath);});
	}
	
	void OnDisable()
	{
		saveButton.onClick.RemoveListener(delegate{SaveData.Save(dataPath,SaveData.spiroContainer);});
		loadButton.onClick.RemoveListener(delegate{SaveData.Load(dataPath);});
	}
}
