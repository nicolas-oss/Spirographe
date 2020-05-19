﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class Calculs_RayonsB : MonoBehaviour
{
    public GameObject DisquePrincipal,DisqueSecondaire,Rayon_1,Rayon_2,PositionInitialeAxe1,PositionInitialeAxe2,AxeRotation1,AxeRotation2,Crayon,SelectedLine; //CurrentLine,
	public float RayonDisque2;
	float RayonPrincipal;
	public float RayonMaximal;
	float HauteurDisque = 0.1f;
	public float DureeAnimation;
	Vector3 ScaleDisque1,ScaleDisque2,PosRayon1,PosRayon2;
	Animator AnimatorAxe1,AnimatorAxe2;
	
	public float FacteurTransmission;

	public int lengthOfLineRenderer;
	public float widthOfLineRenderer;
	public Color c1 = Color.yellow;
    public Color c2 = Color.red;
	public Color selected = Color.black;
	public Color unselected = Color.gray;
	
	Vector3 RotationAxe1,RotationAxe2;
	float CoefTransmission;
	Vector3 PointLineRenderer,PointInitial;
	
	int i=0;
	int LineCount=1;
	bool Play=true;
	
	Text NameLine;
	public GameObject PreviousLine;

	Color ColorColorPicker;
	float Luminosite;
	
			public float AmplitudeOnde1,AmplitudeOnde2;
			public float VitesseOnde1,VitesseOnde2,PhaseOnde1,PhaseOnde2;
	
	// Start is called before the first frame update
    public void Start()
    {
        //GetAnimators
		AnimatorAxe1 = AxeRotation1.GetComponent<Animator>();
		AnimatorAxe2 = AxeRotation2.GetComponent<Animator>();
		
		//LineRenderer
		LineRenderer lineRenderer = SelectedLine.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
		
		// A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
		lineRenderer.widthMultiplier = widthOfLineRenderer;
		
		//Calcul Rayons et initialisation lineRenderer
		AjusteRayonDisque1();
		AjusteRayonDisque2();
		AjusteDureeAnims();
		ResetCurrentLineRenderer();
		JoueAnim();
    }

    void Update()
    { 
	  //Calcul points
		LineRenderer lineRenderer = SelectedLine.GetComponent<LineRenderer>();
		
		if (Play==true) 
		{
			/*if (i==lengthOfLineRenderer || ((PointLineRenderer-PointInitial).magnitude<0.2 && (PositionInitialeAxe1.transform.position-AxeRotation1.transform.position).magnitude<0.2 && (PositionInitialeAxe2.transform.position-AxeRotation2.transform.position).magnitude<0.2 && i>50))  //Stop anim si on revient au départ
				{
					lineRenderer.SetPosition(i-1,PointInitial); //on ferme la courbe
					StopAnim();
				}
			else
				{*/
				i++;
				PointLineRenderer = Crayon.transform.position;
				lineRenderer.positionCount = i;
				lineRenderer.SetPosition(i-1,PointLineRenderer);
						AjusteRayonDisque1();
		AjusteRayonDisque2();
		AjusteDureeAnims();
				//}		
		}
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
		NameLine.text = ("Spiro"+LineCount.ToString());
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
			SelectedLine=GameObject.Find(GameObject.Find("ListSpiro").transform.GetChild(NbChildren-1).gameObject.GetComponent<Text>().text);
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
		SelectedLine = GameObject.Find(NameLine.text);
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
	
	public float OndeSinus()
	{
		return AmplitudeOnde1*Mathf.Sin(VitesseOnde1*Time.frameCount+PhaseOnde1);
	}
	
	public float OndeCosinus()
	{
		return AmplitudeOnde2*Mathf.Sin(VitesseOnde2*Time.frameCount+PhaseOnde2);
	}
	
	public void AjusteDureeAnims()
	{
		AnimatorAxe1.SetFloat("DureeRotationPrincipale",1.0f/DureeAnimation);
		AnimatorAxe2.SetFloat("DureeRotationSecondaire",1.0f/(DureeAnimation*RayonDisque2/(RayonMaximal*FacteurTransmission)));
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
		FacteurTransmission = GameObject.Find("SliderFacteurTransmission").GetComponent <Slider> ().value;
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
	
	
}
