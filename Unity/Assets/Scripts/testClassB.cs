using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testClassB : testClassA
{
    // Start is called before the first frame update
    public int variableB;
	
	void Start()
    {
       fonctionA(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void fonctionA()
	{
		testClassA.fonctionA();
		Debug.Log("fonctionB");
	}
}
