using Galaxies.UI.Elements;
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
            var listBoxSprite          = content.Load<Texture2D>("Sprites/UI/List Box Container");
            var backToMenuSprite       = content.Load<Texture2D>("Sprites/UI/BackToMenu");
            var galaxyBackgroundSprite = content.Load<Texture2D>("Sprites/UI/Galaxy Background");
            var statsSprite            = content.Load<Texture2D>("Sprites/UI/Stats");

            AddUIElement(new ListBox(listBoxSprite, GameUIController.TopLeftCorner(listBoxSprite.Width, listBoxSprite.Height), 0, Color.White, null));
            AddUIElement(new UIElement(backToMenuSprite, GameUIController.BottomLeftCorner(backToMenuSprite.Width, backToMenuSprite.Height), 0, Color.White, null)); //TODO: Back to menu
            AddUIElement(new UIElement(galaxyBackgroundSprite, Vector2.Zero, 0, Color.White, null));
            AddUIElement(new ListBox(statsSprite, GameUIController.BottomRightCorner(statsSprite.Width, statsSprite.Height), 0, Color.White, null));
        }

    }

}
