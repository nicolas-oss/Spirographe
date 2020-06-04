using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFieldRotation : MonoBehaviour
{
    //public GameObject IFRot;
	bool isRotating;
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       isRotating=Spirographe.GetActiveSpiroFormule().AnimRotation;
	   if (isRotating) GetComponent<InputFieldInterface>().RefreshContent();
    }
}
