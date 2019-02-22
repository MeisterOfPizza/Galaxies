using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Extensions;
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

            var visitablesColumn = AddUIElement(new UIScrollableColumn(columnSprite, GameUIController.TopLeftCorner(300, 600), 0, Color.White, new Vector2(300, 600), this, 5, 5, 200));

            AddUIElement(new UIButton(SpriteHelper.Arial_Font, "Menu", TextAlign.MiddleCenter, 5, columnSprite, GameUIController.BottomLeftCorner(100, 100), 0, Color.White, new Vector2(100, 100), new EventArg0(GameUIController.CreateMenuScreen), this));
            
            //Creating visitables
            foreach (IVisitable visitable in GalaxyController.Visitables)
            {
                ///<see cref="PlanetarySystem.Visit"/> and <see cref="Citadel.Visit"/>
                visitablesColumn.AddUIElement(new UIPlanetarySystem(columnSprite, Vector2.Zero, new EventArg0(visitable.Visit), this, visitable));
            }

            /*
            AddUIElement(new UIElement(backToMenuSprite, GameUIController.BottomLeftCorner(backToMenuSprite.Width, backToMenuSprite.Height), 0, Color.White, null)); //TODO: Back to menu
            AddUIElement(new UIElement(galaxyBackgroundSprite, Vector2.Zero, 0, Color.White, null));
            AddUIElement(new ListBox(statsSprite, GameUIController.BottomRightCorner(statsSprite.Width, statsSprite.Height), 0, Color.White, null));
            */
        }

    }

}
