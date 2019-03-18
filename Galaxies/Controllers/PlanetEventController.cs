using Galaxies.Space.Events;
using Galaxies.UIControllers;
using System.Collections.Generic;

namespace Galaxies.Controllers
{

    class PlanetEventController
    {

        private static Queue<PlanetEvent> EventQueue { get; set; } = new Queue<PlanetEvent>();

        public static void SetEventQueue(IList<PlanetEvent> events)
        {
            EventQueue.Clear();

            if (events != null)
            {
                foreach (var e in events)
                {
                    EventQueue.Enqueue(e);
                }
            }
        }
        
        /// <summary>
        /// Call this method when the next event is done.
        /// </summary>
        public static void TriggerNextEvent()
        {
            if (EventQueue.Count > 0)
            {
                EventQueue.Dequeue().Trigger();
            }
            else
            {
                if (GameController.GameState != GameState.PlanetarySystem)
                {
                    //Return to the planetary screen
                    GameUIController.CreatePlanetarySystemScreen();
                }
            }
        }

    }

}
