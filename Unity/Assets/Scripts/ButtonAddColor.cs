using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAddColor : MonoBehaviour
{
    public delegate void ColorAddEvent();
	public static event ColorAddEvent onColorAdd;
	
    void OnEnable()
    {
        Button btn = GetComponent<Button>();
		btn.onClick.AddListener(ColorAdd);
    }
	
	public static void ColorAdd()
	{
		int NbCoul=Spirographe.SelectedAspect.NombreCouleur;
		if (NbCoul<SpiroFormule.TailleTableauCouleur) 
		{
			NbCoul++;
			Spirographe.SelectedAspect.NombreCouleur=NbCoul;
			CallColorAddEvent();
		}
	}
	
	public static void CallColorAddEvent()
	{
		Debug.Log("Calling event ColorAdd");
		if(onColorAdd!= null) onColorAdd();
		Spirographe.SelectedAspect.RecalculeGradient();
	}
}