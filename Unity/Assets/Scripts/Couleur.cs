using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Couleur : MonoBehaviour
{
    public CouleurData data = new CouleurData();
	

	/*public Color c1 = Color.yellow;
    public Color c2 = Color.red;
	public bool isInitialised;//=false;
	float alpha = 1.0f;*/
	
	public int NombreCouleur;
	public static int TailleTableauCouleur = 8;
	public Color[] couleur = new Color[TailleTableauCouleur];
	public Gradient gradient;
	public GradientColorKey[] colorKey; //= new GradientColorKey[TailleTableauCouleur];
	public GradientAlphaKey[] alphaKey; //= new GradientAlphaKey[TailleTableauCouleur];
	
	public void StoreData()
	{
			data.NombreCouleur=NombreCouleur;
			for (int i=0;i<TailleTableauCouleur;i++)
			{
				data.couleur[i] = couleur[i];
			}
			Debug.Log("Color Values copied");
	}
	
	public void LoadData()
	{
		NombreCouleur=data.NombreCouleur;
		for (int i=0;i<TailleTableauCouleur;i++)
		{
			couleur[i] = data.couleur[i];
		}
	}
	
	void OnEnable()
	{	
	}
	
	public void OnDisable()
	{
	}
	
	void Start()
    {	
		RecalculeGradient();
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
}	