using Galaxies.Core;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Galaxies.Controllers
{

    static class TickController
    {

        public static List<TickEvent> TickEvents { get; private set; } = new List<TickEvent>();

        public static void Update(GameTime gameTime)
        {
            foreach (TickEvent @event in TickEvents)
            {
                @event.TimeSinceLastTick += gameTime.ElapsedGameTime.TotalSeconds;

                if (@event.TimeSinceLastTick > @event.TickInterval)
                {
                    @event.OnTick.Invoke();

                    @event.TimeSinceLastTick = 0;
                }
            }
        }

    }
    
    class TickEvent
    {

        public EventArg OnTick            { get; private set; }
        public double   TickInterval      { get; private set; }
        public double   TimeSinceLastTick { get; set; }

        public TickEvent(EventArg onTick, double tickInterval)
        {
            this.OnTick       = onTick;
            this.TickInterval = tickInterval;
        }

    }

}
