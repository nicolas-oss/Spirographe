using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonInterface : MonoBehaviour
{
    public GameObject Interface;
	// Start is called before the first frame update
    void Start()
    {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick); 
    }
	
	void TaskOnClick()
	{
		Debug.Log("Button Interface");
		Interface.GetComponent<Interface>().SelectButton(gameObject);
	}
}
