using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SmallButtonInterface : MonoBehaviour
{
    public GameObject Interface;
	
	// Start is called before the first frame update
    /*void Start()
    {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick); 
    }
	
	void TaskOnClick()
	{
		Interface.GetComponent<Interface>().SelectButton(gameObject);
		SetActiveEvent();
	}*/
	
	public void SetActiveEvent()
	{
		Interface.GetComponent<Interface>().MainDragEvent.RemoveAllListeners();;
		Interface.GetComponent<Interface>().FirstDragEvent.RemoveAllListeners();
	}
}
