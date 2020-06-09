using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpiroFormule : MonoBehaviour
{
    public SpiroData data = new SpiroData();
	
	//Parametres globaux
	public int profondeur;
	//public GameObject Centre;
	public float NombrePoints;
	
	public static int TailleTableaux = 25;
	//Parametres cercles
	public float[] RR = new float[TailleTableaux];
	public float[] RROffset = new float[TailleTableaux];
	public float[] AA = new float[TailleTableaux];
	public float[] VV = new float[TailleTableaux];
	public float[] PP = new float[TailleTableaux];
	public float[] facteurT = new float[TailleTableaux];
	public bool[] OndeRayon = new bool[TailleTableaux];
	public bool[] RotAxe = new bool[TailleTableaux];
	//public GameObject[] Axe = new GameObject[TailleTableaux];
	public GameObject[] CentreRayon = new GameObject[TailleTableaux];
	//GameObject AxeToInstatiate;
	public GameObject Centre;
	
	//paramètres crayon
	//public float CX,CY;
	
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
	public bool z;
	public float profondeur_z;
	bool Attends;
	public bool Master;//=true;
	public Color c1 = Color.yellow;
    public Color c2 = Color.red;
	public bool isInitialised;//=false;
	float alpha = 1.0f;
	Vector3 scaleChange;
	public bool animation,toRefresh;
	
	Vector3 RotationNulle,DeplacementNul;
	//GameObject[] CentreDisque;
	
	public int NombreCouleur;
	public static int TailleTableauCouleur = 8;
	public Color[] couleur = new Color[TailleTableauCouleur];
	public Gradient gradient;
	public GradientColorKey[] colorKey; //= new GradientColorKey[TailleTableauCouleur];
	public GradientAlphaKey[] alphaKey; //= new GradientAlphaKey[TailleTableauCouleur];
	
	public void StoreData()
	{
		if (Master)
		{
			data.profondeur=profondeur;
			data.NombrePoints=NombrePoints;
			data.delta=delta;
			data.NombreDeTour=NombreDeTour;
			data.NombreDeTourMaximum=NombreDeTourMaximum;
			data.Automatique=Automatique;
			data.widthOfLineRenderer=widthOfLineRenderer;
			data.Echelle=Echelle;
			data.Rotation=Rotation;
			data.AnimRotation=AnimRotation;
			data.VitesseRotation=VitesseRotation;
			data.OffsetRotation=OffsetRotation;
			data.Duplication=Duplication;
			data.Animate=Animate;
			data.Fondu=Fondu;
			data.FacteurAttenuationFondu=FacteurAttenuationFondu;
			data.FacteurScaleAnimation=FacteurScaleAnimation;
			data.IntervalDuplication=IntervalDuplication;
			data.DureeVie=DureeVie;
			data.Master=Master;
			data.isInitialised=isInitialised;
			data.z=z;
			data.profondeur_z=profondeur_z;
			for (int i=0;i<TailleTableaux-1;i++)
			{
				data.RR[i]=RR[i];
				data.RROffset[i] = RROffset[i];
				data.AA[i]=AA[i];
				data.VV[i]=VV[i];
				data.PP[i]=PP[i];
				data.facteurT[i]=facteurT[i];
				data.OndeRayon[i]=OndeRayon[i];
				data.RotAxe[i]=RotAxe[i];
			}
			data.NombreCouleur=NombreCouleur;
			for (int i=0;i<TailleTableauCouleur;i++)
			{
				data.couleur[i] = couleur[i];
			}
			Debug.Log("Values copied");
		}
	}
	
	public void LoadData()
	{
		profondeur=data.profondeur;
		NombrePoints=data.NombrePoints;
		delta=data.delta;
		NombreDeTour=data.NombreDeTour;
		NombreDeTourMaximum=data.NombreDeTourMaximum;
		Automatique=data.Automatique;
		widthOfLineRenderer=data.widthOfLineRenderer;
		Echelle=data.Echelle;
		Rotation=data.Rotation;
		AnimRotation=data.AnimRotation;
		VitesseRotation=data.VitesseRotation;
		OffsetRotation=data.OffsetRotation;
		Duplication=data.Duplication;
		Animate=data.Animate;
		Fondu=data.Fondu;
		FacteurAttenuationFondu=data.FacteurAttenuationFondu;
		FacteurScaleAnimation=data.FacteurScaleAnimation;
		IntervalDuplication=data.IntervalDuplication;
		DureeVie=data.DureeVie;
		Master=data.Master;
		isInitialised=data.isInitialised;
		z=data.z;
		profondeur_z = data.profondeur_z;
		for (int i=0;i<TailleTableaux-1;i++)
		{
			RR[i]=data.RR[i];
			RROffset[i] = data.RROffset[i];
			AA[i]=data.AA[i];
			VV[i]=data.VV[i];
			PP[i]=data.PP[i];
			facteurT[i]=data.facteurT[i];
			OndeRayon[i]=data.OndeRayon[i];
			RotAxe[i]=data.RotAxe[i];
		}
		NombreCouleur=data.NombreCouleur;
		for (int i=0;i<TailleTableauCouleur;i++)
		{
			couleur[i] = data.couleur[i];
		}
	}
	
	void OnEnable()
	{
		//SaveData.OnLoaded += delegate{LoadData();};
		//if (Master) 
		//{
			SaveData.OnBeforeSave += delegate{StoreData();};
			SaveData.OnBeforeSave += delegate{SaveData.AddSpiroData(data);};
			Debug.Log("Save Event added");
			Spirographe.onValueChange += GestionAnimation;
		//}		
	}
	
	public void OnDisable()
	{
		//SaveData.OnLoaded -= delegate{LoadData();};
		//if (Master) 
		//{
			//SaveData.ClearSpiros();
			SaveData.OnBeforeSave -= delegate{StoreData();};
			SaveData.OnBeforeSave -= delegate{SaveData.AddSpiroData(data);};
			Spirographe.onValueChange -= GestionAnimation;
			Debug.Log("Souscriptions du spiro annulées");
		//}
	}
	
	void Start()
    {	
		//GameObject AxeToInstatiate = new GameObject();
		if (!isInitialised) 
		{
			//InitValues();
			//Initialisation();
			GameObject Centre = new GameObject();
			GameObject AxeToInstatiate = new GameObject();
			for (int k=0;k<TailleTableaux-1;k++)
			{
				GameObject go = Instantiate(AxeToInstatiate);
				CentreRayon[k]=go;
			}
			//RefreshInputField();
		}
		
		Attends=false;
		if (!(Master))
		{
			Gradient gradient = new Gradient();
			gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f)}, new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) });
			gameObject.GetComponent<LineRenderer>().colorGradient=gradient;
			
			SaveData.OnBeforeSave -= delegate{StoreData();};
			SaveData.OnBeforeSave -= delegate{SaveData.AddSpiroData(data);};
		}
		scaleChange.x = FacteurAttenuationFondu;
		scaleChange.y = FacteurAttenuationFondu;
		scaleChange.z = FacteurAttenuationFondu;
		
		RotationNulle.x = 0.0f;
		RotationNulle.y = 0.0f;
		RotationNulle.z = 0.0f;
		DeplacementNul=RotationNulle;
		isInitialised=true;
		animation=CheckAnimation(); // est-ce animé ?
		RecalculeGradient();
		
		TraceSpirographe();
    }

	public void RecalculeGradient()
	{		
		gradient = new Gradient();
		colorKey = new GradientColorKey[NombreCouleur];
		alphaKey = new GradientAlphaKey[NombreCouleur];

		for (int i=0; i<NombreCouleur; i++)
		{
			colorKey[i].color = couleur[i];
			colorKey[i].time = (1.0f*i)/(1.0f*NombreCouleur);
			alphaKey[i].alpha = 1.0f;
			alphaKey[i].time = (1.0f*i)/(1.0f*NombreCouleur);
        }
		
        gradient.SetKeys(colorKey, alphaKey);
        gameObject.GetComponent<LineRenderer>().colorGradient=gradient;
	}
	
	public void GestionAnimation()
	{
		//animation=CheckAnimation();
		if (tag=="Selected") 
		{
			animation=CheckAnimation();
			ApplyRotation();
			TraceSpirographe();
		}
	}	
	
	public bool CheckAnimation()
	{
		bool check=false;
		for (int i=0;i<profondeur;i++)
		{
			check |= OndeRayon[i];
		}
		return check;
	}
	
	public void ApplyRotation()
	{
		transform.localEulerAngles=RotationNulle;
		if (AnimRotation) {Rotation=(VitesseRotation*Time.time)+OffsetRotation;}
		Rotation%=360.0f;
		transform.Rotate(0.0f,Rotation,0.0f);
	}

    void Update()
    {
        if ((AnimRotation) && (Master)) ApplyRotation();
		//if (Master) 
		if ((animation) && (Master)) TraceSpirographe();
		
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
		/*for (int k=0;k<25;k++)
		{
			/*RR[k]=20.0f-k*(15.0f/100.0f);
			facteurT[k]=1.0f;
			AA[k]=1.0f;
			VV[k]=0.01f;
			PP[k]=0.0f;
			RotAxe[k]=true;
		}
		profondeur=3;
		RR[0]=10.0f;
		RR[1]=6.0f;
		RR[2]=1.0f;*/
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
		SpiroClone.GetComponent<SpiroFormule>().OnDisable();
		Destroy (SpiroClone, DureeVie);
		Attends=false;
	}
	
	//Ondes
	
	public float Onde(float AO, float VO, float PO)
	{
		return AO*Mathf.Sin(VO*Time.frameCount+PO);
	}
	
	//TraceSpirographe
	
	public void TraceSpirographe()
	{
		int m;
		/*for (m=0;m<profondeur-1;m++) 
		{
			if (RR[m]!=0) break;
		}
		if (m==profondeur-1) return;*/
		
		float ratio;
		int k,l;//,NbPtsReel;
		float NbTour;
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.widthMultiplier = widthOfLineRenderer;
		
		//GameObject Centre = new GameObject();
		CentreRayon[0].transform.position=Centre.transform.position;
		CentreRayon[0].transform.localEulerAngles=RotationNulle;
		
		for (m=1;m<profondeur;m++)
		{
			CentreRayon[m].transform.SetParent(CentreRayon[m-1].transform);
			CentreRayon[m].transform.localEulerAngles=RotationNulle;
			CentreRayon[m].transform.localPosition = DeplacementNul;
			CentreRayon[m].transform.Translate(Echelle*(RR[m-1]-RR[m]+RROffset[m]+Convert.ToInt32(OndeRayon[m])*Onde(AA[m],VV[m],PP[m]))*Vector3.forward,Space.Self);
			CentreRayon[m].transform.localEulerAngles=RotationNulle;
		}
		
		//ApplyRotation();
	
		lineRenderer.positionCount = (int)NombrePoints+1;
		
		if (!(Automatique)) {NbTour=NombreDeTour;}
		else
		{
			NbTour=1;
			ratio=(RR[0]/(RR[1]))*facteurT[1];
			for (l=1;l<NombreDeTourMaximum;l++)
			{
				NbTour=l;	
				if (((l*ratio-Mathf.Floor(l*ratio)) < delta) || (NbTour==NombreDeTourMaximum))
				{
					break;
				}
			}
		}
	
		float step_profondeur=profondeur_z/NombrePoints;
		
		for (k=0;k<NombrePoints+1;k++)
			{
				lineRenderer.SetPosition(k,CentreRayon[profondeur-1].transform.position);
				CentreRayon[0].transform.Rotate(0.0f,(360.0f/(NombrePoints/NbTour)),0.0f,Space.Self);
				for (m=1;m<profondeur-1;m++)
				{
					if (RotAxe[m]) CentreRayon[m].transform.Rotate(0.0f,-(360.0f/(NombrePoints/NbTour)*(RR[m-1]/RR[m]))*facteurT[m],0.0f, Space.Self);
				}
				lineRenderer.SetPosition(k,CentreRayon[profondeur-1].transform.position);
				if (z) CentreRayon[profondeur-1].transform.Translate((step_profondeur)*Vector3.up,Space.Self);
			}
	}
}