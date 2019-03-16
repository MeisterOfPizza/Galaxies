using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.Progression;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Special
{

    class UICreateSaveFile : UIGroup
    {

        UIInputField saveFileNameInputField;
        UIInputField playerNameInputField;
        EventArg     onClose;

        public override bool Visable
        {
            get
            {
                return base.Visable;
            }

            set
            {
                base.Visable = value;

                if (!value)
                {
                    //Reset input fields
                    saveFileNameInputField.SetText("");
                    playerNameInputField.SetText("");
                }
            }
        }

        public UICreateSaveFile(Transform transform, Texture2D sprite, Screen screen, EventArg onClose) : base(transform, sprite, screen)
        {
            this.onClose = onClose;

            saveFileNameInputField = AddUIElement(new UIInputField(
                new Transform(new Vector2(0, -transform.Height / 2f + 37.5f), new Vector2(transform.Width, 75)),
                SpriteHelper.Arial_Font,
                "Save file name",
                20,
                TextAlign.MiddleLeft,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                screen
                ));

            playerNameInputField = AddUIElement(new UIInputField(
                new Transform(new Vector2(0, -transform.Height / 2f + 112.5f), new Vector2(transform.Width, 75)),
                SpriteHelper.Arial_Font,
                "Player name",
                20,
                TextAlign.MiddleLeft,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                screen
                ));

            //Create button:
            AddUIElement(new UIButton(
                new Transform(new Vector2(-55, transform.Height / 2f + 35), new Vector2(100, 50)),
                SpriteHelper.Arial_Font,
                "Create",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                new EventArg0(CreateSaveFile),
                screen
                ));

            //Close button:
            AddUIElement(new UIButton(
                new Transform(new Vector2(55, transform.Height / 2f + 35), new Vector2(100, 50)),
                SpriteHelper.Arial_Font,
                "Close",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                onClose,
                screen
                ));
        }

        private void CreateSaveFile()
        {
            SaveFile saveFile = new SaveFile(saveFileNameInputField.Text, playerNameInputField.Text);

            GameController.NewGame();
            SaveFileController.SaveGame(saveFile);
        }

    }

}
