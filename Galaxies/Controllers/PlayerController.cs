using Galaxies.Core;
using Galaxies.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxies.Extensions;
using Galaxies.Items;

namespace Galaxies.Controllers
{

    static class PlayerController
    {

        public static PlayerShip Ship { get; private set; }

        public static Inventory Inventory { get; private set; }

        /// <summary>
        /// Creates an entirely new player.
        /// </summary>
        public static void CreateNewPlayer()
        {
            Ship = new PlayerShip(
                new Transform(new Vector2(100)),
                SpriteHelper.GetSprite("Sprites/Player Ships/Player Ship 1"),
                Vector2.Zero,
                new ShipStats(100, 100, 10, 1000, 50)
                );
        }

        public static void AssignNewShip(PlayerShip newShip)
        {
            Ship = newShip;

            //Swap the two inventories.
            Ship.Inventory.AddItems(Inventory.Items);
            Inventory = Ship.Inventory;
        }

    }

}
