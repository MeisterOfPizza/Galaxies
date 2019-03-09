using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Galaxies.Progression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Galaxies.Controllers
{

    static class SaveFileController
    {

        private const string DOCUMENTS_DIRECTORY_NAME = "Galaxies";
        private const string SAVE_FILE_DIRECTORY_NAME = "Saves";

        private static string LocalDocumentsDirectoryPath          { get; set; }
        public  static string LocalDocumentsSaveFilesDirectoryPath { get; private set; }

        private static DirectoryInfo SaveFileDirectory { get; set; }

        public static SaveFile CurrentSaveFile { get; private set; }

        public static void Initialize()
        {
            //Get the path to My Documents:
            LocalDocumentsDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //Setup full file path:
            LocalDocumentsSaveFilesDirectoryPath = LocalDocumentsDirectoryPath + "/" + DOCUMENTS_DIRECTORY_NAME + "/" + SAVE_FILE_DIRECTORY_NAME;

            //Create and/or return the save file directory.
            SaveFileDirectory = Directory.CreateDirectory(LocalDocumentsSaveFilesDirectoryPath);
        }

        public static FileInfo[] GetSaveFiles()
        {
            if (SaveFileDirectory.Exists)
            {
                return SaveFileDirectory.GetFiles("*.sav*");
            }
            else
            {
                SaveFileDirectory.Create();

                return GetSaveFiles();
            }
        }

        private static bool SaveFileExists(string name)
        {
            if (!SaveFileDirectory.Exists) SaveFileDirectory.Create();

            var files = SaveFileDirectory.GetFiles("*.sav*");

            if (files != null)
            {
                return files.Any(f => f.Name.Equals(name));
            }

            return false; //There were no files, therefore no files with the same name as name exists.
        }

        public static void SaveGame(string saveFileName)
        {
            if (!SaveFileDirectory.Exists) SaveFileDirectory.Create();

            CurrentSaveFile = new SaveFile(CurrentSaveFile.PlayerName, saveFileName);
            CurrentSaveFile.Save();

            SerializeSaveFile(CurrentSaveFile);
        }

        public static void SaveGame(FileInfo fileInfo)
        {
            if (!SaveFileDirectory.Exists) SaveFileDirectory.Create();

            CurrentSaveFile = new SaveFile(CurrentSaveFile.PlayerName, fileInfo.Name);
            CurrentSaveFile.Save();

            SerializeSaveFile(CurrentSaveFile);
        }

        public static void LoadGame(FileInfo fileInfo)
        {
            CurrentSaveFile = DeserializeSaveFile(fileInfo);

            //GameController.LoadGame();
        }

        private static void SerializeSaveFile(SaveFile saveFile)
        {
            //Create the save file.
            FileStream fs = new FileStream(saveFile.SaveFilePath, FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                formatter.Serialize(fs, saveFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to serialize save file. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        private static SaveFile DeserializeSaveFile(FileInfo file)
        {
            SaveFile saveFile = null;

            //Open the save file.
            FileStream fs = new FileStream(file.FullName, FileMode.Open);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                saveFile = (SaveFile)formatter.Deserialize(fs);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to deserialize save file. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

            return saveFile;
        }

    }

}
