using Galaxies.UI.Elements;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Screens
{

    class GalaxyScreen : Screen
    {

        private UIColumn POIListBox       { get; set; }
        private UIButton BackToMenuButton { get; set; }

        public override void CreateUI(ContentManager content)
        {
            var columnSprite           = content.Load<Texture2D>("Sprites/UI/Column");
            /*
            var backToMenuSprite       = content.Load<Texture2D>("Sprites/UI/BackToMenu");
            var galaxyBackgroundSprite = content.Load<Texture2D>("Sprites/UI/Galaxy Background");
            var statsSprite            = content.Load<Texture2D>("Sprites/UI/Stats");
            */
            var arialFont              = content.Load<SpriteFont>("Fonts/Arial");

            POIListBox = AddUIElement(new UIColumn(columnSprite, GameUIController.TopLeftCorner(), 0, Color.White, null, this, 5, 5));
            POIListBox.SetDrawSize(200, 300);
            POIListBox.AddUIElement(new UIElement(columnSprite, Vector2.Zero, 0, Color.White, null, this));
            POIListBox.AddUIElement(new UIElement(columnSprite, Vector2.Zero, 0, Color.White, null, this));
            POIListBox.AddUIElement(new UIElement(columnSprite, Vector2.Zero, 0, Color.White, null, this));

            BackToMenuButton = AddUIElement(new UIButton(arialFont, "Menu", columnSprite, GameUIController.BottomLeftCorner(100, 100),  0, Color.White, new UIElement.OnClickEvent(GameUIController.CreateMenuScreen), this));

            /*
            AddUIElement(new UIElement(backToMenuSprite, GameUIController.BottomLeftCorner(backToMenuSprite.Width, backToMenuSprite.Height), 0, Color.White, null)); //TODO: Back to menu
            AddUIElement(new UIElement(galaxyBackgroundSprite, Vector2.Zero, 0, Color.White, null));
            AddUIElement(new ListBox(statsSprite, GameUIController.BottomRightCorner(statsSprite.Width, statsSprite.Height), 0, Color.White, null));
            */
        }

    }

}
