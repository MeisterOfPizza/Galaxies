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
                return MainGame.Singleton.Content.Load<Texture2D>("Sprites/UI/column");
            }
        }

        #endregion

        public Citadel()
        {
            //TODO: Refresh store:
        }

        #region IVisitable

        public void Visit()
        {
            GameUIController.CreateCitadelScreen();
        }

        #endregion

    }

}
