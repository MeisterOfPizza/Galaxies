using Galaxies.Datas;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Galaxies.Controllers
{

    static class DataController
    {

        private static Dictionary<string, DataFile> DataFiles { get; set; } = new Dictionary<string, DataFile>();

        /// <summary>
        /// Loads the specified data from specified file (if it exists).
        /// </summary>
        public static T LoadData<T>(string id, string file) where T : Data
        {
            var dataFile = DataFiles[file];

            if (dataFile != null)
            {
                return Deserialize<T>(dataFile.GetNode(id));
            }
            else
            {
                return null;
            }
        }

        public static T Deserialize<T>(XmlNode xmlNode) where T : Data
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            XmlReader reader = XmlReader.Create(xmlNode.OuterXml);

            T data = (T)serializer.Deserialize(reader);

            return data;
        }

    }

    class DataFile
    {

        public XmlDocument Document { get; private set; }

        public XmlNode GetNode(string id)
        {
            return Document.SelectSingleNode(string.Format("//*[@id='{0}']", id));
        }

    }

}
