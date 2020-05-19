using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiroFormule : MonoBehaviour
{
    public bool Duplication,Animate,Fondu;
	public float FacteurAttenuationFondu,FacteurScaleAnimation;
	public float IntervalDuplication,DureeVie;
	bool Attends;
	bool Master=true;
	public Color c1 = Color.yellow;
    public Color c2 = Color.red;
	float alpha = 1.0f;
	Vector3 scaleChange;
	
	// Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        if ((Master) && (Duplication) && !(Attends))
		{
			StartCoroutine(DuplicationMaster());
		}
		if (!(Master))
		{
			GestionClone();
		}
    }
	
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
}