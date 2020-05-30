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
	
	public void BeginColorPicking()
	{
		isPicking=!isPicking;
		SelectedLine=GetActiveSpiroFormule();
	}
	
	public void ColorPicking()
	{
		PickedColorString=IFColorPicker.GetComponent<InputField>().text;
		ColorUtility.TryParseHtmlString(PickedColorString, out newCol);
		SelectedLine.GetType().GetField(InputID).SetValue(SelectedLine,newCol);
		ColorFieldToRefresh.GetComponent<Image>().color=newCol;
		Debug.Log(newCol.ToString());
	}
}
