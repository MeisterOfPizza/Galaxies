using Galaxies.Controllers;
using Galaxies.Datas.Space;
using Galaxies.Space.Events;

namespace Galaxies.Space
{

    class Planet
    {

        public PlanetData Data { get; private set; }

        public PlanetEvent[] OnVisit { get; set; }

        public bool Visited { get; private set; }

        public Planet(PlanetData data)
        {
            this.Data = data;

            OnVisit = new PlanetEvent[data.OnVisitEvents.Length];

            for (int i = 0; i < data.OnVisitEvents.Length; i++)
            {
                OnVisit[i] = data.OnVisitEvents[i].CreateEvent();
            }
        }

        public void Visit()
        {
            PlanetEventController.SetEventQueue(OnVisit);
            PlanetEventController.TriggerNextEvent();

            Visited = true;
        }

    }

}
