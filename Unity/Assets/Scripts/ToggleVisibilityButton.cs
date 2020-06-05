using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToggleVisibilityButton : MonoBehaviour
{
	Text NameLine;
	public void ToggleVisibility(GameObject TextNameLine)
	{
		bool Visibility;
		NameLine = TextNameLine.GetComponent<Text>();
		Visibility=GameObject.Find(NameLine.text).GetComponent<Renderer>().enabled;
		GameObject.Find(NameLine.text).GetComponent<Renderer>().enabled=!Visibility;
		GameObject.Find(NameLine.text).GetComponent<SpiroFormule>().enabled=!Visibility;
	}
}
