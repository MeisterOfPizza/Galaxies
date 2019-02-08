using Galaxies.Datas;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Galaxies.Controllers
{

    static class DataController
    {

        private static Dictionary<DataFileType, DataFile> DataFiles { get; set; } = new Dictionary<DataFileType, DataFile>();

        /// <summary>
        /// Loads all the needed xml documents for future data fetching.
        /// </summary>
        public static void Initialize()
        {
            FileStream file = File.Open("Data\\PlanetarySystems.xml", FileMode.Open);
            XmlDocument document = new XmlDocument();
            document.Load(file);
            DataFiles.Add(DataFileType.PlanetarySystems, new DataFile(document));

            file = File.Open("Data\\Planets.xml", FileMode.Open);
            document = new XmlDocument();
            document.Load(file);
            DataFiles.Add(DataFileType.Planets, new DataFile(document));

            file = File.Open("Data\\Items.xml", FileMode.Open);
            document = new XmlDocument();
            document.Load(file);
            DataFiles.Add(DataFileType.Items, new DataFile(document));

            file = File.Open("Data\\Enemies.xml", FileMode.Open);
            document = new XmlDocument();
            document.Load(file);
            DataFiles.Add(DataFileType.Enemies, new DataFile(document));
        }

        /// <summary>
        /// Loads the specified data from specified file (if it exists).
        /// </summary>
        public static T LoadData<T>(string id, DataFileType type) where T : Data
        {
            var dataFile = DataFiles[type];

            if (dataFile != null)
            {
                return Deserialize<T>(dataFile.GetNode(id));
            }
            else
            {
                return null;
            }
        }

        private static T Deserialize<T>(XmlNode xmlNode) where T : Data
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            XmlReader reader = XmlReader.Create(xmlNode.OuterXml);

            T data = (T)serializer.Deserialize(reader);

            return data;
        }

    }

    enum DataFileType
    {
        PlanetarySystems,
        Planets,
        Items,
        Enemies
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

    }

}
