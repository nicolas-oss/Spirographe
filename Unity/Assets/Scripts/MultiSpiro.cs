using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSpiro : MonoBehaviour
{
    public LineRenderer Line,Line1,Line2;
	public GameObject Spiro1,Spiro2;
	public float quotient,alpha,beta,L,L1,L2;
	public int NbrPts,N1,N2;
	public GameObject Root1,Root2,Crayon,Root1Rotation;
	Vector3 A,A1,A2,rotation,position,globalPosition;
	Quaternion RotQuaternion;
	public int multiple=5;
	public float vitesseRelative=1.0f;
	//public int i;
	static SpiroFormule spiroFormule1,spiroFormule2;
	public bool profondeur;
	public float facteur_profondeur;
	
	public MultiSpiroData data = new MultiSpiroData();
	
    public void StoreData()
	{
		//data.Line1=Line1;
		//data.Line2=Line2;
		//data.Spiro1=Spiro1;
		//data.Spiro2=Spiro2;
		data.multiple=multiple;
		data.vitesseRelative=vitesseRelative;
		//data.spiroFormule1=spiroFormule1;
		//data.spiroFormule2=spiroFormule2;
		data.L1=L1;
		data.L2=L2;
		data.profondeur=profondeur;
		data.facteur_profondeur=facteur_profondeur;
	}
	
	public void LoadData()
	{
		//Line1=data.Line1;
		//Line2=data.Line2;
		//Spiro1=data.Spiro1;
		//Spiro2=data.Spiro2;
		multiple=data.multiple;
		vitesseRelative=data.vitesseRelative;
		L1=data.L1;
		L2=data.L2;
		//spiroFormule1=data.spiroFormule1;
		//spiroFormule2=data.spiroFormule2;
		profondeur=data.profondeur;
		facteur_profondeur=data.facteur_profondeur;
	}

	
	void OnEnable()
	{
		Spiro1.GetComponent<SpiroFormule>().onPostSpiro +=  delegate{CalculeMultiSpiro();};
		Spiro2.GetComponent<SpiroFormule>().onPostSpiro +=  delegate{CalculeMultiSpiro();};
	}
	
	void OnDisable()
	{
		Spiro1.GetComponent<SpiroFormule>().onPostSpiro -=  delegate{CalculeMultiSpiro();};
		Spiro2.GetComponent<SpiroFormule>().onPostSpiro -=  delegate{CalculeMultiSpiro();};
	}
	
	void Update()
	{
		//CalculeMultiSpiro();
	}
	
	void Start()
    {
		Line1=Spiro1.GetComponent<LineRenderer>();
		Line2=Spiro2.GetComponent<LineRenderer>();
		spiroFormule1=Spiro1.GetComponent<SpiroFormule>();
		spiroFormule2=Spiro2.GetComponent<SpiroFormule>();
		N1=Line1.positionCount;
		N2=Line2.positionCount;
		if (Line1.positionCount<Line2.positionCount) {NbrPts=Line2.positionCount;} else {NbrPts = Line1.positionCount;}
		Line = GetComponent<LineRenderer>();
		Line.positionCount = NbrPts*multiple;
		//i=0;
		//Spiro2.GetComponent<SpiroFormule>().onPostSpiro += CalculeMultiSpiro();
    }

    public float CalculQuotient()
	{
		return ((L1*L1-L2*L2)/(2.0f*L1*L));
	}
	
	/*public void orienteRoot1();
	{
		float alpha;
		alpha = Mathf.Atan(
	}*/
    
	public void CalculeMultiSpiro()
    {
        //for (int j = 0; j<10;j++)
			//{
		//Start();
		if ((Line1==null) || (Line2==null)) return;
		
		Line1=Spiro1.GetComponent<LineRenderer>();
		Line2=Spiro2.GetComponent<LineRenderer>();
		spiroFormule1=Spiro1.GetComponent<SpiroFormule>();
		spiroFormule2=Spiro2.GetComponent<SpiroFormule>();
		N1=Line1.positionCount;
		N2=Line2.positionCount;
		if (Line1.positionCount<Line2.positionCount) {NbrPts=Line2.positionCount;} else {NbrPts = Line1.positionCount;}
		Line = GetComponent<LineRenderer>();
		Line.positionCount = NbrPts*multiple;
		
		for (int i = 0; i<NbrPts*multiple; i++)
		{
			Root1.transform.position = Spiro1.transform.TransformPoint(Line1.GetPosition(i%(N1-1)));
			Root2.transform.position = Spiro2.transform.TransformPoint(Line2.GetPosition((int)(i*vitesseRelative%(N2-1))));
			A1=Root1.transform.position;
			A2=Root2.transform.position;
			//Root1.transform.position = A1;
			//Root2.transform.position = A2;
			L=Vector3.Distance(A1,A2);
			//Debug.Log("L="+L.ToString());
			//Debug.Log("L="+L.ToString());
			//beta=Mathf.Asin(A1.z/L);
			A=A2-A1;
			beta=Mathf.Acos(A.x/L);

			//if (A.x==0.0f) {beta=90;} else beta = 57.296f*Mathf.Atan((A.z)/(A.x));
			if (L==0.0f) {beta=90;} else {beta*=57.296f;} //conversion radian vers degres
			if (A.z<0.0f) {beta=-beta;}
			
			if (L==0.0f) 
			{
				Debug.Log("WARNING DIVISION");
				L=1.0f;
			}
			
			quotient = CalculQuotient();
			if (quotient>1) 
			{
				Debug.Log("WARNING ACOS trop grand");
				//quotient=1.0f;
				while (quotient>1)
				{
					L2+=0.001f;
					quotient=CalculQuotient();
				}
			}
			if (quotient<-1) 
			{
				Debug.Log("WARNING ACOS trop petit");
				//quotient=1.0f;
				while (quotient<-1)
				{
					L2-=0.01f;
					quotient=CalculQuotient();
				}
			}
			alpha = Mathf.Acos(quotient);
			alpha*=57.296f; //conversion radian vers degres
			
			position.x=L1;
			position.y=0.0f;
			position.z=0.0f;
			
			rotation.x = 0.0f;
			rotation.y = 0.0f;
			rotation.z = 0.0f;
			
			//Debug.Log("rotation x="+(rotation.x).ToString()+" y="+(rotation.y).ToString()+" z="+(rotation.z).ToString());
			Root1.transform.localEulerAngles = rotation;		//on applique la rotation de la contrainte Aim sur Root1
			Root1.transform.Rotate(0.0f,beta,0.0f,Space.Self);
			
			Root1Rotation.transform.localEulerAngles = rotation;
			Root1Rotation.transform.Rotate(0.0f,alpha,0.0f,Space.Self);
			Root1Rotation.transform.localPosition = position;
			globalPosition = Crayon.transform.position;
			if (profondeur) globalPosition+=1.0f*i*facteur_profondeur*Vector3.up;
			Line.SetPosition(i,globalPosition);  
			
			//i++;
			
		}
		//if (i>multiple*NbrPts) i=0;
    }
}
