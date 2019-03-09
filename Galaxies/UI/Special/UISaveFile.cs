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

        public FileInfo FileInfo { get; private set; }

        public UISaveFile(Transform transform, Texture2D sprite, Screen screen, FileInfo fileInfo, bool canSave) : base(transform, sprite, screen)
        {
            this.FileInfo = fileInfo;

            //Name of save file:
            AddUIElement(new UIText(
                new Transform(new Vector2(transform.Width, 50), new Vector2(0, -transform.Height / 2f + 25)),
                SpriteHelper.Arial_Font,
                fileInfo.Name,
                TextAlign.MiddleLeft,
                5,
                screen
                ));

            //Date of save file:
            AddUIElement(new UIText(
                new Transform(new Vector2(transform.Width, 50), new Vector2(0, -transform.Height / 2f + 75)),
                SpriteHelper.Arial_Font,
                fileInfo.LastWriteTime.ToString(),
                TextAlign.MiddleCenter,
                5,
                screen
                ));

            if (canSave)
            {
                AddUIElement(new UIButton(
                    new Transform(new Vector2(100, 50), new Vector2(-transform.Width / 2f + 50, transform.Height / 2f - 25)),
                    SpriteHelper.Arial_Font,
                    "Save",
                    TextAlign.MiddleLeft,
                    5,
                    SpriteHelper.GetSprite("Sprites/UI/Column"),
                    new EventArg1<FileInfo>(SaveFileController.SaveGame, FileInfo),
                    screen
                ));
            }

            AddUIElement(new UIButton(
                new Transform(new Vector2(100, 50), new Vector2(transform.Width / 2f - 50, transform.Height / 2f - 25)),
                SpriteHelper.Arial_Font,
                "Load",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                new EventArg1<FileInfo>(SaveFileController.LoadGame, FileInfo),
                screen
                ));
        }

    }

}
