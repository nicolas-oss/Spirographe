using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRemplissage : MonoBehaviour
{
    // Start is called before the first frame update
    public SpiroFormule spiroFormule;
	public int simplification,stepRemplissage;
	LineRenderer lineRemplissage;	// = gameObject.AddComponent<LineRenderer>();

	//public delegate void RemplissageEvent();
	//public static event RemplissageEvent onRemplissage;

	void Start()
    {
		LineRenderer lineRemplissage = gameObject.AddComponent<LineRenderer>();
		spiroFormule.onPostSpiro += TraceRemplissage;
    }
	
	void onDisable()
	{
		spiroFormule.onPostSpiro -= TraceRemplissage;
	}
	
	void Update()
	{
		//TraceRemplissage();
	}

	public static void Remplissage()
	{
		Debug.Log("Calling event Remplissage");
		//if(onRemplissage!= null) onRemplissage();
	}
	
	void TraceRemplissage()
	{
		LineRenderer lineInitiale = spiroFormule.GetComponent<LineRenderer>();
		lineRemplissage = GetComponent<LineRenderer>();
		
		Gradient gradient;
		gradient = spiroFormule.gameObject.GetComponent<LineRenderer>().colorGradient;
		gameObject.GetComponent<LineRenderer>().colorGradient = gradient;
		
		int NombrePoints = lineInitiale.positionCount;
		lineRemplissage.positionCount = NombrePoints;
		Vector3 pointPosition;
		
		for (int k=0;k<NombrePoints;k+=simplification)
		{
			pointPosition = lineInitiale.GetPosition((int)((k*stepRemplissage*simplification)%(NombrePoints)));
			lineRemplissage.SetPosition(k,pointPosition);
		}

	}
}
