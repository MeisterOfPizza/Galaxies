﻿using Galaxies.Core;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class UIMessageBox : UIGroup
    {

        public UIButton OkBtn { get; protected set; }

        public UIMessageBox(Transform transform, SpriteFont spriteFont, string text, TextAlign textAlign, int textPadding, Texture2D backgroundSprite, EventArg onClick, Screen screen) : base(transform, backgroundSprite, screen)
        {
            AddUIElement(new UIText(
                new Transform(new Vector2(0, -32.5f) /*Half of 65*/, new Vector2(transform.Width, transform.Height - 65)),
                spriteFont,
                text,
                textAlign,
                textPadding,
                screen
                ));

            OkBtn = AddUIElement(new UIButton(
                new Transform(new Vector2(0, transform.Height / 2 - 35), new Vector2(100, 50)),
                spriteFont,
                "Ok",
                TextAlign.MiddleCenter,
                5,
                backgroundSprite,
                onClick,
                screen
                ));
            
            CalculatePositions();
            Screen.ForceFocus(OkBtn);
        }

    }

}
