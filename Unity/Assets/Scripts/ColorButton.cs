﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    public GameObject ColorPickerPanel;
	public GameObject IFColorPicker;
	public GameObject ColorFieldToRefresh;
	public int index;
	GameObject ActiveGO;
	SpiroFormule SelectedLine;
	bool isPicking;
	Color newCol;
	string PickedColorString;


	// Start is called before the first frame update
    void OnEnable()
	{
		Spirographe.onSelectionLine += Refresh;
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(BeginColorPicking);
		//btn.onClick.AddListener(BeginColorPicking); 
	}
	
	void OnDisable()
	{
		Spirographe.onSelectionLine -= Refresh;
	}
	
	void Start()
    {
        index=transform.GetSiblingIndex();
		Refresh();
    }

    public void OnSelect()
	{
		ColorPicking();
	}
	
	public void ShowPanel()
	{
		ColorPickerPanel.gameObject.SetActive(true);
	}
	
	public void BeginColorPicking()
	{	
		Debug.Log("BeginColorPicking");
		Spirographe.DestroyColorChangeEvent();
		ShowPanel();
		ColorPickerPanel.GetComponent<ColorPicker>().CurrentColor = Spirographe.SelectedLine.couleur[index];
		Spirographe.onColorChange += ColorPicking;
	}
	
	public void ColorPicking()
	{
		Debug.Log("ColorPicking");
		Color CurrentColor;
		/*PickedColorString=IFColorPicker.GetComponent<InputField>().text;
		ColorUtility.TryParseHtmlString(PickedColorString, out newCol);
		SelectedLine.GetType().GetField(InputID).SetValue(SelectedLine,newCol);*/
		CurrentColor = ColorPickerPanel.GetComponent<ColorPicker>().CurrentColor;
		Spirographe.SelectedLine.couleur[index] = CurrentColor;
		GetComponent<Image>().color=CurrentColor;
		Spirographe.SelectedLine.RecalculeGradient();
	}
	
	public void EndColorPicking()
	{
		isPicking=false;
	}
	
	public void Refresh()
	{
		SelectedLine = Spirographe.GetActiveSpiroFormule();
		int NbCoul = SelectedLine.NombreCouleur;
		Debug.Log("Nombre Coul = "+NbCoul.ToString());
		if (index > NbCoul-1) 
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);
			GetComponent<Image>().color = SelectedLine.couleur[index]; 
		}
	}
}
