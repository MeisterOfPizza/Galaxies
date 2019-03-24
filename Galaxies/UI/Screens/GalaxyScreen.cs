using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.Items;
using Galaxies.Space;
using Galaxies.UI.Elements;
using Galaxies.UI.Special;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;

namespace Galaxies.UI.Screens
{

    class GalaxyScreen : Screen
    {

        UIScrollableColumn visitablesColumn;
        UIInventory        uiInventory;

        public override void CreateUI()
        {
            //Background:
            AddUIElement(new UIBackgroundAnimation(
                new Transform(Alignment.MiddleCenter, GameUIController.WindowSize),
                ContentHelper.Space_Background_Animation_1,
                this
                ));

            var columnSprite = ContentHelper.GetSprite("Sprites/UI/column");

            //Scrollable column with all the visitables that the player can click on:
            visitablesColumn = AddUIElement(new UIScrollableColumn(
                new Transform(Alignment.MiddleCenter,
                new Vector2(300, 600)),
                columnSprite,
                this,
                new Vector4(5, 0, 5, 0),
                new Vector2(0, 5),
                200
                ));

            //Back to menu button:
            AddUIElement(new UIButton(
                new Transform(Alignment.BottomLeft,
                new Vector2(100, 100)),
                ContentHelper.Arial_Font,
                "Menu",
                TextAlign.MiddleCenter,
                5,
                columnSprite,
                new EventArg1<EventArg>(GameUIController.CreateMenuScreen, null),
                this
                ));

            UpdateUIVisitables();

            AddUIElement(new UIButton(
                new Transform(Alignment.BottomRight, new Vector2(250, 50)),
                ContentHelper.Arial_Font,
                "Inventory",
                TextAlign.MiddleCenter,
                5,
                columnSprite,
                new EventArg0(ToggeUIInventory),
                this
                ));

            uiInventory = AddUIElement(new UIInventory(
                new Transform(Alignment.MiddleCenter, new Vector2(1200, 600)),
                this,
                PlayerController.Player.Inventory.Items,
                "Inventory",
                true,
                new EventArg0(ToggeUIInventory)
                ));

            uiInventory.Visable = false;

            SetupUIItemsInUIInventory();

            //Add the Top Info bar:
            AddUIElement(new UITopInfo(this));
        }

        private void SetupUIItemsInUIInventory()
        {
            foreach (UIItem uiItem in uiInventory.Items)
            {
                if (uiItem.Item.CanUse)
                {
                    var uiItemPointer = uiItem;
                    var itemPointer   = (StarChart)uiItem.Item;

                    var e = new EventArgList(new EventArg0(itemPointer.Use), new EventArg1<UIItem>(uiInventory.ItemGrid.RemoveUIElement, uiItemPointer));

                    uiItem.CreateUseButton(e);
                }
            }
        }

        public void UpdateUIVisitables()
        {
            var columnSprite = ContentHelper.GetSprite("Sprites/UI/column");

            visitablesColumn.Clear();

            //Creating visitables
            foreach (IVisitable visitable in GalaxyController.Visitables)
            {
                ///<see cref="PlanetarySystem.Visit"/> and <see cref="Citadel.Visit"/>.
                visitablesColumn.AddUIElement(new UIPlanetarySystem(new Transform(Vector2.Zero), columnSprite, new EventArg0(visitable.Visit), this, visitable));
            }
        }

        private void ToggeUIInventory()
        {
            uiInventory.Visable = !uiInventory.Visable;
        }

    }

}
