using System.Xml.Serialization;

namespace Galaxies.Datas
{

    [XmlRoot(Namespace = "", ElementName = "Data")]
    public abstract class Data
    {

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("Name", IsNullable = false)]
        public string Name { get; set; }

        [XmlElement("Description", IsNullable = true)]
        public string Description { get; set; }

    }

}
