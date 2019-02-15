using Galaxies.Space;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    class UIPlanetarySystem : UIGroup
    {

        public UIPlanetarySystem(Texture2D sprite, Vector2 position, OnClickEvent onClick, Screen screen, IVisitable visitable) : base(sprite, position, 0, Color.White, new Vector2(300, 200), onClick, screen)
        {
            var content = MainGame.Singleton.Content;
            
            AddUIElement(new UIElement(visitable.VisitableTypeIcon, new Vector2(-125, -75), 0, Color.Red, new Vector2(50), null, screen, false));
            AddUIElement(new UIText(content.Load<SpriteFont>("Fonts/Arial"), visitable.Name, TextAlign.MiddleLeft, 5, new Vector2(25, -75), 0, Color.White, new Vector2(250, 50), screen));
            AddUIElement(new UIText(content.Load<SpriteFont>("Fonts/Arial"), visitable.Description, TextAlign.TopLeft, 5, new Vector2(0, 25), 0, Color.White, new Vector2(300, 150), screen));
        }

    }

}
