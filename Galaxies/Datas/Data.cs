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

        /// <summary>
        /// Return all <see cref="Data"/> objects that this data points to (see <see cref="Helpers.DataPointer"/>).
        /// If there are no pointers, this method just returns an empty array (to avoid null values).
        /// </summary>
        public virtual Data[] GetDataFromIds()
        {
            return new Data[0];
        }

    }

}
