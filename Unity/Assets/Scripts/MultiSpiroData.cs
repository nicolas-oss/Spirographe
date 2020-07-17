using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSpiroData
{
    //public LineRenderer Line1,Line2;
	//public GameObject Spiro1,Spiro2;
	public int multiple=5;
	public float vitesseRelative=1.0f;
	//public SpiroFormule spiroFormule1,spiroFormule2;
	public bool profondeur;
	public float facteur_profondeur;
	public float L1,L2;
	
	/////Données ASPECT
	
	public int NombreCouleur;
	public static int TailleTableauCouleur = 8;
	public Color[] couleur = new Color[TailleTableauCouleur];
	
	public float widthOfLineRenderer;
}

