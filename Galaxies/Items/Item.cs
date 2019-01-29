using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Items
{

    abstract class Item
    {

        #region Fields

        int       id;
        string    name;
        Texture2D sprite;
        Color     color = Color.White;

        #endregion

        #region Properties

        public int Id
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        #endregion

        public Item(int id, string name, Texture2D sprite, Color color)
        {
            this.id     = id;
            this.name   = name;
            this.sprite = sprite;
            this.color  = color;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(sprite, color);
        }

    }

}
