﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Calculs_Rayons : MonoBehaviour
{
    public GameObject Root,DisquePrincipal,DisqueSecondaire,Rayon_1,Rayon_2,PositionInitialeAxe1,PositionInitialeAxe2,AxeRotation1,AxeRotation2,Crayon;
	public SpiroFormule SelectedLine;//,SpiroFormule; //CurrentLine,
	public float Echelle;
	public float RayonMaximal;
	public float RayonDisque2;
	float RayonPrincipal;
	public float Rayon3;
	public bool RotationAxeB;
	public float CrayonX,CrayonY;
	public float FacteurTransmission2;

	public bool OndeR1,OndeR2,OndeR3,OndeCrayonX,OndeCrayonY;
	public float A1,V1,P1;
	public float HauteurDisque;
	public float DureeAnimation;
	public float delta;
	public int NombreDeTour,NombreDeTourMaximum;
	public bool Automatique;
	public int IndexFormule;
	Vector3 ScaleDisque1,ScaleDisque2,PosRayon1,PosRayon2;
	Animator AnimatorAxe1,AnimatorAxe2;
	
	public int lengthOfLineRenderer;
	public float widthOfLineRenderer;
	public Color c1 = Color.yellow;
    public Color c2 = Color.red;
	public Color selected = Color.black;
	public Color unselected = Color.gray;
	
	Vector3 RotationAxe1,RotationAxe2;
	float CoefTransmission;
	Vector3 PointLineRenderer,PointInitial,posCrayon;
	
	int i=0;
	int LineCount=1;
	bool Play=true;
	
	GameObject CentreRotation,AxeA,AxeB,AxeC;
	
	Text NameLine;
	public GameObject PreviousLine;

	Color ColorColorPicker;
	float Luminosite;
	
			public float AmplitudeOnde1,AmplitudeOnde2;
			public float VitesseOnde1,VitesseOnde2,PhaseOnde1,PhaseOnde2;
			
	public bool Trainee;
	
	// Start is called before the first frame update
    public void Start()
    {
        AxeA = new GameObject();
		AxeB = new GameObject();
		AxeC = new GameObject();
		CentreRotation = new GameObject();
		
		//GetAnimators
		AnimatorAxe1 = AxeRotation1.GetComponent<Animator>();
		AnimatorAxe2 = AxeRotation2.GetComponent<Animator>();
		
		//LineRenderer
		LineRenderer lineRenderer = SelectedLine.GetComponent<LineRenderer>();
		//lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		
		// A simple 2 color gradient with a fixed alpha of 1.0f.
        /*float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;*/
		lineRenderer.widthMultiplier = widthOfLineRenderer;
		
		//Calcul Rayons et initialisation lineRenderer
		//AjusteRayonDisque1();
		//AjusteRayonDisque2();
		//AjusteDureeAnims();
		//ResetCurrentLineRenderer();
		//JoueAnim();
		Time.fixedDeltaTime=0.02f;
    }

    void Update()
    { 
	  //Calcul points
		//LineRenderer lineRenderer = SelectedLine.GetComponent<LineRenderer>();
		
		//RayonPrincipal=RayonMaximal-RayonDisque2;
		//var SpiroTemp = Instantiate(SpiroFormule);
		
		
	/*if (Play==true) 
		{/*
			if (i==lengthOfLineRenderer || ((PointLineRenderer-PointInitial).magnitude<0.2 && (PositionInitialeAxe1.transform.position-AxeRotation1.transform.position).magnitude<0.2 && (PositionInitialeAxe2.transform.position-AxeRotation2.transform.position).magnitude<0.2 && i>50))  //Stop anim si on revient au départ
				{
					lineRenderer.SetPosition(i-1,PointInitial); //on ferme la courbe
					StopAnim();
				}
			else
				{
				i++;
				PointLineRenderer = Crayon.transform.position;
				lineRenderer.positionCount = i;
				lineRenderer.SetPosition(i-1,PointLineRenderer);
		AjusteRayonDisque1();
		AjusteRayonDisque2();
		AjusteCrayon();
		AjusteDureeAnims();
				}		*/
		//}
	}
	
	public void FixPointLineRenderer(LineRenderer lineRenderer, int debut, int fin, Vector3 PointPosition)
	{
			for (int k = debut-1; k < fin; k++)
			{
				lineRenderer.SetPosition(k,PointPosition);
			}
	}
	
	public void StopAnim()
	{
		AnimatorAxe1.SetBool("PlayRotationDisque1",false);
		AnimatorAxe2.SetBool("PlayRotationDisque2",false);
		Play=false;
	}
	
	public void JoueAnim()
	{
		AnimatorAxe1.SetBool("PlayRotationDisque1",true);
		AnimatorAxe2.SetBool("PlayRotationDisque2",true);
		Play=true;
	}
	
	public void ResetCurrentLineRenderer()
	{
		LineRenderer lineRenderer = SelectedLine.GetComponent<LineRenderer>();
		//initialisation compteur
		i=1;
		lineRenderer.positionCount = 1;
		//définition premier point linerenderer
		PointLineRenderer = Crayon.transform.position;
		lineRenderer.SetPosition(0,PointLineRenderer);
		//debut animation
		PointInitial=PointLineRenderer;
		PositionInitialeAxe1=AxeRotation1;
		PositionInitialeAxe2=AxeRotation2;
		
		IndexFormule=0;
	}

	public void NewLineRenderer()
	{
		bool Visibility;
		var NewLine = Instantiate(SelectedLine);
		GameObject BoutonDelete;
		LineCount++;
		
		var NewLineName = Instantiate(GameObject.Find("TextBase"));
		NameLine = NewLineName.GetComponent<Text>();
		NewLineName.transform.SetParent(GameObject.Find("ListSpiro").transform,false);
		NameLine.text = ("SpiroFormule"+LineCount.ToString());
		NewLine.name = NameLine.text;
		NewLineName.transform.Find("DeleteButton").gameObject.SetActive(true);
		SelectLine(NewLineName);
		ResetCurrentLineRenderer();
		StopAnim();
	}
	
	public void DeleteLine(GameObject TextNameLineToDelete)
	{
		int NbChildren,LineToActivate;
		GameObject NewTextNameLine;
		
		NbChildren = GameObject.Find("ListSpiro").transform.childCount;
		if (NbChildren>1)
		{
			NameLine = TextNameLineToDelete.GetComponent<Text>();
			//Debug.Log("childCount="+NbChildren);
			//Debug.Log("SiblingIndex="+TextNameLineToDelete.transform.GetSiblingIndex());
			if (TextNameLineToDelete.transform.GetSiblingIndex()==(NbChildren-1))
			{
				LineToActivate = NbChildren-2;
			}
			else
			{
				LineToActivate = NbChildren-1;
			}
			SelectedLine=GameObject.Find(GameObject.Find("ListSpiro").transform.GetChild(NbChildren-1).gameObject.GetComponent<Text>().text).GetComponent<SpiroFormule>();
			Destroy(GameObject.Find(NameLine.text));
			Destroy(TextNameLineToDelete);
			NewTextNameLine = GameObject.Find("ListSpiro").transform.GetChild(LineToActivate).gameObject;
			SelectLine(NewTextNameLine);
		}
	}
	
	public void ToggleVisibility(GameObject TextNameLine)
	{
		bool Visibility;
		NameLine = TextNameLine.GetComponent<Text>();
		Visibility=GameObject.Find(NameLine.text).GetComponent<Renderer>().enabled;
		GameObject.Find(NameLine.text).GetComponent<Renderer>().enabled=!Visibility;
	}
	
	public void SelectLine(GameObject TextNameLine)
	{
		if (PreviousLine==null) {PreviousLine=TextNameLine;}
		//if (SelectedLine==null) {SelectedLine=PreviousLine;}
		NameLine = TextNameLine.GetComponent<Text>();
		PreviousLine.GetComponent<Text>().color = unselected;
		TextNameLine.GetComponent<Text>().color = selected;
		SelectedLine = GameObject.Find(NameLine.text).GetComponent<SpiroFormule>();
		PreviousLine = TextNameLine;
		StopAnim();
	}

	public void AjusteRayonDisque1()
	{
		//Calcul et affectation Rayon Principal
		RayonPrincipal=RayonMaximal-RayonDisque2+OndeSinus();
		//positionnnement par rapport à l'origine
		PosRayon1= new Vector3(RayonPrincipal,0.0f,0.0f);
		Rayon_1.transform.localPosition = PosRayon1;  
		//échelle du disque
		ScaleDisque1 = new Vector3(RayonMaximal*2,HauteurDisque,RayonMaximal*2);
		DisquePrincipal.transform.localScale = ScaleDisque1;
	}
	
	public void AjusteRayonDisque2()
	{	
		//positionnement par rapport au disque 1
		PosRayon2= new Vector3(RayonDisque2+OndeCosinus(),0.0f,0.0f);
	    Rayon_2.transform.localPosition = PosRayon2;
		//échelle du disque 2
		ScaleDisque2 = new Vector3(RayonDisque2*2,HauteurDisque,RayonDisque2*2);
		DisqueSecondaire.transform.localScale = ScaleDisque2;
	}
	
	public void AjusteCrayon()
	{
		posCrayon = new Vector3(CrayonX,0.0f,CrayonY);
		Crayon.transform.localPosition = posCrayon;
	}
	
	public float OndeSinus()
	{
		return AmplitudeOnde1*Mathf.Sin(VitesseOnde1*Time.frameCount+PhaseOnde1);
	}
	
	public float OndeCosinus()
	{
		return AmplitudeOnde2*Mathf.Sin(VitesseOnde2*Time.frameCount+PhaseOnde2);
	}
	
	public float Onde()
	{
		return A1*Mathf.Sin(V1*Time.frameCount+P1);
	}
	
	public void AjusteDureeAnims()
	{
		AnimatorAxe1.SetFloat("DureeRotationPrincipale",1.0f/DureeAnimation);
		AnimatorAxe2.SetFloat("DureeRotationSecondaire",1.0f/(DureeAnimation*RayonDisque2/(RayonMaximal*SelectedLine.FacteurTransmission1)));
	}
	
	public void SliderRayonDisque1()
	{
		RayonMaximal = GameObject.Find("SliderRayonDisque1").GetComponent <Slider> ().value;	
		AjusteRayonDisque1();		
		ResetCurrentLineRenderer();
		JoueAnim();
	}
	
	public void SliderRayonDisque2()
	{		
		RayonDisque2 = GameObject.Find("SliderRayonDisque2").GetComponent <Slider> ().value;
		//Calcul et affectation Rayon secondaire
		AjusteRayonDisque2();	
		ResetCurrentLineRenderer();
		JoueAnim();
	}
	
	public void SliderTransmission()
	{
		SelectedLine.FacteurTransmission1 = GameObject.Find("SliderFacteurTransmission").GetComponent <Slider> ().value;
		AjusteDureeAnims();
		ResetCurrentLineRenderer();
		JoueAnim();
	}
	
	public void SliderLargeurTrait()
	{
		widthOfLineRenderer = GameObject.Find("SliderLargeurTrait").GetComponent <Slider> ().value;
		SelectedLine.GetComponent<LineRenderer>().widthMultiplier = widthOfLineRenderer;
	}
	
	public void SliderPalette()
	{
		Luminosite = GameObject.Find("SliderPalette").GetComponent <Slider> ().value;
		GameObject.Find("ImageColorPicker").GetComponent<Image>().color=Color.HSVToRGB(0,0,Luminosite);
	}
	
	public Vector2 Cercle(float t)
	{
		float x,y;
		Vector2 coordonnees;
		coordonnees.x=Mathf.Cos(t);
		coordonnees.y=Mathf.Sin(t);
		return coordonnees;
	}
	
	public void Spirographe(SpiroFormule Line,GameObject Centre,float R1,float R2,float R3, float CX, float CY, float facteur1, float facteur2, int NombrePoints)
	{
		if (R1==0 || R2==0 || R3==0) {return;}
		float ratio;
		int k,l,NbTour;
		int NbTourMax=100;
		LineRenderer lineRenderer = Line.GetComponent<LineRenderer>();
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
		
		CentreRotation.transform.position=Centre.transform.position;
		CentreRotation.transform.localEulerAngles=RotationNulle;
		
		AxeA.transform.localPosition = DeplacementNul;
		AxeA.transform.Translate(Echelle*(R1-R2+Convert.ToInt32(OndeR1)*OndeSinus())*Vector3.forward,Space.Self);
		AxeA.transform.localEulerAngles=RotationNulle;
		
		AxeB.transform.localPosition = DeplacementNul;
		AxeB.transform.localEulerAngles=RotationNulle;
		AxeB.transform.Translate(Echelle*(R2-R3+Convert.ToInt32(OndeR2)*OndeCosinus())*Vector3.forward,Space.Self);
		AxeB.transform.Translate(Echelle*(R2-R3+Convert.ToInt32(OndeR2)*OndeCosinus())*Vector3.forward,Space.Self);
		
		AxeC.transform.localPosition = DeplacementNul;
		AxeC.transform.localEulerAngles=RotationNulle;
		AxeC.transform.Translate((Echelle+Convert.ToInt32(OndeR3)*Onde())*(CX*Vector3.left+CY*Vector3.forward),Space.Self);
	
		lineRenderer.positionCount = NombrePoints+IndexFormule;
		
		NbTour=1;
		for (l=1;l<NbTourMax;l++)
		{
			NbTour=l;
			ratio=(R1/(R2));
			//Debug.Log("l="+l+"   ratio="+ratio);	
			if (((l*ratio-Mathf.Floor(l*ratio)) < delta) || ((NbTour==NombreDeTour) && !(Automatique)))
			{
				//Debug.Log("NbTour="+NbTour+"   RayonMaximal="+RM+"   RayonPrincipal="+RayonPrincipal);
				break;
			}
		}

		for (k=0;k<NombrePoints;k++)
			{
				CentreRotation.transform.Rotate(0.0f,(360.0f/(NombrePoints/NbTour)),0.0f,Space.Self);
				AxeA.transform.Rotate(0.0f,(-360.0f/(NombrePoints/NbTour)*(R1/R2))*facteur1,0.0f, Space.Self);
				if (RotationAxeB) {AxeB.transform.Rotate(0.0f,(-360.0f/(NombrePoints/NbTour)*(R2/R3))*facteur2,0.0f, Space.Self);}
				//AxeB.transform.Rotate(0.0f,(360.0f/NombrePoints)*(R1/R2)*facteur,0.0f, Space.Self);
				//t=k/NombrePoints;			
				//PointLine = R1*Cercle(t);
				//AxeC.transform.Translate(Onde()*Vector3.up);
				lineRenderer.SetPosition(k+IndexFormule,AxeC.transform.position);
				//Debug.Log("k="+k+"p="+AxeC.transform.position+"r="+AxeC.transform.localEulerAngles);
			}
		if ((Trainee) && (IndexFormule<200000))
			{IndexFormule+=k;}
		else
			{IndexFormule=0;}
	}
	
	public void FixedUpdate()
	{
		Spirographe(SelectedLine, Root, RayonMaximal, RayonDisque2, Rayon3,CrayonX+Convert.ToInt32(OndeCrayonX)*Onde(),CrayonY+Convert.ToInt32(OndeCrayonY)*Onde(),SelectedLine.FacteurTransmission1, FacteurTransmission2, lengthOfLineRenderer);	
	}
}
