using System.Collections;
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
	Aspect SelectedAspect;
	bool isPicking;
	Color newCol;
	string PickedColorString;


	// Start is called before the first frame update
    void OnEnable()
	{
		Spirographe.onSelectionLine += Refresh;
		ButtonAddColor.onColorAdd += Refresh;
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(BeginColorPicking);
		//btn.onClick.AddListener(BeginColorPicking); 
	}
	
	void OnDisable()
	{
		Spirographe.onSelectionLine -= Refresh;
		ButtonAddColor.onColorAdd -= Refresh;
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
		//Debug.Log("BeginColorPicking");
		Spirographe.DestroyColorChangeEvent();
		ShowPanel();
		ColorPickerPanel.GetComponent<ColorPicker>().CurrentColor = Spirographe.SelectedAspect.couleur[index];
		Spirographe.onColorChange += ColorPicking;
	}
	
	public void ColorPicking()
	{
		//Debug.Log("ColorPicking");
		Color CurrentColor;
		/*PickedColorString=IFColorPicker.GetComponent<InputField>().text;
		ColorUtility.TryParseHtmlString(PickedColorString, out newCol);
		SelectedLine.GetType().GetField(InputID).SetValue(SelectedLine,newCol);*/
		CurrentColor = ColorPickerPanel.GetComponent<ColorPicker>().CurrentColor;
		Spirographe.SelectedAspect.couleur[index] = CurrentColor;
		GetComponent<Image>().color=CurrentColor;
		Spirographe.SelectedAspect.RecalculeGradient();
	}
	
	public void EndColorPicking()
	{
		isPicking=false;
	}
	
	public void Refresh()
	{
		int NbCoul;
		SelectedAspect = Spirographe.GetActiveAspect();
		if (SelectedAspect!=null) {NbCoul = SelectedAspect.NombreCouleur;} else {NbCoul = 0;}
		{	
			Debug.Log("Nombre Coul = "+NbCoul.ToString());
			if (index > NbCoul-1) 
			{
				gameObject.GetComponent<Button>().enabled=false;
				gameObject.GetComponent<Image>().enabled=false;
			}
			else
			{
				//Debug.Log("Activation");
				gameObject.GetComponent<Button>().enabled=true;
				gameObject.GetComponent<Image>().enabled=true;
				GetComponent<Image>().color = SelectedAspect.couleur[index]; 
			}
		}
	}
}
