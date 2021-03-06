﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Spirographe
{
	public delegate void RefreshInputFieldEvent();  //signature de l'event refreshIF
	public delegate void DestroyEventInputFieldEvent();
	public delegate void RefreshInputFieldPanelDisquesEvent();
	public delegate void InitialisationEvent();
	public delegate void SelectionLineEvent();
	public delegate void ValueChangeEvent();
	public delegate void ColorChangeEvent();
    public static event RefreshInputFieldEvent onRefreshInputField; //declaration de l'event suivant la signature précédente
	public static event RefreshInputFieldPanelDisquesEvent onRefreshInputFieldPanelDisques;
	public static event DestroyEventInputFieldEvent onDestroyRefreshInputFieldEvent;
	public static event InitialisationEvent onInitialisation;
	public static event SelectionLineEvent onSelectionLine;
	public static event ValueChangeEvent onValueChange;
	public static event ColorChangeEvent onColorChange;
	
	public static SpiroFormule SelectedLine;
	public static Aspect SelectedAspect;
	public static MultiSpiro SelectedMultiSpiro;
	public static GameObject ActiveObjectInScene;

	///////////////////////////////////////////Refresh Event////////////////////////////////////////////////
	
	public static void RefreshInputField()
	{
		//Debug.Log("Calling Refresh");
		if (onRefreshInputField != null) onRefreshInputField();
	}
	
	public static void RefreshInputFieldPanelDisques()
	{
		//Debug.Log("Calling Event Refresh Panel Disques");
		if (onRefreshInputFieldPanelDisques != null) onRefreshInputFieldPanelDisques();
	}
	
	public static void DestroyEventInputField()
	{
		//Debug.Log("Calling Event Destroy Event Input Field");
		if(onDestroyRefreshInputFieldEvent != null) onDestroyRefreshInputFieldEvent();
	}
	
	public static void Initialisation()
	{
		//Debug.Log("Calling event Initialisation");
		if(onInitialisation != null) onInitialisation();
	}
	
	public static void Selection()
	{
		//Debug.Log("Calling event SelectionLine");
		if(onSelectionLine != null) 
			{
				SelectedLine = GetActiveSpiroFormule();
				SelectedAspect = GetActiveAspect();
				SelectedMultiSpiro = GetActiveMultiSpiro();
				ActiveObjectInScene = GetActiveObject();
				onSelectionLine();
			}
	}
	
	public static void ValueChange()
	{
		Debug.Log("Calling event ValueChange");
		if(onValueChange != null) onValueChange();
	}
	
	public static void ColorChange()
	{
		Debug.Log("Calling event ColorChange");
		if(onColorChange!= null) onColorChange();
	}
	
	public static void DestroyColorChangeEvent()
	{
		onColorChange=null;
	}
	
	/////////////////////////////////////////GetActiveObject functions//////////////////////////////////////
	
	public static GameObject GetActiveObject()
	{
		return GameObject.FindWithTag("Selected");
	}
	
	public static SpiroFormule GetActiveSpiroFormule()
	{
		return GetActiveObject().GetComponent<SpiroFormule>();
	}
	
	public static Aspect GetActiveAspect()
	{
		return GetActiveObject().GetComponent<Aspect>();
	}
	
	public static MultiSpiro GetActiveMultiSpiro()
	{
		return GetActiveObject().GetComponent<MultiSpiro>();
	}
}
