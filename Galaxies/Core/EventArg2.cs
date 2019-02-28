namespace Galaxies.Core
{

    /// <summary>
    /// Event with two arguments.
    /// </summary>
    class EventArg2<T1, T2> : EventArg
    {

        public _EventArg2<T1, T2> Event { get; private set; }

        public T1 Arg1 { get; private set; }
        public T2 Arg2 { get; private set; }

        public EventArg2()
        {

        }

        public EventArg2(_EventArg2<T1, T2> @event, T1 arg1, T2 arg2)
        {
            this.Event = @event;
            this.Arg1  = arg1;
            this.Arg2  = arg2;
        }

        public EventArg2(T1 arg1, T2 arg2, params _EventArg2<T1, T2>[] events)
        {
            foreach (_EventArg2<T1, T2> @event in events)
            {
                this.Event += @event;
            }

            this.Arg1 = arg1;
            this.Arg2 = arg2;
        }

        /// <summary>
        /// Returns false if the delegate did not exist.
        /// </summary>
        public override bool Invoke()
        {
            if (Event != null)
            {
                Event.Invoke(Arg1, Arg2);
            }

            return Event != null;
        }

        /// <summary>
        /// Takes one argument.
        /// </summary>
        public override void SetArguments(params object[] args)
        {
            if (args != null && args.Length >= 2)
            {
                Arg1 = (T1)args[0];
                Arg2 = (T2)args[1];
            }
        }

        public override void AddEvent(object @event)
        {
            Event += (_EventArg2<T1, T2>)@event;
        }

    }

}
