namespace Galaxies.Core
{

    delegate void _EventArg0();
    delegate void _EventArg1<T1>(T1 t1);

    /// <summary>
    /// Base class for all events.
    /// </summary>
    abstract class EventArg
    {

        public abstract bool Invoke();

    }

    /// <summary>
    /// Event with zero arguments.
    /// </summary>
    class EventArg0 : EventArg
    {

        public _EventArg0 Event { get; private set; }

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

    }

    /// <summary>
    /// Event with one argument.
    /// </summary>
    class EventArg1<T1> : EventArg
    {

        public _EventArg1<T1> Event { get; private set; }

        public T1 Value1 { get; private set; }

        public EventArg1(_EventArg1<T1> @event, T1 value)
        {
            this.Event = @event;
        }

        public EventArg1(params _EventArg1<T1>[] events)
        {
            foreach (_EventArg1<T1> @event in events)
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
                Event.Invoke(Value1);
            }

            return Event != null;
        }

    }

}
