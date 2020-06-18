using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRemoveColor : MonoBehaviour
{	
    void OnEnable()
    {
        Button btn = GetComponent<Button>();
		btn.onClick.AddListener(ColorRemove);
    }
	
	public static void ColorRemove()
	{
		int NbCoul=Spirographe.SelectedLine.NombreCouleur;
		if (NbCoul>1) 
		{
			NbCoul--;
			Spirographe.SelectedLine.NombreCouleur=NbCoul;
			//Debug.Log("Calling event ColorAdd");
			ButtonAddColor.CallColorAddEvent();
		}
	}
}