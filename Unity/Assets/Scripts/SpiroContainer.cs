using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("SpiroCollection")]
public class SpiroContainer
{
    [XmlArray("SpiroArray")]
	[XmlArrayItem("Spiro")]
	public List<SpiroData> spiros = new List<SpiroData>();
}
