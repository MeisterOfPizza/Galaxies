using Galaxies.Controllers;
using Galaxies.Datas.Space;
using Galaxies.Space.Events;

namespace Galaxies.Space
{

    class Planet
    {

        public PlanetData Data    { get; private set; }
        public bool       Visited { get; private set; }

        PlanetEvent[] onVisit;

        public Planet(PlanetData data)
        {
            this.Data = data;

            onVisit = new PlanetEvent[data.OnVisitEvents.Length];

            for (int i = 0; i < data.OnVisitEvents.Length; i++)
            {
                onVisit[i] = data.OnVisitEvents[i].CreateEvent();
            }
        }

        public void Visit()
        {
            PlanetEventController.SetEventQueue(onVisit);
            PlanetEventController.TriggerNextEvent();

            Visited = true;
        }

    }

}
