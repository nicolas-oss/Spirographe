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
	SpiroFormule SelectedLine;
	bool isPicking;
	Color newCol;
	string PickedColorString;
	Gradient gradient;
	GradientColorKey[] colorKey;
	GradientAlphaKey[] alphaKey;

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
		gradient = new Gradient();
    }

    public void OnSelect()
	{
		ColorPicking();
	}
	
	public void InitGradient()
	{
		int NbCoul = Spirographe.SelectedLine.NombreCouleur;
        colorKey = new GradientColorKey[NbCoul-1];
		alphaKey = new GradientAlphaKey[NbCoul-1];
		
        for (int i=0; i<NbCoul; i++)
		{
			colorKey[i].color = Spirographe.SelectedLine.couleur[i];
			colorKey[i].time = i/(NbCoul-1);
			alphaKey[i].alpha = 1.0f;
        }

        gradient.SetKeys(colorKey, alphaKey);

        Spirographe.ActiveObjectInScene.GetComponent<LineRenderer>().colorGradient=gradient;
	}
	
	public void RecalculeGradient()
	{
		gradient = new Gradient();
		
		int NbCoul = Spirographe.SelectedLine.NombreCouleur;
		Debug.Log("NbCoul="+NbCoul.ToString());
        colorKey = new GradientColorKey[NbCoul];
		alphaKey = new GradientAlphaKey[NbCoul];
		
        for (int i=0; i<NbCoul; i++)
		{
			colorKey[i].color = Spirographe.SelectedLine.couleur[i];
			colorKey[i].time = i/(NbCoul-1);
			alphaKey[i].alpha = 1.0f;
        }

        gradient.SetKeys(colorKey, alphaKey);

        Spirographe.ActiveObjectInScene.GetComponent<LineRenderer>().colorGradient=gradient;
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
		RecalculeGradient();
	}
	
	public void EndColorPicking()
	{
		isPicking=false;
	}
	
	public void Refresh()
	{
		if (index > Spirographe.SelectedLine.NombreCouleur) 
		{
			gameObject.SetActive(false);
		}
		else
		{
			gameObject.SetActive(true);
			GetComponent<Image>().color = Spirographe.SelectedLine.couleur[index]; 
		}
	}
}
