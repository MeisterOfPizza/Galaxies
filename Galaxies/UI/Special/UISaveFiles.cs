using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using System.IO;

namespace Galaxies.UI.Special
{

    class UISaveFiles : UIGroup
    {

        UIScrollableColumn uiSaveFileColumn;
        bool               canSave;
        EventArg           onClose;

        public UISaveFiles(Transform transform, Screen screen, bool canSave, string title, EventArg onClose) : base(transform, null, screen)
        {
            this.canSave = canSave;
            this.onClose = onClose;

            AddUIElement(new UIText(
                new Transform(new Vector2(0, -transform.Height / 2f - 25), new Vector2(transform.Width, 50)),
                SpriteHelper.Arial_Font,
                title,
                TextAlign.MiddleLeft,
                5,
                screen
                ));

            uiSaveFileColumn = AddUIElement(new UIScrollableColumn(
                new Transform(transform.Size),
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                screen,
                new Vector4(5),
                new Vector2(5),
                200
                ));

            AddUIElement(new UIButton(
                new Transform(new Vector2(0, transform.Height / 2f + 25), new Vector2(100, 50)),
                SpriteHelper.Arial_Font,
                "Close",
                TextAlign.MiddleCenter,
                5,
                SpriteHelper.GetSprite("Sprites/UI/Column"),
                new EventArg0(Close),
                screen
                ));
        }

        public void Open()
        {
            Visable = true;

            UpdateUISaveFiles();
        }

        public void Close()
        {
            Visable = false;

            onClose.Invoke();
        }

        private void UpdateUISaveFiles()
        {
            uiSaveFileColumn.Clear();

            foreach (FileInfo fileInfo in SaveFileController.GetSaveFiles())
            {
                var tempFileInfo = fileInfo;

                uiSaveFileColumn.AddUIElement(new UISaveFile(
                    new Transform(new Vector2(250, 200)),
                    SpriteHelper.GetSprite("Sprites/UI/Column"),
                    screen,
                    tempFileInfo,
                    canSave
                    ));
            }
        }

    }

}
