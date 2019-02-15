using Galaxies.Space;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    class UIPlanet : UIGroup
    {

        public UIPlanet(Texture2D sprite, Vector2 position, OnClickEvent onClick, Screen screen, Planet planet) : base(sprite, position, 0, Color.White, new Vector2(300, 200), onClick, screen)
        {
            var content = MainGame.Singleton.Content;
            
            AddUIElement(new UIText(content.Load<SpriteFont>("Fonts/Arial"), planet.Data.Name, TextAlign.MiddleLeft, 5, new Vector2(0, -75), 0, Color.White, new Vector2(300, 50), screen));
            AddUIElement(new UIText(content.Load<SpriteFont>("Fonts/Arial"), planet.Data.Description, TextAlign.TopLeft, 5, new Vector2(0, 25), 0, Color.White, new Vector2(300, 150), screen));
        }

    }

}
