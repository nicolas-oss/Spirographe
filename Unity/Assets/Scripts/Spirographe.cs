using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirographe : MonoBehaviour
{
	public GameObject GetActiveObject()
	{
		return GameObject.FindWithTag("Selected");
	}
	
	public SpiroFormule GetActiveSpiroFormule()
	{
		return GetActiveObject().GetComponent<SpiroFormule>();
	}
}
