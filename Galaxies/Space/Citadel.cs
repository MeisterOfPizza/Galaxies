using Galaxies.Extensions;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework.Graphics;

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
                return ContentHelper.GetSprite("Sprites/UI/citadel-icon");
            }
        }

        #endregion

        #region IVisitable

        public void Visit()
        {
            GameUIController.CreateCitadelScreen();
        }

        #endregion

    }

}
