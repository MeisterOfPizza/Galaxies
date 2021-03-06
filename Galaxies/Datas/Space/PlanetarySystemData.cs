﻿using System.Xml.Serialization;

namespace Galaxies.Datas.Space
{

    [XmlRoot(Namespace = "", ElementName = "PlanetarySystem")]
    public class PlanetarySystemData : Data
    {

        [XmlArray("PlanetIds", IsNullable = false)]
        [XmlArrayItem("PlanetId", IsNullable = false)]
        public string[] PlanetIds { get; set; }

    }

}
