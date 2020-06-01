using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirographe : MonoBehaviour
{
	public delegate void RefreshInputFieldEvent();  //signature de l'event refreshIF
	public delegate void DestroyEventInputFieldEvent();
	public delegate void RefreshInputFieldPanelDisquesEvent();
	public delegate void InitialisationEvent();
    public static event RefreshInputFieldEvent onRefreshInputField; //declaration de l'event suivant la signature précédente
	public static event RefreshInputFieldPanelDisquesEvent onRefreshInputFieldPanelDisques;
	public static event DestroyEventInputFieldEvent onDestroyRefreshInputFieldEvent;
	public static event InitialisationEvent onInitialisation;

	///////////////////////////////////////////Refresh Event////////////////////////////////////////////////
	
	public void RefreshInputField()
	{
		Debug.Log("Calling Refresh");
		if (onRefreshInputField != null) onRefreshInputField();
	}
	
	public void RefreshInputFieldPanelDisques()
	{
		Debug.Log("Calling Event Refresh Panel Disques");
		if (onRefreshInputFieldPanelDisques != null) onRefreshInputFieldPanelDisques();
	}
	
	public void DestroyEventInputField()
	{
		Debug.Log("Calling Event Destroy Event Input Field");
		if(onDestroyRefreshInputFieldEvent != null) onDestroyRefreshInputFieldEvent();
	}
	
	public void Initialisation()
	{
		Debug.Log("Calling event Initialisation");
		if(onInitialisation != null) onInitialisation();
	}
	
	/////////////////////////////////////////GetActiveObject functions//////////////////////////////////////
	
	public GameObject GetActiveObject()
	{
		return GameObject.FindWithTag("Selected");
	}
	
	public SpiroFormule GetActiveSpiroFormule()
	{
		return GetActiveObject().GetComponent<SpiroFormule>();
	}
}
