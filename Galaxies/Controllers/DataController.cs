﻿using Galaxies.Datas;
using Galaxies.Debug;
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
            file.Dispose();

            file = File.Open("Data\\Planets.xml", FileMode.Open);
            document = new XmlDocument();
            document.Load(file);
            DataFiles.Add(DataFileType.Planets, new DataFile(document));
            file.Dispose();

            file = File.Open("Data\\Items.xml", FileMode.Open);
            document = new XmlDocument();
            document.Load(file);
            DataFiles.Add(DataFileType.Items, new DataFile(document));
            file.Dispose();

            file = File.Open("Data\\Enemies.xml", FileMode.Open);
            document = new XmlDocument();
            document.Load(file);
            DataFiles.Add(DataFileType.Enemies, new DataFile(document));
            file.Dispose();

            file = File.Open("Data\\PlayerShipTemplates.xml", FileMode.Open);
            document = new XmlDocument();
            document.Load(file);
            DataFiles.Add(DataFileType.PlayerShipTemplates, new DataFile(document));
            file.Dispose();

            file = File.Open("Data\\Merchant.xml", FileMode.Open);
            document = new XmlDocument();
            document.Load(file);
            DataFiles.Add(DataFileType.Merchant, new DataFile(document));
            file.Dispose();

            file = File.Open("Data\\Visitables.xml", FileMode.Open);
            document = new XmlDocument();
            document.Load(file);
            DataFiles.Add(DataFileType.Visitables, new DataFile(document));
            file.Dispose();
        }

        /// <summary>
        /// Loads the specified data from specified file (if it exists).
        /// </summary>
        public static T LoadData<T>(string id, DataFileType type) where T : Data
        {
            if (DataFiles.TryGetValue(type, out DataFile dataFile))
            {
                XmlNode node = dataFile.GetNode(id);

                if (node != null)
                {
                    return Deserialize<T>(node);
                }
                else
                {
                    CrashHandler.ShowException(string.Format("Data of type {0} with ID {1} does not exist.", id, type));

                    return null;
                }
            }
            else
            {
                CrashHandler.ShowException(string.Format("Data of type {0} with ID {1} does not exist.", id, type));

                return null;
            }
        }

        public static T[] LoadAllData<T>(DataFileType type, string nodeName) where T : Data
        {
            if (DataFiles.TryGetValue(type, out DataFile dataFile))
            {
                XmlNodeList nodeList = dataFile.GetNodes(nodeName);
                T[] nodes = new T[nodeList.Count];
                
                for (int i = 0; i < nodeList.Count; i++)
                {
                    nodes[i] = Deserialize<T>(nodeList[i]);
                }

                return nodes;
            }
            else
            {
                CrashHandler.ShowException(string.Format("Data of type {0} in node {1} does not exist.", type, nodeName));

                return null;
            }
        }

        private static T Deserialize<T>(XmlNode xmlNode) where T : Data
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            StringReader reader = new StringReader(xmlNode.OuterXml);

            T data = (T)serializer.Deserialize(reader);

            return data;
        }

    }

}
