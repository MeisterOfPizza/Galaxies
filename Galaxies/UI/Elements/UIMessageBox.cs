using Galaxies.Controllers;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class UIMessageBox : UIGroup
    {

        UIButton okBtn;

        public UIMessageBox(SpriteFont spriteFont, string text, TextAlign textAlign, int textPadding, Texture2D backgroundSprite, Vector2 position, float rotation, Color color, Vector2 size, OnClickEvent onClick, Screen screen) : base(backgroundSprite, position, rotation, color, size, onClick, screen, false)
        {
            AddUIElement(new UIText(
                spriteFont,
                text,
                textAlign,
                textPadding,
                new Vector2(0, Height / 2 - 100),
                0,
                Color.Wheat,
                new Vector2(Width, Height - 50),
                screen
                ));

            okBtn = AddUIElement(new UIButton(
                spriteFont,
                "Ok",
                TextAlign.MiddleCenter,
                5,
                backgroundSprite,
                new Vector2(0, Height / 2 - 25),
                0,
                Color.White,
                new Vector2(100, 50),
                new OnClickEvent(onClick + Clicked),
                screen
                ));

            CalculatePositions();
        }

        private void Clicked()
        {
            Screen.RemoveUIElement(this);
            PlanetEventController.TriggerNextEvent();
        }

    }

}
