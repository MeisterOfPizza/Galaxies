using Galaxies.Controllers;
using Galaxies.Datas.Items;
using Galaxies.Datas.Player;
using Galaxies.Datas.Space;
using Galaxies.Economy;
using Galaxies.Items;
using Galaxies.Space;
using System;
using System.Linq;

namespace Galaxies.Progression
{

    /// <summary>
    /// In which state was the player in when the save file was created?
    /// </summary>
    enum SaveFile_GameState
    {
        Galaxy,
        PlanetarySystem,
        Citadel
    }

    [Serializable]
    class SaveFile
    {

        #region General

        /// <summary>
        /// Name of the player. [CUSTOM]
        /// </summary>
        public string PlayerName { get; private set; }

        /// <summary>
        /// Name of the save file. [CUSTOM]
        /// </summary>
        public string SaveFileName { get; private set; }

        /// <summary>
        /// Full path of the save file (except the extension, in this case, ".sav"). [SYSTEM]
        /// </summary>
        public string SaveFilePath { get; private set; }

        /// <summary>
        /// When was the save file created? [SYSTEM]
        /// </summary>
        public DateTime SaveFileDateTime { get; private set; }

        public SaveFile_GameState GameState { get; private set; }

        #endregion

        #region Specific

        //////////////////////////////////////////
        //      CAN CONTAIN NULL VALUES!        //
        //////////////////////////////////////////

        /// <summary>
        /// Planetary Systems not visited yet.
        /// </summary>
        public string[] PlanetarySystemIds { get; private set; } = new string[0];

        /// <summary>
        /// The Planetary System currently being visited.
        /// </summary>
        public SaveFile_PlanetarySystem CurrentPlanetarySystem { get; private set; }

        /// <summary>
        /// Keeps track of what the player has in his/her <see cref="Inventory"/> and what their <see cref="Economy.Balance"/> is.
        /// </summary>
        public SaveFile_Trader PlayerTrader { get; private set; }

        /// <summary>
        /// Keeps track of which ship the player's using and which ships they've unlocked.
        /// </summary>
        public SaveFile_PlayerShipTemplates PlayerShipTemplates { get; private set; }

        #endregion

        public SaveFile(string saveFileName, string playerName)
        {
            this.PlayerName       = playerName;
            this.SaveFileName     = saveFileName;
            this.SaveFilePath     = SaveFileController.LocalDocumentsSaveFilesDirectoryPath + "/" + saveFileName;
            this.SaveFileDateTime = DateTime.Now;
        }

        #region Saving

        public void Save()
        {
            Write_GameState();
            Write_PlanetarySystemIds();
            Write_CurrentPlanetarySystem();
            Write_Player();
            Write_PlayerShipTemplates();
        }

        private void Write_GameState()
        {
            switch (GameController.GameState)
            {
                case Controllers.GameState.PlanetarySystem:
                    GameState = SaveFile_GameState.PlanetarySystem;
                    break;
                case Controllers.GameState.Citadel:
                    GameState = SaveFile_GameState.Citadel;
                    break;
                case Controllers.GameState.Galaxy:
                default: //The player can ONLY be in the states stated above, therefore, if the player is in any other state, just say that they're in the galaxy (screen):
                    GameState = SaveFile_GameState.Galaxy;
                    break;
            }
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
            PlayerTrader = new SaveFile_Trader();
        }

        private void Write_PlayerShipTemplates()
        {
            PlayerShipTemplates = new SaveFile_PlayerShipTemplates();
        }

        #endregion

        #region Loading

        public PlanetarySystem[] Load_PlanetarySystems()
        {
            PlanetarySystem[] systems = new PlanetarySystem[PlanetarySystemIds.Length];

            for (int i = 0; i < systems.Length; i++)
            {
                systems[i] = new PlanetarySystem(DataController.LoadData<PlanetarySystemData>(PlanetarySystemIds[i], DataFileType.PlanetarySystems));
            }

            return systems;
        }

        public PlanetarySystem Load_CurrentPlanetarySystem()
        {
            return new PlanetarySystem(DataController.LoadData<PlanetarySystemData>(CurrentPlanetarySystem.Id, DataFileType.PlanetarySystems));
        }

        public void Load_Player()
        {
            Load_PlayerShipTemplates();

            Trader trader = new Trader(null, new Balance(PlayerTrader.PlayerBalance));
            Inventory inventory = new Inventory(trader);
            trader.Inventory = inventory;

            PlayerController.AssignNewTrader(trader);
        }

        private void Load_PlayerShipTemplates()
        {
            for (int i = 0; i < PlayerShipTemplates.Templates.Length; i++)
            {
                var template = ShipyardController.PlayerShipTemplates.First(t => t.Id == PlayerShipTemplates.Templates[i].Id);

                if (template != null)
                {
                    template.Unlocked = PlayerShipTemplates.Templates[i].Unlocked;
                }
            }

            //Assign the new player ship:
            ShipyardController.AssignPlayerShip(ShipyardController.PlayerShipTemplates.First(t => t.Id == PlayerShipTemplates.CurrentPlayerShipTemplateId));
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
    class SaveFile_Trader
    {

        public int                PlayerBalance   { get; private set; }
        public SaveFile_Inventory PlayerInventory { get; private set; }

        public SaveFile_Trader()
        {
            PlayerBalance   = PlayerController.Player.Balance.GalacticGold;
            PlayerInventory = new SaveFile_Inventory(PlayerController.Player.Inventory);
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
