using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.Space;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    class UIPlanetarySystem : UIGroup
    {

        public UIPlanetarySystem(Transform transform, Texture2D sprite, EventArg onClick, Screen screen, IVisitable visitable) : base(transform, sprite, onClick, screen)
        {
            transform.Size = new Vector2(300, 200);

            AddUIElement(new UIElement(new Transform(new Vector2(-125, -75), new Vector2(50)), visitable.VisitableTypeIcon, null, screen, false));
            AddUIElement(new UIText(new Transform(new Vector2(25, -75), new Vector2(250, 50)), SpriteHelper.Arial_Font, visitable.Name, TextAlign.MiddleLeft, 5, screen));
            AddUIElement(new UIText(new Transform(new Vector2(0, 25), new Vector2(300, 150)), SpriteHelper.Arial_Font, visitable.Description, TextAlign.TopLeft, 5, screen));
        }

    }

}
