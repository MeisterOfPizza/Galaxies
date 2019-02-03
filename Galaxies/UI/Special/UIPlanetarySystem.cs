using Galaxies.Space;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    class UIPlanetarySystem : UIGroup
    {

        IVisitable visitable;

        public UIPlanetarySystem(Texture2D sprite, Vector2 position, OnClickEvent onClick, Screen screen, IVisitable visitable) : base(sprite, position, 0, Color.White, onClick, screen)
        {
            var content = MainGame.Singleton.Content;

            this.visitable = visitable;

            AddUIElement(new UIElement(visitable.VisitableTypeIcon, new Vector2(0, 0), 0, Color.Red, null, screen, false)).SetDrawSize(50, 50);
            AddUIElement(new UIText(content.Load<SpriteFont>("Fonts/Arial"), visitable.Name, TextAlign.MiddleLeft, 5, new Vector2(50, 0), 0, Color.White, screen)).SetDrawSize(250, 50);
            AddUIElement(new UIText(content.Load<SpriteFont>("Fonts/Arial"), visitable.Description, TextAlign.TopLeft, 5, new Vector2(0, 50), 0, Color.White, screen)).SetDrawSize(300, 150);

            SetDrawSize(300, 200);
        }

    }

}
