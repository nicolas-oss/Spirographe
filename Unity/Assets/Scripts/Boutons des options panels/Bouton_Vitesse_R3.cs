using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bouton_Vitesse_R3 : MonoBehaviour
{
	public GameObject Interface;
	public float ValeurInitiale;
	public GameObject InputFieldToRefresh;
	public SpiroFormule SelectedLine;
	
	void Start()
	{
		SelectedLine = Interface.GetComponent<Interface>().SelectedLine;
	}
	
	public void SetActiveEvent()
	{
		Interface.GetComponent<Interface>().FirstDragEvent.AddListener(BeginAjusteWithDrag);
		Interface.GetComponent<Interface>().MainDragEvent.AddListener(AjusteWithDrag);
	}
		
	public void BeginAjusteWithDrag()
	{
		ValeurInitiale = SelectedLine.V3;
	}
	
	public void AjusteWithDrag()
	{
		//string TextToRefresh;
		SelectedLine.V3 = ValeurInitiale + Interface.GetComponent<Interface>().DeltaMousePos.x/10000.0f;
		InputFieldToRefresh.GetComponent<InputField>().text = SelectedLine.V3.ToString();
	}
}
