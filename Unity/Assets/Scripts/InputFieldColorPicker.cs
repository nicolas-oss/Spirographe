using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldColorPicker : MonoBehaviour
{
    void OnEnable()
    {
		//MainInputField=gameObject.GetComponent<InputField>();
		gameObject.GetComponent<InputField>().onValueChange.AddListener(delegate {CallColorChangeEvent();}); 
    }
	
	void CallColorChangeEvent()
	{
		Spirographe.ColorChange();
	}
}