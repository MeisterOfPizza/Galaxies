using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class ListBox : UIElement
    {

        private const int SpaceY = 5;

        public List<UIElement> Container { get; private set; } = new List<UIElement>();

        public ListBox(Texture2D sprite, Vector2 position, float rotation, Color color, OnClickEvent onClick) : base(sprite, position, rotation, color, onClick, false)
        {
        }

        private int NextY()
        {
            int nextY = 0;

            foreach (UIElement elem in Container)
            {
                nextY += elem.Sprite.Height + SpaceY;
            }

            return nextY;
        }

    }

}
