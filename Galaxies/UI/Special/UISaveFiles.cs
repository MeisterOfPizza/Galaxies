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

        public override bool Visable
        {
            get
            {
                return base.Visable;
            }

            set
            {
                base.Visable = value;

                if (value)
                {
                    //Get all files in directory:
                    UpdateUISaveFiles();
                }
            }
        }

        public UISaveFiles(Transform transform, Screen screen, bool canSave, string title, EventArg onClose) : base(transform, null, screen)
        {
            this.canSave = canSave;
            this.onClose = onClose;

            //Title:
            AddUIElement(new UIText(
                new Transform(new Vector2(0, -transform.Height / 2f - 25), new Vector2(transform.Width, 50)),
                ContentHelper.Arial_Font,
                title,
                TextAlign.MiddleLeft,
                5,
                screen
                ));

            //Actual column with save files:
            uiSaveFileColumn = AddUIElement(new UIScrollableColumn(
                new Transform(transform.Size),
                ContentHelper.Box4x4_Sprite,
                screen,
                new Vector4(5),
                new Vector2(5),
                200
                ));

            uiSaveFileColumn.SetColor(new Color(56, 56, 56));

            //Close button:
            AddUIElement(new UIButton(
                new Transform(new Vector2(0, transform.Height / 2f + 40), new Vector2(100, 50)),
                ContentHelper.Arial_Font,
                "Close",
                TextAlign.MiddleCenter,
                5,
                ContentHelper.Box4x4_Sprite,
                onClose,
                screen
                )).SetColor(new Color(28, 28, 28));
        }

        private void UpdateUISaveFiles()
        {
            uiSaveFileColumn.Clear();

            foreach (FileInfo fileInfo in SaveFileController.GetSaveFiles())
            {
                var tempFileInfo = fileInfo;

                uiSaveFileColumn.AddUIElement(new UISaveFile(
                    new Transform(new Vector2(250, 200)),
                    ContentHelper.Box4x4_Sprite,
                    screen,
                    tempFileInfo,
                    canSave
                    ));
            }
        }

    }

}
