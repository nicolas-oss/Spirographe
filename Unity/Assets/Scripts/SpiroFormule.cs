using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpiroFormule : Spirographe
{
    //Parametres globaux
	public int profondeur;
	public GameObject Centre;
	public float NombrePoints;
	
	//Parametres cercles
	public float[] RR = new float[100];
	public float[] AA = new float[100];
	public float[] VV = new float[100];
	public float[] PP = new float[100];
	public float[] facteurT = new float[100];
	public bool[] OndeRayon = new bool[100];
	public bool[] RotAxe = new bool[100];
	public GameObject[] Axe = new GameObject[100];
	public GameObject[] CentreRayon = new GameObject[100];
	public GameObject AxeToInstatiate;
	
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
	public bool isInitialised=false;
	float alpha = 1.0f;
	Vector3 scaleChange;
	
	Vector3 RotationNulle,DeplacementNul;
	GameObject[] CentreDisque;
	
	void Start()
    {	
		if (!isInitialised) 
		{
			InitValues();
			Initialisation();
			//RefreshInputField();
		}
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
		isInitialised=true;
    }
	
	/*public void InitSize()
	{
		R=new float[profondeur];
		A=new float[profondeur];
		V=new float[profondeur];
		P=new float[profondeur];
		facteur=new float[profondeur];
		OndeR=new bool[profondeur];
		RotAxe=new bool[profondeur];
		Axe=new GameObject[];
	}*/

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
		
    public void InitValues()
	{
		for (int k=0;k<25;k++)
		{
			RR[k]=20.0f-k*(15.0f/100.0f);
			facteurT[k]=1.0f;
			AA[k]=1.0f;
			VV[k]=0.01f;
			PP[k]=0.0f;
			RotAxe[k]=true;
			GameObject go = Instantiate(AxeToInstatiate);
			CentreRayon[k]=go;
		}
		profondeur=3;
		RR[0]=10.0f;
		RR[1]=6.0f;
		RR[2]=1.0f;
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
	
	public float Onde(float AO, float VO, float PO)
	{
		return AO*Mathf.Sin(VO*Time.frameCount+PO);
	}
	
	//Spirographe
	
	public void Spirographe()
	{
		int m;
		/*for (m=0;m<profondeur-1;m++) 
		{
			if (RR[m]!=0) break;
		}
		if (m==profondeur-1) return;*/
		
		float ratio;
		int k,l,NbPtsReel;
		float NbTour;
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.widthMultiplier = widthOfLineRenderer;
		
		CentreRayon[0].transform.position=Centre.transform.position;
		CentreRayon[0].transform.localEulerAngles=RotationNulle;
		
		for (m=1;m<profondeur;m++)
		{
			CentreRayon[m].transform.SetParent(CentreRayon[m-1].transform);
			CentreRayon[m].transform.localEulerAngles=RotationNulle;
			CentreRayon[m].transform.localPosition = DeplacementNul;
			CentreRayon[m].transform.Translate(Echelle*(RR[m-1]-RR[m]+Convert.ToInt32(OndeRayon[m])*Onde(AA[m],VV[m],PP[m]))*Vector3.forward,Space.Self);
			CentreRayon[m].transform.localEulerAngles=RotationNulle;
		}
		
		transform.localEulerAngles=RotationNulle;
		if (AnimRotation) {Rotation=(VitesseRotation*Time.time)+OffsetRotation;}
		Rotation%=360.0f;
		transform.Rotate(0.0f,Rotation,0.0f);
	
		lineRenderer.positionCount = (int)NombrePoints+1;
		
		if (!(Automatique)) {NbTour=NombreDeTour;}
		else
		{
			NbTour=1;
			ratio=(RR[0]/(RR[1]))*facteurT[0];
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
				lineRenderer.SetPosition(k,CentreRayon[profondeur-1].transform.position);
				CentreRayon[0].transform.Rotate(0.0f,(360.0f/(NombrePoints/NbTour)),0.0f,Space.Self);
				for (m=1;m<profondeur-1;m++)
				{
					if (RotAxe[m]) CentreRayon[m].transform.Rotate(0.0f,-(360.0f/(NombrePoints/NbTour)*(RR[m-1]/RR[m]))*facteurT[m],0.0f, Space.Self);
				}
				lineRenderer.SetPosition(k,CentreRayon[profondeur-1].transform.position);
			}
	}
}