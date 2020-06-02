using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ColorButton : Spirographe
{
    public ColorPicker ColorPickerPanel;
	public GameObject IFColorPicker;
	public GameObject ColorFieldToRefresh;
	public string InputID;
	GameObject ActiveGO;
	SpiroFormule SelectedLine;
	bool isPicking;
	Color newCol;
	string PickedColorString;

	// Start is called before the first frame update
    void Start()
    {
        isPicking=false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isPicking) ColorPicking();
    }
	
	public void ShowPanel()
	{
		ColorPickerPanel.gameObject.SetActive(true);
	}
	
	public void BeginColorPicking()
	{	
		//ShowPanel();
		SelectedLine=GetActiveSpiroFormule();
		//if (!isPicking) 
		ColorPickerPanel.GetComponent<ColorPicker>().CurrentColor=ColorFieldToRefresh.GetComponent<Image>().color;
		isPicking=true;
		//IFColorPicker.GetComponent<InputField>().text="#"+ColorUtility.ToHtmlStringRGBA(ColorFieldToRefresh.GetComponent<Image>().color);
	}
	
	public void ColorPicking()
	{
		/*PickedColorString=IFColorPicker.GetComponent<InputField>().text;
		ColorUtility.TryParseHtmlString(PickedColorString, out newCol);
		SelectedLine.GetType().GetField(InputID).SetValue(SelectedLine,newCol);*/
		ColorFieldToRefresh.GetComponent<Image>().color=ColorPickerPanel.GetComponent<ColorPicker>().CurrentColor;
	}
	
	public void EndColorPicking()
	{
		isPicking=false;
	}
}
