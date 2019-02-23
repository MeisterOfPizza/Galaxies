namespace Galaxies.Core
{

    /// <summary>
    /// Event with zero arguments.
    /// </summary>
    class EventArg0 : EventArg
    {

        public _EventArg0 Event { get; private set; }

        public EventArg0()
        {

        }

        public EventArg0(_EventArg0 @event)
        {
            this.Event = @event;
        }

        public EventArg0(params _EventArg0[] events)
        {
            foreach (_EventArg0 @event in events)
            {
                this.Event += @event;
            }
        }

        /// <summary>
        /// Returns false if the delegate did not exist.
        /// </summary>
        public override bool Invoke()
        {
            if (Event != null)
            {
                Event.Invoke();
            }

            return Event != null;
        }

        public override void AddEvent(object @event)
        {
            Event += (_EventArg0)@event;
        }

    }

}
