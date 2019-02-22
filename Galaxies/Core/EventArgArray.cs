namespace Galaxies.Core
{

    /// <summary>
    /// Event with an event array.
    /// </summary>
    class EventArgArray : EventArg
    {

        public EventArg[] Events { get; private set; }

        public EventArgArray(params EventArg[] events)
        {
            this.Events = events;
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

            return Events != null && Events.Length > 0;
        }

    }

}
