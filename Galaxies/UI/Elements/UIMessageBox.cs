using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class UIMessageBox : UIGroup
    {

        UIButton okBtn;

        public UIMessageBox(Transform transform, SpriteFont spriteFont, string text, TextAlign textAlign, int textPadding, Texture2D backgroundSprite, EventArg onClick, Screen screen) : base(transform, backgroundSprite, screen)
        {
            SetColor(new Color(48, 48, 48));

            //Message:
            AddUIElement(new UIText(
                new Transform(new Vector2(0, -32.5f) /*Half of 65*/, new Vector2(transform.Width, transform.Height - 65)),
                spriteFont,
                text,
                textAlign,
                textPadding,
                screen
                ));

            //Ok button to close the message box:
            okBtn = AddUIElement(new UIButton(
                new Transform(new Vector2(0, transform.Height / 2 - 35), new Vector2(100, 50)),
                spriteFont,
                "Ok",
                TextAlign.MiddleCenter,
                0,
                ContentHelper.Box4x4_Sprite,
                onClick,
                screen
                ));

            okBtn.SetColor(new Color(28, 28, 28));
            
            Screen.ForceFocus(okBtn);
        }

    }

}
