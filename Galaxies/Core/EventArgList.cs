using System.Collections.Generic;
using System.Linq;

namespace Galaxies.Core
{

    /// <summary>
    /// Event with an event array.
    /// </summary>
    class EventArgList : EventArg
    {

        public List<EventArg> Events { get; private set; }

        public EventArgList()
        {
            Events = new List<EventArg>();
        }

        public EventArgList(params EventArg[] events)
        {
            this.Events = new List<EventArg>();
            this.Events = events.ToList();
        }

        /// <summary>
        /// Returns false if there were no events to execute (null or empty array).
        /// </summary>
        /// <returns></returns>
        public override bool Invoke()
        {
            if (Events != null)
            {
                foreach (EventArg @event in Events)
                {
                    @event.Invoke();
                }
            }

            return Events != null && Events.Count > 0;
        }

        /// <summary>
        /// Add an event of type <see cref="EventArg"/>.
        /// </summary>
        public override void AddEvent(object @event)
        {
            Events.Add((EventArg)@event);
        }

    }

}
