using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputFieldInterface : MonoBehaviour
{
    public GameObject Interface;
	
	// Start is called before the first frame update
    /*void Start()
    {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick); 
    }*/
	
	void Start()
	{
		SelectedLine = Interface.GetComponent<Interface>().SelectedLine;
	}
	
	/*void TaskOnClick()
	{
		Interface.GetComponent<Interface>().SelectButton(gameObject);
		SetActiveEvent();
	}*/
	
	public void SetActiveEvent()
	{
		Interface.GetComponent<Interface>().MainDragEvent.RemoveAllListeners();;
		Interface.GetComponent<Interface>().FirstDragEvent.RemoveAllListeners();
		Interface.GetComponent<Interface>().FirstDragEvent.AddListener(delegate
        {
            BeginAjusteWithDrag(string InputID);
        });
		Interface.GetComponent<Interface>().MainDragEvent.AddListener(AjusteWithDrag);
	}
	
	public void BeginAjusteWithDrag(string InputID)
	{
		ValeurInitiale = SelectedLine.A1;
		switch (InputID)
      {
         case "A1": ValeurInitiale = SelectedLine.A1;
            break;
         case "V1": ValeurInitiale = SelectedLine.V1;
            break;
         case "P1": ValeurInitiale = SelectedLine.P1;
            break;
      }
	}
}
