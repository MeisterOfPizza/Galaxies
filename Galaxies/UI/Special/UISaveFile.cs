using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Galaxies.UI.Special
{

    class UISaveFile : UIGroup, IMaskable
    {

        FileInfo fileInfo;
        UIButton loadButton;
        UIButton saveButton;

        #region IMaskable

        public bool IsInteractableAfterMask
        {
            get
            {
                return isInteractableAfterMask;
            }
        }

        private bool isInteractableAfterMask;

        #endregion

        public UISaveFile(Transform transform, Texture2D sprite, Screen screen, FileInfo fileInfo, bool canSave) : base(transform, sprite, screen)
        {
            this.fileInfo = fileInfo;

            SetColor(new Color(48, 48, 48));

            //Name of save file:
            AddUIElement(new UIText(
                new Transform(new Vector2(0, -transform.Height / 2f + 25), new Vector2(transform.Width, 50)),
                ContentHelper.Arial_Font,
                fileInfo.Name,
                TextAlign.MiddleLeft,
                5,
                screen
                ));

            //Date of save file:
            AddUIElement(new UIText(
                new Transform(new Vector2(0, -transform.Height / 2f + 75), new Vector2(transform.Width, 50)),
                ContentHelper.Arial_Font,
                fileInfo.LastWriteTime.ToString(),
                TextAlign.MiddleLeft,
                5,
                screen
                ));

            if (canSave)
            {
                saveButton = AddUIElement(new UIButton(
                    new Transform(new Vector2(-transform.Width / 2f + 50, transform.Height / 2f - 25), new Vector2(100, 50)),
                    ContentHelper.Arial_Font,
                    "Save",
                    TextAlign.MiddleCenter,
                    5,
                    ContentHelper.Box4x4_Sprite,
                    new EventArg1<FileInfo>(SaveFileController.SaveGame, this.fileInfo),
                    screen
                    ));

                saveButton.SetColor(new Color(28, 28, 28));
            }

            loadButton = AddUIElement(new UIButton(
                new Transform(new Vector2(transform.Width / 2f - 50, transform.Height / 2f - 25), new Vector2(100, 50)),
                ContentHelper.Arial_Font,
                "Load",
                TextAlign.MiddleCenter,
                5,
                ContentHelper.Box4x4_Sprite,
                new EventArg1<FileInfo>(SaveFileController.LoadGame, this.fileInfo),
                screen
                ));

            loadButton.SetColor(new Color(28, 28, 28));
        }

        #region IMaskable

        public void CheckMask(Rectangle mask)
        {
            Rectangle loadRect = new Rectangle(loadButton.Transform.Position.ToPoint(), loadButton.Transform.Size.ToPoint());

            if (mask.Contains(loadRect))
            {
                if (saveButton != null)
                {
                    Rectangle saveRect = new Rectangle(saveButton.Transform.Position.ToPoint(), saveButton.Transform.Size.ToPoint());

                    isInteractableAfterMask = mask.Contains(saveRect);
                }
                else
                {
                    isInteractableAfterMask = true;
                }
            }
            else
            {
                isInteractableAfterMask = false;
            }
        }

        #endregion

    }

}
