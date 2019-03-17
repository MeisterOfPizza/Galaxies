using Galaxies.Controllers;
using Galaxies.Datas.Space;
using Galaxies.Extensions;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Space
{

    class PlanetarySystem : IVisitable
    {

        public PlanetarySystemData Data { get; private set; }

        public Planet[] Planets { get; private set; }

        #region IVisitable

        public string Name
        {
            get
            {
                return Data.Name;
            }
        }

        public string Description
        {
            get
            {
                return Data.Description;
            }
        }

        public Texture2D VisitableTypeIcon
        {
            get
            {
                return null;
            }
        }

        #endregion

        public PlanetarySystem(PlanetarySystemData data)
        {
            this.Data = data;
        }

        private void CreatePlanets()
        {
            Planets = new Planet[Data.PlanetIds.Length];

            for (int i = 0; i < Data.PlanetIds.Length; i++)
            {
                Planets[i] = new Planet(DataController.LoadData<PlanetData>(Data.PlanetIds[i], DataFileType.Planets));
            }
        }

        #region IVisitable

        public void Visit()
        {
            CreatePlanets();

            GalaxyController.RemoveVisistable(this); //Remove this from visitable things.
            PlanetarySystemController.SetPlanetarySystem(this); //Set the current planetary system.
            GameUIController.CreatePlanetarySystemScreen(); //Create the UI.
        }

        #endregion

    }

}
