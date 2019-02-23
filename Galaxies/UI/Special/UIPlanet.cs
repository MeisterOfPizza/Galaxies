using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.Space;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    class UIPlanet : UIGroup
    {

        public UIPlanet(Transform transform, Texture2D sprite, EventArg onClick, Screen screen, Planet planet) : base(transform, sprite, onClick, screen)
        {
            transform.Size = new Vector2(300, 200);

            AddUIElement(new UIText(new Transform(new Vector2(0, -75), new Vector2(300, 50)), SpriteHelper.Arial_Font, planet.Data.Name, TextAlign.MiddleLeft, 5, screen));
            AddUIElement(new UIText(new Transform(new Vector2(0, 25), new Vector2(300, 150)), SpriteHelper.Arial_Font, planet.Data.Description, TextAlign.TopLeft, 5, screen));
        }

    }

}
