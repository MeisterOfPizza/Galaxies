using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UI.Special;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace Galaxies.UI.Screens
{

    class CitadelScreen : Screen
    {

        UIShop           uiShop;
        UIShipyard       uiShipyard;
        UISaveFiles      uiSaveFiles;
        UICreateSaveFile uiCreateSaveFile;

        public async override Task CreateUIAsync()
        {
            //Add background gif:
            AddUIElement(new UIBackgroundAnimation(
                new Transform(Alignment.MiddleCenter, new Vector2(GameUIController.WindowWidth, GameUIController.WindowHeight)),
                Random.Next(2) == 0 ? SpriteHelper.Citadel_Background_Animation_1 : SpriteHelper.Citadel_Background_Animation_2,
                this
                ));

            //Add the shop:
            uiShop = AddUIElement(new UIShop(
                new Transform(Alignment.MiddleCenter, new Vector2(1300, 800)),
                this
                ));

            uiShop.Visable = false; //Hide the shop.

            //Add the shipyard:
            uiShipyard = AddUIElement(new UIShipyard(
                new Transform(Alignment.MiddleCenter, new Vector2(1200, 800)),
                this
                ));

            uiShipyard.Visable = false; //Hide the shipyard.

            uiSaveFiles = AddUIElement(new UISaveFiles(
                new Transform(Alignment.MiddleCenter, new Vector2(250, 600)),
                this,
                true,
                "Load",
                new EventArg0(ToggleUISaveFiles)
                ));

            uiSaveFiles.Visable = false;

            uiCreateSaveFile = AddUIElement(new UICreateSaveFile(
                new Transform(Alignment.MiddleCenter, new Vector2(500, 200)),
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                this,
                new EventArg0(ToggleUICreateSaveFile)
                ));

            uiCreateSaveFile.Visable = false;

            //Go back button:
            AddUIElement(new UIButton(
                new Transform(Alignment.BottomLeft, new Vector2(300, 50)),
                SpriteHelper.Arial_Font,
                "Exit the Citadel",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                new EventArg0(GameUIController.CreateGalaxyScreen),
                this
                ));

            //Open shipyard button:
            AddUIElement(new UIButton(
                new Transform(Alignment.BottomRight, new Vector2(200, 50)),
                SpriteHelper.Arial_Font,
                "Shipyard",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                new EventArg0(ToggleUIShipyard),
                this
                ));

            //Open shop button:
            AddUIElement(new UIButton(
                new Transform(Transform.SetPosition(Alignment.BottomRight, new Vector2(-212.5f, 0), new Vector2(200, 50)), new Vector2(200, 50)),
                SpriteHelper.Arial_Font,
                "Shop",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                new EventArg0(ToggleUIShop),
                this
                ));

            //Open save files:
            AddUIElement(new UIButton(
                new Transform(Transform.SetPosition(Alignment.BottomRight, new Vector2(-425, 0), new Vector2(200, 50)), new Vector2(200, 50)),
                SpriteHelper.Arial_Font,
                "Load",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                new EventArg0(ToggleUISaveFiles),
                this
                ));

            //Open create save file:
            AddUIElement(new UIButton(
                new Transform(Transform.SetPosition(Alignment.BottomRight, new Vector2(-637.5f, 0), new Vector2(200, 50)), new Vector2(200, 50)),
                SpriteHelper.Arial_Font,
                "Save",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                new EventArg0(ToggleUICreateSaveFile),
                this
                ));

            //Add the Top Info bar:
            AddUIElement(new UITopInfo(this));
        }

        private void ToggleUIShop()
        {
            uiShop.Visable = !uiShop.Visable; //Switch the visibility
            uiShipyard.Visable = false;
            uiSaveFiles.Visable = false;
            uiCreateSaveFile.Visable = false;
        }

        private void ToggleUIShipyard()
        {
            uiShop.Visable = false;
            uiShipyard.Visable = !uiShipyard.Visable;  //Switch the visibility
            uiSaveFiles.Visable = false;
            uiCreateSaveFile.Visable = false;
        }

        private void ToggleUISaveFiles()
        {
            uiShop.Visable = false;
            uiShipyard.Visable = false;
            uiSaveFiles.Visable = !uiSaveFiles.Visable; //Switch the visibility
            uiCreateSaveFile.Visable = false;
        }

        private void ToggleUICreateSaveFile()
        {
            uiShop.Visable = false;
            uiShipyard.Visable = false;
            uiSaveFiles.Visable = false;
            uiCreateSaveFile.Visable = !uiCreateSaveFile.Visable; //Switch the visibility
        }

    }

}
