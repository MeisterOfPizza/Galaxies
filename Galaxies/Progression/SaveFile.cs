using Galaxies.Controllers;
using Galaxies.Datas.Items;
using Galaxies.Datas.Space;
using Galaxies.Items;
using Galaxies.Space;
using System;
using System.IO;

namespace Galaxies.Progression
{

    [Serializable]
    class SaveFile
    {

        /// <summary>
        /// Check if this save file was just created.
        /// </summary>
        public bool IsNewGame { get; set; } = true;


        public string   PlayerName       { get; private set; }
        public string   SaveFileName     { get; private set; }
        public string   SaveFilePath     { get; private set; }
        public DateTime SaveFileDateTime { get; private set; }

        /// <summary>
        /// Planetary Systems not visited yet.
        /// </summary>
        public string[] PlanetarySystemIds { get; private set; } = new string[0];

        /// <summary>
        /// The Planetary System currently being visited.
        /// </summary>
        public SaveFile_PlanetarySystem CurrentPlanetarySystem { get; private set; }

        /// <summary>
        /// Keeps track of what the player has in his/her inventory and what ship they're using and which ships they've unlocked.
        /// </summary>
        public SaveFile_Player Player { get; private set; }

        public SaveFile(string playerName, string saveFileName)
        {
            this.PlayerName       = playerName;
            this.SaveFileName     = saveFileName;
            this.SaveFilePath     = SaveFileController.LocalDocumentsSaveFilesDirectoryPath + "/" + saveFileName;
            this.SaveFileDateTime = DateTime.Now;
        }

        #region Saving

        public void Save()
        {
            Write_PlanetarySystemIds();
            Write_CurrentPlanetarySystem();
            Write_Player();
        }

        private void Write_PlanetarySystemIds()
        {
            //Skip the Citadel index:
            PlanetarySystemIds = new string[GalaxyController.Visitables.Count - 1];

            //Skip the Citadel:
            for (int i = 1; i < GalaxyController.Visitables.Count; i++)
            {
                PlanetarySystemIds[i - 1] = ((PlanetarySystem)GalaxyController.Visitables[i]).Data.Id;
            }
        }

        private void Write_CurrentPlanetarySystem()
        {
            if (PlanetarySystemController.CurrentPlanetarySystem != null)
            {
                CurrentPlanetarySystem = new SaveFile_PlanetarySystem(PlanetarySystemController.CurrentPlanetarySystem);
            }
        }

        private void Write_Player()
        {
            Player = new SaveFile_Player();
        }

        #endregion

        #region Loading

        public void Load()
        {

        }

        private void Read_CurrentPlanetarySystem()
        {

        }

        #endregion

    }

    [Serializable]
    class SaveFile_PlanetarySystem
    {

        public string            Id      { get; private set; }
        public SaveFile_Planet[] Planets { get; private set; }

        public SaveFile_PlanetarySystem(PlanetarySystem planetarySystem)
        {
            this.Id = planetarySystem.Data.Id;

            this.Planets = new SaveFile_Planet[planetarySystem.Planets.Length];

            for (int i = 0; i < Planets.Length; i++)
            {
                Planets[i] = new SaveFile_Planet(planetarySystem.Planets[i].Data.Id, planetarySystem.Planets[i].Visited);
            }
        }

        public PlanetarySystemData GetPlanetarySystemData()
        {
            return DataController.LoadData<PlanetarySystemData>(Id, DataFileType.PlanetarySystems);
        }

    }

    [Serializable]
    class SaveFile_Planet
    {

        public string Id      { get; private set; }
        public bool   Visited { get; private set; }

        public SaveFile_Planet(string id, bool visited)
        {
            this.Id      = id;
            this.Visited = visited;
        }

    }

    [Serializable]
    class SaveFile_Player
    {

        public SaveFile_PlayerShipTemplates PlayerShipTemplates { get; private set; }
        public SaveFile_Inventory           PlayerInventory     { get; private set; }

        public SaveFile_Player()
        {
            PlayerShipTemplates = new SaveFile_PlayerShipTemplates();
            PlayerInventory     = new SaveFile_Inventory(PlayerController.Player.Inventory);
        }

    }

    [Serializable]
    class SaveFile_PlayerShipTemplates
    {

        public string                        CurrentPlayerShipTemplateId { get; private set; }
        public SaveFile_PlayerShipTemplate[] Templates                   { get; private set; }

        public SaveFile_PlayerShipTemplates()
        {
            Templates = new SaveFile_PlayerShipTemplate[ShipyardController.PlayerShipTemplates.Length];

            for (int i = 0; i < Templates.Length; i++)
            {
                Templates[i] = new SaveFile_PlayerShipTemplate(ShipyardController.PlayerShipTemplates[i].Id, ShipyardController.PlayerShipTemplates[i].Unlocked);
            }

            CurrentPlayerShipTemplateId = ShipyardController.CurrentPlayerShipTemplate.Id;
        }

    }

    [Serializable]
    class SaveFile_PlayerShipTemplate
    {

        public string Id       { get; private set; }
        public bool   Unlocked { get; private set; }

        public SaveFile_PlayerShipTemplate(string id, bool unlocked)
        {
            this.Id       = id;
            this.Unlocked = unlocked;
        }

    }

    [Serializable]
    class SaveFile_Inventory
    {

        public SaveFile_Item[] Items { get; private set; }

        public SaveFile_Inventory(Inventory inventory)
        {
            Items = new SaveFile_Item[inventory.Items.Count];

            for (int i = 0; i < Items.Length; i++)
            {
                Items[i] = new SaveFile_Item(inventory.Items[i]);
            }
        }

    }

    [Serializable]
    class SaveFile_Item
    {

        public string   ItemId   { get; private set; }
        public ItemType ItemType { get; private set; }

        public SaveFile_Item(Item item)
        {
            this.ItemId   = item.Data.Id;
            this.ItemType = item.Data.ItemType;
        }

    }

}
