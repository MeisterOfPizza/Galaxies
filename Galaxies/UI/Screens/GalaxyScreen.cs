using Galaxies.Controllers;
using Galaxies.Space;
using Galaxies.UI.Elements;
using Galaxies.UI.Special;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Screens
{

    class GalaxyScreen : Screen
    {

        public override void CreateUI(ContentManager content)
        {
            var columnSprite           = content.Load<Texture2D>("Sprites/UI/Column");
            /*
            var backToMenuSprite       = content.Load<Texture2D>("Sprites/UI/BackToMenu");
            var galaxyBackgroundSprite = content.Load<Texture2D>("Sprites/UI/Galaxy Background");
            var statsSprite            = content.Load<Texture2D>("Sprites/UI/Stats");
            */
            var arialFont              = content.Load<SpriteFont>("Fonts/Arial");

            var visitablesColumn = AddUIElement(new UIColumn(columnSprite, GameUIController.TopLeftCorner(), 0, Color.White, null, this, 5, 5));
            visitablesColumn.SetDrawSize(300, 300);

            AddUIElement(new UIButton(arialFont, "Menu", TextAlign.MiddleCenter, 5, columnSprite, GameUIController.BottomLeftCorner(100, 100),  0, Color.White, GameUIController.CreateMenuScreen, this));

            //Creating visitables
            foreach (IVisitable visitable in GalaxyController.Visitables)
            {
                visitablesColumn.AddUIElement(new UIPlanetarySystem(columnSprite, Vector2.Zero, visitable.Visit, this, visitable)).CalculatePositions();
            }

            /*
            AddUIElement(new UIElement(backToMenuSprite, GameUIController.BottomLeftCorner(backToMenuSprite.Width, backToMenuSprite.Height), 0, Color.White, null)); //TODO: Back to menu
            AddUIElement(new UIElement(galaxyBackgroundSprite, Vector2.Zero, 0, Color.White, null));
            AddUIElement(new ListBox(statsSprite, GameUIController.BottomRightCorner(statsSprite.Width, statsSprite.Height), 0, Color.White, null));
            */
        }

    }

}
