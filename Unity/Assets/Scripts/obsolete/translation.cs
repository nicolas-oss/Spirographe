using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class translation : MonoBehaviour
{
	public float V1 = 7.0f;
	public float A = 20.0f;
	public float V2 = 20.0f;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, V2*Mathf.Sin(Time.deltaTime*A), 0);
    }
}
