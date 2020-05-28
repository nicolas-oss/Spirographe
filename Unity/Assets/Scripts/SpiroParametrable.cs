using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpiroParametrable : Spirographe
{
    //Parametres globaux
	public int profondeur;
	public GameObject Centre;
	public float NombrePoints;
	
	//Parametres cercles
	public float[] R,A,V,P,facteur;
	public bool[] OndeR;
	public bool[] RotationAxe;
	public GameObject[] Axe;
	
	//paramètres crayon
	public float AX,AY,VX,VY,PX,PY;
	public bool OndeCX,OndeCY;
	public float CX,CY;

	
	//Nombre de tours
	public float delta;
	public float NombreDeTour,NombreDeTourMaximum;
	public bool Automatique;
	
	public float widthOfLineRenderer;
	
	public float Echelle,Rotation;
	public bool AnimRotation;
	public float VitesseRotation,OffsetRotation;
	
	public bool Duplication,Animate,Fondu;
	public float FacteurAttenuationFondu,FacteurScaleAnimation;
	public float IntervalDuplication,DureeVie;
	bool Attends;
	bool Master=true;
	public Color c1 = Color.yellow;
    public Color c2 = Color.red;
	float alpha = 1.0f;
	Vector3 scaleChange;
	
	Vector3 RotationNulle,DeplacementNul;
	GameObject[] CentreDisque;
	
    void Start()
    {	
		Attends=false;
		if (!(Master))
		{
			Gradient gradient = new Gradient();
			gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f)}, new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) });
			gameObject.GetComponent<LineRenderer>().colorGradient=gradient;
		}
		scaleChange.x = FacteurAttenuationFondu;
		scaleChange.y = FacteurAttenuationFondu;
		scaleChange.z = FacteurAttenuationFondu;
		
		RotationNulle.x = 0.0f;
		RotationNulle.y = 0.0f;
		RotationNulle.z = 0.0f;
		DeplacementNul=RotationNulle;
    }

    void Update()
    {
        /*if (Master) Spirographe();
		
		if ((Master) && (Duplication) && !(Attends))
		{
			StartCoroutine(DuplicationMaster());
		}
		if (!(Master))
		{
			GestionClone();
		}*/
    }
	
	//Duplication

	/*void GestionClone()
	{
		if (Fondu)
		{
			alpha*=FacteurAttenuationFondu;
			Gradient gradient = new Gradient();
			gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f)}, new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) });
			gameObject.GetComponent<LineRenderer>().colorGradient=gradient;
			//GetComponent<lineRenderer>().color=red;
		}
		if (Animate)
		{
			gameObject.transform.localScale*=FacteurScaleAnimation;
		}
	}*/
	
	/*IEnumerator DuplicationMaster()
	{
		Attends=true;
		yield return new WaitForSeconds(IntervalDuplication);
		var SpiroClone = Instantiate(gameObject);
		//SpiroClone.AddComponent<SpiroClone>;
		SpiroClone.GetComponent<SpiroFormule>().Master=false;
		Destroy (SpiroClone, DureeVie);
		Attends=false;
	}*/
	
	//Ondes
	
	public float Onde(float A, float V, float P)
	{
		return A*Mathf.Sin(V*Time.frameCount+P);
	}
	
	//Spirographe
	
	public void Spirographe()
	{
		int m;
		for (m=0;m<profondeur-1;m++) 
		{
			if (R[m]!=0) break;
		}
		if (m==profondeur-1) return;
		
		float ratio;
		int k,l,NbPtsReel;
		float NbTour;
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		//Debug.Log(lineRenderer);
		lineRenderer.widthMultiplier = widthOfLineRenderer;
		
		Axe[0].transform.position=Centre.transform.position;
		Axe[0].transform.localEulerAngles=RotationNulle;
		
		for (m=1;m<profondeur-1;m++)
		{
			Axe[m].transform.SetParent(Axe[m-1].transform);
			Axe[m].transform.localEulerAngles=RotationNulle;
			Axe[m].transform.localPosition = DeplacementNul;
			Axe[m].transform.Translate(Echelle*(R[m]-R[m+1]+Convert.ToInt32(OndeR[m])*Onde(A[m],V[m],P[m]))*Vector3.forward,Space.Self);
			Axe[m].transform.localEulerAngles=RotationNulle;
		}
		
		transform.localEulerAngles=RotationNulle;
		//Rotation=Rotation+((VitesseRotation*Time.time)+OffsetRotation)*Convert.ToInt32(AnimRotation);
		if (AnimRotation) {Rotation=(VitesseRotation*Time.time)+OffsetRotation;}
		Rotation%=360.0f;
		//Debug.Log("V="+VitesseRotation+" Offset="+OffsetRotation+" bool="+Convert.ToInt32(AnimRotation));
		transform.Rotate(0.0f,Rotation,0.0f);
	
		lineRenderer.positionCount = (int)NombrePoints+1;
		
		if (!(Automatique)) {NbTour=NombreDeTour;}
		else
		{
			NbTour=1;
			ratio=(R[0]/(R[1]))*facteur[0];
			for (l=1;l<NombreDeTourMaximum;l++)
			{
				NbTour=l;	
				if (((l*ratio-Mathf.Floor(l*ratio)) < delta) || (NbTour==NombreDeTourMaximum))
				{
					break;
				}
			}
		}
	
		for (k=0;k<NombrePoints+1;k++)
			{
				lineRenderer.SetPosition(k,Axe[profondeur-1].transform.position);
				Axe[0].transform.Rotate(0.0f,(360.0f/(NombrePoints/NbTour)),0.0f,Space.Self);
				for (m=2;m<profondeur-1;m++)
				{
					if (RotationAxe[m]) Axe[m].transform.Rotate(0.0f,(-360.0f/(NombrePoints/NbTour)*(R[m]/R[m+1]))*facteur[m],0.0f, Space.Self);
				}
				lineRenderer.SetPosition(k,Axe[profondeur-1].transform.position);
			}
	}
}