using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClicPanelSurface : MonoBehaviour
{
    public Vector3 CurrentMousePos,MousePosInitiale,DeltaMousePos;

	public void BeginDrag()
	{
		MousePosInitiale = Input.mousePosition;
		Debug.Log("Init POs = "+MousePosInitiale);
	}
	
	public void DragMouse()
    {
        CurrentMousePos = Input.mousePosition;
		DeltaMousePos = CurrentMousePos-MousePosInitiale;
		Debug.Log("Delta = "+DeltaMousePos);
    }
}
