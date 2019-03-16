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

        public const string ITEM_TYPE_STARCHART   = "sc";
        public const string ITEM_TYPE_SHIPUPGRADE = "su";

        public const string VISITABLE_TYPE_PLANETARYSYSTEM = "ps";

        #endregion

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

    }

}
