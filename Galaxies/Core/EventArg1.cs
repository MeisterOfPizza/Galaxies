namespace Galaxies.Core
{

    /// <summary>
    /// Event with one argument.
    /// </summary>
    class EventArg1<T1> : EventArg
    {

        public _EventArg1<T1> Event { get; private set; }

        public T1 Arg1 { get; private set; }

        public EventArg1(_EventArg1<T1> @event, T1 arg1)
        {
            this.Event = @event;
            this.Arg1  = arg1;
        }

        public EventArg1(T1 arg1, params _EventArg1<T1>[] events)
        {
            foreach (_EventArg1<T1> @event in events)
            {
                this.Event += @event;
            }

            this.Arg1 = arg1;
        }

        /// <summary>
        /// Returns false if the delegate did not exist.
        /// </summary>
        public override bool Invoke()
        {
            if (Event != null)
            {
                Event.Invoke(Arg1);
            }

            return Event != null;
        }

    }

}
