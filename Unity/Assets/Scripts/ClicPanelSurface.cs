using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class ClicPanelSurface : MonoBehaviour
{
	public delegate void BeginDragEvent();
	public delegate void DragEvent();
	public delegate void EndEvent();
    public static event BeginDragEvent FirstDragEvent;
	public static event DragEvent MainDragEvent;
	public static event EndEvent EndDragEvent;

	public void BeginDrag()
	{
		if(FirstDragEvent != null) FirstDragEvent();
	}
	
	public void DragMouse()
    {
		if(MainDragEvent != null) MainDragEvent();
		//Debug.Log("Dragging");
    }
	
	public static void DestroyEvent()
	{
		FirstDragEvent=null;
		MainDragEvent=null;
	}
}