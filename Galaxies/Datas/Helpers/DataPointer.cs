using System.Xml.Serialization;

namespace Galaxies.Datas.Helpers
{

    [XmlRoot(Namespace = "", ElementName = "DataPointer")]
    public class DataPointer
    {

        #region Constants

        /*
        
        These constant fields are used throughout the project to identify data types.
        
        */

        public const string TYPE_ITEM_STARCHART   = "sc";
        public const string TYPE_ITEM_SHIPUPGRADE = "su";

        #endregion

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

    }

}
