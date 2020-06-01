using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testClassA : MonoBehaviour
{
    // Start is called before the first frame update
    public int variableA;
	
	void Start()
    {
       fonctionA(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public static void fonctionA()
	{
		Debug.Log("fonctionA");
	}
}
