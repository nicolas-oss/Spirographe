using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class Interface : MonoBehaviour
{
    public SpiroFormule SelectedLine;
	
	public Color UnselectedColor,SelectedColor;
	
	public UnityEvent MainDragEvent,FirstDragEvent;
	public GameObject ButtonEchelle,ButtonRotation,ButtonDisque1,ButtonTousDisques,ButtonDisque2,ButtonDisque3,ButtonCrayon,ButtonDuplication;
	public GameObject PanelOptionButtonEchelle,PanelOptionButtonRotation,PanelOptionButtonTousDisques,PanelOptionButtonDisque1,PanelOptionButtonDisque2,PanelOptionButtonDisque3,PanelOptionButtonCrayon,PanelOptionNombreDeTours,PanelOptionButtonDuplication;
	public GameObject Surface;
	public GameObject InputEchelle,InputRotation,InputR1,InputR2,InputR3,InputCX,InputCY;
	public Vector3 DeltaMousePos;
	GameObject ActiveButton;
	//public GameObject PreviousSelectedButton;
	public float CurrentEchelle,CurrentRotation,CurrentSizeDisque1,CurrentSizeDisque2,CurrentSizeDisque3,CurrentSizeCX,CurrentSizeCY;
	
	////////////////////////////////////////////////////////////////////////////  Events  ////////////////////////////////
	
	/*public void CliqueSurfaceDeTravail()
	{
		Debug.Log(DeltaMousePos);
	}*/
	
	public void BeginAjusteEchelleWithDrag()
	{
		CurrentEchelle = SelectedLine.Echelle;
	}
	
	public void AjusteEchelleWithDrag()
	{
		SelectedLine.Echelle = CurrentEchelle + DeltaMousePos.x/100.0f;
		InputEchelle.GetComponent<InputField>().text = SelectedLine.Echelle.ToString();
	}
	
	public void BeginAjusteRotationWithDrag()
	{
		CurrentRotation = SelectedLine.Rotation;
	}
	
	public void AjusteRotationWithDrag()
	{
		SelectedLine.Rotation = CurrentRotation + DeltaMousePos.x/10.0f;
		InputRotation.GetComponent<InputField>().text = SelectedLine.Rotation.ToString();
	}
	
	public void BeginAjusteDisque1WithDrag()
	{
		CurrentSizeDisque1 = SelectedLine.R1;
	}
	
	public void AjusteDisque1WithDrag()
	{
		SelectedLine.R1 = (float)Math.Floor(10.0f*(CurrentSizeDisque1 + DeltaMousePos.x/100.0f))/10.0f;
		InputR1.GetComponent<InputField>().text = SelectedLine.R1.ToString();
	}
	
	public void BeginAjusteDisque2WithDrag()
	{
		CurrentSizeDisque2 = SelectedLine.R2;
	}
	
	public void AjusteDisque2WithDrag()
	{
		SelectedLine.R2 = (float)Math.Floor(10.0f*(CurrentSizeDisque2 + DeltaMousePos.x/100.0f))/10.0f;
		InputR2.GetComponent<InputField>().text = SelectedLine.R2.ToString();
	}
		
	public void BeginAjusteDisque3WithDrag()
	{
		CurrentSizeDisque3 = SelectedLine.R3;
	}
	
	public void AjusteDisque3WithDrag()
	{
		SelectedLine.R3 = (float)Math.Floor(10.0f*(CurrentSizeDisque3 + DeltaMousePos.x/100.0f))/10.0f;
		InputR3.GetComponent<InputField>().text = SelectedLine.R3.ToString();
	}
		
	public void BeginAjusteCXYWithDrag()
	{
		CurrentSizeCX = SelectedLine.CX;
		CurrentSizeCY = SelectedLine.CY;
	}
	
	public void AjusteCXYWithDrag()
	{
		SelectedLine.CX = (float)Math.Floor(10.0f*(CurrentSizeCX + DeltaMousePos.x/100.0f))/10.0f;
		SelectedLine.CY = (float)Math.Floor(10.0f*(CurrentSizeCY + DeltaMousePos.y/100.0f))/10.0f;
		InputCX.GetComponent<InputField>().text = SelectedLine.CX.ToString();
		InputCY.GetComponent<InputField>().text = SelectedLine.CY.ToString();
	}
			
	public void BeginAjusteCYWithDrag()
	{
		CurrentSizeCY = SelectedLine.CY;
	}
	
	public void AjusteCYWithDrag()
	{
		
		
	}
}
