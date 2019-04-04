using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Galaxies.UI.Special
{

    class UISaveFile : UIGroup
    {

        FileInfo fileInfo;

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
                AddUIElement(new UIButton(
                    new Transform(new Vector2(-transform.Width / 2f + 50, transform.Height / 2f - 25), new Vector2(100, 50)),
                    ContentHelper.Arial_Font,
                    "Save",
                    TextAlign.MiddleCenter,
                    5,
                    ContentHelper.Box4x4_Sprite,
                    new EventArg1<FileInfo>(SaveFileController.SaveGame, this.fileInfo),
                    screen
                    )).SetColor(new Color(28, 28, 28));
            }

            AddUIElement(new UIButton(
                new Transform(new Vector2(transform.Width / 2f - 50, transform.Height / 2f - 25), new Vector2(100, 50)),
                ContentHelper.Arial_Font,
                "Load",
                TextAlign.MiddleCenter,
                5,
                ContentHelper.Box4x4_Sprite,
                new EventArg1<FileInfo>(SaveFileController.LoadGame, this.fileInfo),
                screen
                )).SetColor(new Color(28, 28, 28));
        }

    }

}
