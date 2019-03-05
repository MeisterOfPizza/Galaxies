using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    class UITopInfo : UIGroup
    {

        UIText galacticGoldText;

        public UITopInfo(Screen screen) : base(new Transform(Alignment.TopCenter, new Vector2(GameUIController.WindowWidth, 75)), SpriteHelper.Box4x4_Sprite, screen)
        {
            galacticGoldText = AddUIElement(new UIText(
                new Transform(Alignment.TopLeft, new Vector2(250, 75)),
                SpriteHelper.Arial_Font,
                PlayerController.Player.Balance.GalacticGold + "GG",
                TextAlign.MiddleCenter,
                5,
                screen
                ));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            galacticGoldText.Text = PlayerController.Player.Balance.GalacticGold + "GG";

            base.Draw(spriteBatch);
        }

    }

}
