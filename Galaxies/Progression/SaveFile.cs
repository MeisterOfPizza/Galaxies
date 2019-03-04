using Galaxies.Controllers;
using Galaxies.Datas.Items;
using Galaxies.Datas.Space;
using Galaxies.Entities;
using Galaxies.Items;
using System;

namespace Galaxies.Progression
{

    [Serializable]
    class SaveFile
    {

        /// <summary>
        /// Check if this save file was just created.
        /// </summary>
        public bool IsNewGame { get; set; } = true;

        public string   PlayerName       { get; set; }
        public string   SaveFileName     { get; set; }
        public DateTime SaveFileDateTime { get; set; }

        /// <summary>
        /// Planetary Systems not visited yet.
        /// </summary>
        public string[] PlanetarySystemIds { get; set; } = new string[0];

        /// <summary>
        /// The Planetary System currently being visited.
        /// </summary>
        public SaveFile_PlanetarySystem CurrentPlanetarySystem { get; set; }

    }

    [Serializable]
    class SaveFile_PlanetarySystem
    {

        public string            Id      { get; private set; }
        public SaveFile_Planet[] Planets { get; private set; }

        public SaveFile_PlanetarySystem()
        {

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

        public SaveFile_PlayerShip PlayerShip { get; private set; }
        public SaveFile_Inventory  Inventory  { get; private set; }

        public SaveFile_Player()
        {
            PlayerShip = new SaveFile_PlayerShip(PlayerController.Ship.BaseStats, PlayerController.Ship.ModifiedStats);
            Inventory  = new SaveFile_Inventory(PlayerController.Inventory);
        }

    }

    [Serializable]
    class SaveFile_PlayerShip
    {

        public ShipStats BaseStats     { get; private set; }
        public ShipStats ModifiedStats { get; private set; }

        public SaveFile_PlayerShip(ShipStats baseStats, ShipStats modifiedStats)
        {
            this.BaseStats     = baseStats;
            this.ModifiedStats = modifiedStats;
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
