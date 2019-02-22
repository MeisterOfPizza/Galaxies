using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class UIMessageBox : UIGroup
    {

        public UIButton OkBtn { get; protected set; }

        public UIMessageBox(SpriteFont spriteFont, string text, TextAlign textAlign, int textPadding, Texture2D backgroundSprite, Vector2 position, float rotation, Color color, Vector2 size, OnClickEvent onClick, Screen screen) : base(backgroundSprite, position, rotation, color, size, null, screen, false)
        {
            AddUIElement(new UIText(
                spriteFont,
                text,
                textAlign,
                textPadding,
                new Vector2(0, -32.5f), //Half of 65
                0,
                Color.White,
                new Vector2(Width, Height - 65),
                screen
                ));

            OkBtn = AddUIElement(new UIButton(
                spriteFont,
                "Ok",
                TextAlign.MiddleCenter,
                5,
                backgroundSprite,
                new Vector2(0, Height / 2 - 35),
                0,
                Color.White,
                new Vector2(100, 50),
                onClick,
                screen
                ));
            
            CalculatePositions();
            Screen.SelectLast();
        }

    }

}
