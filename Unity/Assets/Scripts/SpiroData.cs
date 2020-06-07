using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiroData
{
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
	//public GameObject[] CentreRayon = new GameObject[TailleTableaux];
	//public GameObject AxeToInstatiate;
	
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
	//bool Attends;
	public bool Master;
	public Color c1 = Color.yellow;
    public Color c2 = Color.red;
	public bool isInitialised;
	public bool z=true;
	public float profondeur_z;
}
