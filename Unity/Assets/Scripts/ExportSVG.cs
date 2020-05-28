using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExportSVG : Spirographe
{
	string FileName;
	public GameObject Interface;
	float facteurZoom=40.0f;
	public float OffsetX=250.0f;
	public float OffsetY=100.0f;
	public float LargeurSVG=400.0f;
	public SpiroFormule SelectedLine;
	public string HeaderFileName,FooterFileName;
	LineRenderer lineRenderer;
	public float Xmin,Xmax,Ymin,Ymax,XCurr,YCurr;

    void Start()
    {
        SelectedLine = GetActiveSpiroFormule();
		lineRenderer = SelectedLine.GetComponent<LineRenderer>();
		HeaderFileName=string.Format("{0}/SVG/", Application.dataPath)+HeaderFileName;
		FooterFileName=string.Format("{0}/SVG/", Application.dataPath)+FooterFileName;
    }
	
	public static string ExportFileName()
	{
		return string.Format("{0}/SVG/Export_{1}.SVG", Application.dataPath, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void Export()
    {
				Debug.Log(HeaderFileName);
		FileName=ExportFileName();
		AddToFile(HeaderFileName);
		File.AppendAllText(FileName, "<polyline class=\"spiro\" points=\"");
		File.AppendAllText(FileName, generatePointList());
		File.AppendAllText(FileName, "\"/>\n");
		AddToFile(FooterFileName);
    }
	
	public void AddToFile(string FileToAdd)
	{
		string line;
        StreamReader theReader = new StreamReader(FileToAdd);
        using (theReader)
		{
            do
            {
                line = theReader.ReadLine();
                if (line != null)
                {
					File.AppendAllText(FileName, line + "\n");
                }
            }
            while (line != null); 
            theReader.Close();
        }
	}
	
	public void calculBoundingBox()
	{
		Xmin=0.0f;
		Xmax=0.0f;
		Ymin=0.0f;
		Ymax=0.0f;
		int i;
	}
	
	public string generatePointList()
	{
		string X,Y,lineSVG;
		int i;
		float A,B;
		
		Vector3[] allPos = new Vector3[lineRenderer.positionCount];
		lineRenderer.GetPositions(allPos);
		Xmin=allPos[0].x;
		Xmax=Xmin;
		Ymin=allPos[0].z;
		Ymax=Ymin;
		
		/////Get Min and Max
		for (i=0;i<lineRenderer.positionCount;i++)
		{
			XCurr = allPos[i].x;
			YCurr = allPos[i].z;
			Xmax = (Xmax>XCurr)? Xmax:XCurr;
			Ymax = (Ymax>YCurr)? Ymax:YCurr;
			Xmin = (Xmin<XCurr)? Xmin:XCurr;
			Ymin = (Ymin<YCurr)? Ymin:YCurr;
		}
		//Debug.Log("Xmin="+Xmin.ToString()+" Ymin="+Ymin.ToString()+"Xmax="+Xmax.ToString()+" Ymax="+Ymax.ToString());
		facteurZoom=LargeurSVG/(Xmax-Xmin);
		X="";
		Y="";
		lineSVG="";
		for (i=0;i<lineRenderer.positionCount;i++)
		{
			XCurr=allPos[i].x;
			YCurr=allPos[i].z;
			A=(XCurr-Xmin)*facteurZoom;
			B=(Ymax-YCurr)*facteurZoom;
			X=(Mathf.Floor(A)).ToString();
			Y=(Mathf.Floor(B)).ToString();	
			lineSVG+=X+","+Y+" ";
		}
		return lineSVG;
	}
}
