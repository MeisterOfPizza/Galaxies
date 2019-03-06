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
                PlayerController.Player.Balance.GalacticGold + " GG",
                TextAlign.MiddleCenter,
                5,
                screen
                ));

            Vector2 progressBarSize = new Vector2(250, 25);

            //Player health bar:
            AddUIElement(new UIProgressBar(
                new Transform(Transform.SetPosition(Alignment.TopRight, new Vector2(-700, 25), progressBarSize), progressBarSize),
                SpriteHelper.Box4x4_Sprite,
                Color.Red,
                SpriteHelper.Box4x4_Sprite,
                Color.LawnGreen,
                Vector2.Zero,
                PlayerController.Ship.Health / (float)PlayerController.Ship.MaxHealth,
                screen
                ));

            //Player shield bar:
            AddUIElement(new UIProgressBar(
                new Transform(Transform.SetPosition(Alignment.TopRight, new Vector2(-400, 25), progressBarSize), progressBarSize),
                SpriteHelper.Box4x4_Sprite,
                Color.DarkBlue,
                SpriteHelper.Box4x4_Sprite,
                Color.Cyan,
                Vector2.Zero,
                PlayerController.Ship.Shield / (float)PlayerController.Ship.MaxShield,
                screen
                ));

            //Player energy bar:
            AddUIElement(new UIProgressBar(
                new Transform(Transform.SetPosition(Alignment.TopRight, new Vector2(-100, 25), progressBarSize), progressBarSize),
                SpriteHelper.Box4x4_Sprite,
                Color.Black,
                SpriteHelper.Box4x4_Sprite,
                Color.Yellow,
                Vector2.Zero,
                PlayerController.Ship.Energy / (float)PlayerController.Ship.MaxEnergy,
                screen
                ));

            SetColor(new Color(28, 28, 28));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            galacticGoldText.Text = PlayerController.Player.Balance.GalacticGold + " GG";

            base.Draw(spriteBatch);
        }

    }

}
