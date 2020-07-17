using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Aspect : MonoBehaviour
{
    //public SpiroData data = new SpiroData();
	public int NombreCouleur;
	public static int TailleTableauCouleur = 8;
	public Color[] couleur = new Color[TailleTableauCouleur];
	public Gradient gradient;
	public GradientColorKey[] colorKey; //= new GradientColorKey[TailleTableauCouleur];
	public GradientAlphaKey[] alphaKey; //= new GradientAlphaKey[TailleTableauCouleur];
	
	public float widthOfLineRenderer;

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

	//Duplication

	/*void GestionClone()
	{
		if (Fondu)
		{
			alpha*=FacteurAttenuationFondu;
			
			gradient = gameObject.GetComponent<LineRenderer>().colorGradient;
			alphaKey = new GradientAlphaKey[NombreCouleur];
			colorKey = new GradientColorKey[NombreCouleur];

			for (int i=0; i<NombreCouleur; i++)
			{
				colorKey[i].color = couleur[i];
				colorKey[i].time = (1.0f*i)/(1.0f*NombreCouleur);
				alphaKey[i].alpha = alpha;
				alphaKey[i].time = (1.0f*i)/(1.0f*NombreCouleur);
			}
		
			gradient.SetKeys(colorKey, alphaKey);
			gameObject.GetComponent<LineRenderer>().colorGradient=gradient;
			
		}
	}*/
}