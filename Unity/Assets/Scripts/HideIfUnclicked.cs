using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideIfUnclicked : MonoBehaviour
{
    public GameObject panelDetector;

    void Update()
    {
        HideIfClickedOutside(panelDetector);
    }
	
	private void HideIfClickedOutside(GameObject panel)
	{
		//if (Input.GetMouseButton(0) && panel.activeSelf && !RectTransformUtility.RectangleContainsScreenPoint(panel.GetComponent<RectTransform>(),Input.mousePosition,Camera.main))
		if (Input.GetMouseButton(0) && panel.activeSelf && !RectTransformUtility.RectangleContainsScreenPoint(panel.GetComponent<RectTransform>(),Input.mousePosition,null))
		{
            panel.SetActive(false);
        }
    }
}
