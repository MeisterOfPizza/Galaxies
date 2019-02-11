using Microsoft.Xna.Framework;
using System.Xml.Serialization;

namespace Galaxies.Datas
{

    [XmlRoot(Namespace = "", ElementName = "Color")]
    public class ColorData
    {

        [XmlAttribute("r")]
        public byte R { get; set; } = byte.MaxValue;

        [XmlAttribute("g")]
        public byte G { get; set; } = byte.MaxValue;

        [XmlAttribute("b")]
        public byte B { get; set; } = byte.MaxValue;

        public Color GetColor()
        {
            return new Color(R, G, B, byte.MaxValue);
        }

    }

}
