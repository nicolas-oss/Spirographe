using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class InputFieldAspect : InputFieldInterface
{
	void EcritureValeur(float ValeurOut)
	{
		Spirographe.SelectedAspect.GetType().GetField(InputID).SetValue(Spirographe.SelectedAspect,ValeurOut);
	}
		
	public float LectureValeur()
	{
		return (float)Spirographe.SelectedAspect.GetType().GetField(InputID).GetValue(Spirographe.SelectedAspect);
	}

	/*public void RefreshContent()
	{
		float ValeurCourante;
		if ((dataType=="SpiroFormule") && (Spirographe.SelectedLine==null))
		{
			GetComponent<InputField>().interactable = false;
		}
		else
		{
			GetComponent<InputField>().interactable = true;
			ValeurCourante = (float)Spirographe.SelectedLine.GetType().GetField(InputID).GetValue(Spirographe.SelectedLine); 	//on lit la valeur courante de l'inputID
			GetComponent<InputField>().text = ValeurCourante.ToString(); 								//on l'écrit dans le champ text de l'IF
		}
	}*/
}
