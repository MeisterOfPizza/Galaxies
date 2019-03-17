namespace Galaxies.Core
{

    /// <summary>
    /// Event with three arguments.
    /// </summary>
    class EventArg3<T1, T2, T3> : EventArg
    {

        public _EventArg3<T1, T2, T3> Event { get; private set; }

        public T1 Arg1 { get; private set; }
        public T2 Arg2 { get; private set; }
        public T3 Arg3 { get; private set; }

        public EventArg3()
        {

        }

        public EventArg3(_EventArg3<T1, T2, T3> @event, T1 arg1, T2 arg2, T3 arg3)
        {
            this.Event = @event;
            this.Arg1  = arg1;
            this.Arg2  = arg2;
            this.Arg3  = arg3;
        }

        public EventArg3(T1 arg1, T2 arg2, T3 arg3, params _EventArg3<T1, T2, T3>[] events)
        {
            foreach (_EventArg3<T1, T2, T3> @event in events)
            {
                this.Event += @event;
            }

            this.Arg1 = arg1;
            this.Arg2 = arg2;
            this.Arg3 = arg3;
        }

        /// <summary>
        /// Returns false if the delegate did not exist.
        /// </summary>
        public override bool Invoke()
        {
            if (Event != null)
            {
                Event.Invoke(Arg1, Arg2, Arg3);
            }

            return Event != null;
        }

        /// <summary>
        /// Takes three arguments.
        /// </summary>
        public override void SetArguments(params object[] args)
        {
            if (args != null && args.Length >= 3)
            {
                Arg1 = (T1)args[0];
                Arg2 = (T2)args[1];
                Arg3 = (T3)args[2];
            }
        }

        public override void AddEvent(object @event)
        {
            Event += (_EventArg3<T1, T2, T3>)@event;
        }

    }

}
