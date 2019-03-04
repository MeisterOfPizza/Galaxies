using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Elements;
using Galaxies.UI.Screens;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.UI.Special
{

    class UITopInfo : UIGroup
    {

        UIText galacticGoldText;

        public UITopInfo(Screen screen) : base(new Transform(Alignment.TopCenter, new Vector2(GameUIController.WindowWidth, 75)), SpriteHelper.GetSprite("Sprites/Box 4x4"), screen)
        {
            galacticGoldText = AddUIElement(new UIText(
                new Transform(Alignment.TopLeft, new Vector2(250, 75)),
                SpriteHelper.Arial_Font,
                "GG"
                ));
        }

    }

}
