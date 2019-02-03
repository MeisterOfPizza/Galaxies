using Galaxies.Datas.Space;
using Galaxies.Space.Events;

namespace Galaxies.Space
{

    class Planet
    {

        public PlanetData Data { get; private set; }

        public PlanetEvent[] OnVisit { get; set; }

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
            foreach (PlanetEvent @event in OnVisit)
            {
                @event.Trigger();
            }
        }

    }

}
