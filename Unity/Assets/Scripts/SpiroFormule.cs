using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpiroFormule : MonoBehaviour
{
    //Parametres entree ancienne fonction Spirographe
	public GameObject Centre;
	public float R1,R2,R3,CX,CY,facteur1,facteur2;
	public bool RotationAxeB;
	public float NombrePoints;
	
	//Parametres animation ondes cercles + crayon
	public float A1,A2,A3,AX,AY,V1,V2,V3,VX,VY,P1,P2,P3,PX,PY;
	public bool OndeR1,OndeR2,OndeR3,OndeCX,OndeCY;
	
	//Nombre de tours
	public float delta;
	public float NombreDeTour,NombreDeTourMaximum;
	public bool Automatique;
	public int IndexFormule;
	
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
	
	GameObject AxeA,AxeB,AxeC,CentreRotation;
	
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
		
		AxeA = new GameObject();
		AxeB = new GameObject();
		AxeC = new GameObject();
		CentreRotation = new GameObject();
    }

    void Update()
    {
        if (Master) Spirographe();
		
		if ((Master) && (Duplication) && !(Attends))
		{
			StartCoroutine(DuplicationMaster());
		}
		if (!(Master))
		{
			GestionClone();
		}
    }
	
	//Duplication

	void GestionClone()
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
	}
	
	IEnumerator DuplicationMaster()
	{
		Attends=true;
		yield return new WaitForSeconds(IntervalDuplication);
		var SpiroClone = Instantiate(gameObject);
		//SpiroClone.AddComponent<SpiroClone>;
		SpiroClone.GetComponent<SpiroFormule>().Master=false;
		Destroy (SpiroClone, DureeVie);
		Attends=false;
	}
	
	//Ondes
	
	public float Onde(float A, float V, float P)
	{
		return A*Mathf.Sin(V*Time.frameCount+P);
	}
	
	//Spirographe
	
	public void Spirographe()
	{
		if (R1==0 || R2==0 || R3==0) {return;}
		float ratio;
		int k,l,NbPtsReel;
		float NbTour;
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		//Debug.Log(lineRenderer);
		lineRenderer.widthMultiplier = widthOfLineRenderer;
		Vector3 PointLine,RotationNulle,DeplacementNul;
	
		AxeC.transform.SetParent(AxeB.transform);
		AxeB.transform.SetParent(AxeA.transform);
		AxeA.transform.SetParent(CentreRotation.transform);

		RotationNulle.x = 0.0f;
		RotationNulle.y = 0.0f;
		RotationNulle.z = 0.0f;
		DeplacementNul=RotationNulle;
		
		transform.localEulerAngles=RotationNulle;
		//Rotation=Rotation+((VitesseRotation*Time.time)+OffsetRotation)*Convert.ToInt32(AnimRotation);
		if (AnimRotation) {Rotation=(VitesseRotation*Time.time)+OffsetRotation;}
		Rotation%=360.0f;
		//Debug.Log("V="+VitesseRotation+" Offset="+OffsetRotation+" bool="+Convert.ToInt32(AnimRotation));
		transform.Rotate(0.0f,Rotation,0.0f);
		
		CentreRotation.transform.position=Centre.transform.position;
		CentreRotation.transform.localEulerAngles=RotationNulle;
		
		AxeA.transform.localPosition = DeplacementNul;
		AxeA.transform.Translate(Echelle*(R1-R2+Convert.ToInt32(OndeR1)*Onde(A1,V1,P1))*Vector3.forward,Space.Self);
		AxeA.transform.localEulerAngles=RotationNulle;
		
		AxeB.transform.localPosition = DeplacementNul;
		AxeB.transform.localEulerAngles=RotationNulle;
		AxeB.transform.Translate(Echelle*(R2-R3+Convert.ToInt32(OndeR2)*Onde(A2,V2,P2))*Vector3.forward,Space.Self);
		
		AxeC.transform.localPosition = DeplacementNul;
		AxeC.transform.localEulerAngles=RotationNulle;
		AxeC.transform.Translate((Echelle+Convert.ToInt32(OndeR3)*Onde(A3,V3,P3))*((CX+Convert.ToInt32(OndeCX)*Onde(AX,VX,PX))*Vector3.left+(CY+Convert.ToInt32(OndeCY)*Onde(AY,VY,PY))*Vector3.forward),Space.Self);
	
		lineRenderer.positionCount = (int)NombrePoints+1;
		
		if (!(Automatique)) {NbTour=NombreDeTour;}
		else
		{
			NbTour=1;
			ratio=(R1/(R2))*facteur1;
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
				lineRenderer.SetPosition(k,AxeC.transform.position);
				CentreRotation.transform.Rotate(0.0f,(360.0f/(NombrePoints/NbTour)),0.0f,Space.Self);
				AxeA.transform.Rotate(0.0f,(-360.0f/(NombrePoints/NbTour)*(R1/R2))*facteur1,0.0f, Space.Self);
				if (RotationAxeB) {AxeB.transform.Rotate(0.0f,(-360.0f/(NombrePoints/NbTour)*(R2/R3))*facteur2,0.0f, Space.Self);}
				lineRenderer.SetPosition(k,AxeC.transform.position);
			}
	}
}