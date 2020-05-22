using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonDisque1 : MonoBehaviour
{
    public GameObject Interface;
	// Start is called before the first frame update
    void Start()
    {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void TaskOnClick()
	{
		Debug.Log("Button R1");
		Interface.GetComponent<Interface>().MainEvent.RemoveAllListeners();
		Interface.GetComponent<Interface>().MainEvent.AddListener(Interface.GetComponent<Interface>().EventR1);
	}
}
