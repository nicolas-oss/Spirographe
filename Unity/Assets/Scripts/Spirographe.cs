using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirographe : MonoBehaviour
{
	public delegate void RefreshInputFieldEvent();  //signature de l'event refreshIF
	public delegate void DestroyEventInputFieldEvent();
    public static event RefreshInputFieldEvent onRefreshInputField ; //declaration de l'event suivant la signature précédente
	public static event DestroyEventInputFieldEvent onDestroyRefreshInputFieldEvent;

	///////////////////////////////////////////Refresh Event////////////////////////////////////////////////
	
	public void RefreshInputField()
	{
		Debug.Log("Calling Refresh");
		if (onRefreshInputField != null) onRefreshInputField();
	}
	
	public void DestroyEventInputField()
	{
		if(onDestroyRefreshInputFieldEvent != null) onDestroyRefreshInputFieldEvent();
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
