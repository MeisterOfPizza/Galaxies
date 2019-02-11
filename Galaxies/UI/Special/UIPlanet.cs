using Galaxies.Space;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    class UIPlanet : UIGroup
    {

        public UIPlanet(Texture2D sprite, Vector2 position, OnClickEvent onClick, Screen screen, Planet planet) : base(sprite, position, 0, Color.White, onClick, screen)
        {
            var content = MainGame.Singleton.Content;
            
            AddUIElement(new UIText(content.Load<SpriteFont>("Fonts/Arial"), planet.Data.Name, TextAlign.MiddleLeft, 5, new Vector2(0, 0), 0, Color.White, screen)).SetDrawSize(300, 50);
            AddUIElement(new UIText(content.Load<SpriteFont>("Fonts/Arial"), planet.Data.Description, TextAlign.TopLeft, 5, new Vector2(0, 50), 0, Color.White, screen)).SetDrawSize(300, 150);

            SetDrawSize(300, 200);
        }

    }

}
