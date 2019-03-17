using System.Xml;

namespace Galaxies.Datas
{

    enum DataFileType
    {
        PlanetarySystems,
        Planets,
        Items,
        Enemies,
        PlayerShipTemplates,
        Merchant,
        Visitables
    }

    class DataFile
    {

        public XmlDocument Document { get; private set; }

        public DataFile(XmlDocument document)
        {
            this.Document = document;
        }

        public XmlNode GetNode(string id)
        {
            return Document.SelectSingleNode(string.Format("//*[@id='{0}']", id));
        }

        public XmlNodeList GetNodes(string nodeName)
        {
            return Document.SelectNodes("//" + nodeName);
        }

    }

}
