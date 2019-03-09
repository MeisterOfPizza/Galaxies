using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    class UICreateSaveFile : UIGroup
    {

        EventArg onClose;

        public UICreateSaveFile(Transform transform, Texture2D sprite, Screen screen, EventArg onClose) : base(transform, sprite, screen)
        {
            this.onClose = onClose;

            AddUIElement(new UIInputField(
                new Transform(new Vector2(0, -transform.Height / 2f + 25), new Vector2(transform.Width, 50)),
                SpriteHelper.Arial_Font,
                "SaveFile Name",
                TextAlign.MiddleLeft,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                screen
                ));
        }

        public void Open()
        {
            Visable = true;
        }

        public void Close()
        {
            Visable = false;
            //Reset input fields

            if (onClose != null)
            {
                onClose.Invoke();
            }
        }

    }

}
