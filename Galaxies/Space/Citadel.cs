using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Space
{

    class Citadel : IVisitable
    {

        #region IVisitable

        public string Name
        {
            get
            {
                return "The Citadel";
            }
        }

        public string Description
        {
            get
            {
                return "Trade, complete quests and meet new people (or aliens)";
            }
        }

        public Texture2D VisitableTypeIcon
        {
            get
            {
                return MainGame.Singleton.Content.Load<Texture2D>("Sprites/UI/Column");
            }
        }

        #endregion

        //NPCs, etc.

        public Citadel()
        {

        }

        #region IVisitable

        public void Visit()
        {

        }

        #endregion

    }

}
