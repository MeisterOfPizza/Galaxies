using Microsoft.Xna.Framework;
using System.Xml.Serialization;

namespace Galaxies.Datas
{

    [XmlRoot(Namespace = "", ElementName = "Color")]
    class ColorData
    {

        [XmlAttribute("r", typeof(byte))]
        public byte R { get; set; } = byte.MaxValue;

        [XmlAttribute("g", typeof(byte))]
        public byte G { get; set; } = byte.MaxValue;

        [XmlAttribute("b", typeof(byte))]
        public byte B { get; set; } = byte.MaxValue;

        public Color GetColor()
        {
            return new Color(R, G, B, byte.MaxValue);
        }

    }

}
