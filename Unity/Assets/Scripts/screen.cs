using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screen : MonoBehaviour
{
	public int resWidth = 1920; 
	public int resHeight = 1080;
	
	public void ScreenShot()
    {
        ScreenCapture.CaptureScreenshot("SpiroScreenShot"+System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")+".png",1);
    }
}
