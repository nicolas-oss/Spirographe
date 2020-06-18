using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenShot : MonoBehaviour
{
	public int resWidth = 1920; 
	public int resHeight = 1080;
	
	public static string ScreenShotName(int width, int height) 
	{
		return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", Application.dataPath, width, height, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
	
	public void Capture(string filename)
	{
		RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
		GetComponent<Camera>().targetTexture = rt;
		Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
		GetComponent<Camera>().Render();
		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
		GetComponent<Camera>().targetTexture = null;
		RenderTexture.active = null; // JC: added to avoid errors
		Destroy(rt);
		byte[] bytes = screenShot.EncodeToPNG();
		System.IO.File.WriteAllBytes(filename, bytes);
		//Debug.Log(string.Format("Took screenshot to: {0}", filename));
	}
	
	public void Record(string filename)
	{
		string filenamecomplet;
		filenamecomplet="{0}/sequences/"+filename+".png";
		Capture(filenamecomplet);
		Debug.Log("rec "+filename);
	}
	
	public void ScreenShot()
    {
        string filename;
		filename = ScreenShotName(resWidth, resHeight);
		Capture(filename);
		Debug.Log(string.Format("Took screenshot to: {0}", filename));
    }
}
