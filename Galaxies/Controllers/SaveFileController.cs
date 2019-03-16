using Galaxies.Debug;
using Galaxies.Progression;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

        public static void SaveGame(SaveFile saveFile)
        {
            if (!SaveFileDirectory.Exists) SaveFileDirectory.Create();

            CurrentSaveFile = saveFile;
            CurrentSaveFile.Save();

            SerializeSaveFile(CurrentSaveFile);
        }

        public static void SaveGame(FileInfo fileInfo)
        {
            if (!SaveFileDirectory.Exists) SaveFileDirectory.Create();

            //Remove the extension AND the dot:
            string fileNameWithoutExtension = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);

            CurrentSaveFile = new SaveFile(fileNameWithoutExtension, CurrentSaveFile.PlayerName);
            CurrentSaveFile.Save();

            SerializeSaveFile(CurrentSaveFile);
        }

        public static void LoadGame(FileInfo fileInfo)
        {
            CurrentSaveFile = DeserializeSaveFile(fileInfo);

            GameController.LoadGame(CurrentSaveFile);
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
                CrashHandler.ShowException("Failed to deserialize save file. Reason: " + e.Message);
            }
            finally
            {
                fs.Close();
            }
        }

        private static SaveFile DeserializeSaveFile(FileInfo file)
        {
            SaveFile saveFile = null;

            file.Refresh();

            if (!file.Exists)
            {
                CrashHandler.ShowException(new FileNotFoundException("The file " + file.Name + " was not found.", file.Name));

                return saveFile;
            }

            //Open the save file.
            FileStream fs = new FileStream(file.FullName, FileMode.Open);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                
                saveFile = (SaveFile)formatter.Deserialize(fs);
            }
            catch (Exception e)
            {
                CrashHandler.ShowException("Failed to deserialize save file. Reason: " + e.Message);
            }
            finally
            {
                fs.Close();
            }

            return saveFile;
        }

    }

}
