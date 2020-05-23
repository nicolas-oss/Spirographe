using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CliqueSurface : MonoBehaviour
{
    public GameObject Interface;
	void OnMouseDown()
    {
		Debug.Log("MouseDown");
        if (!(EventSystem.current.IsPointerOverGameObject()))
		{
			Debug.Log("Clic");
			//Interface.GetComponent<Interface>().MainEvent.Invoke();
		}
    }
}
