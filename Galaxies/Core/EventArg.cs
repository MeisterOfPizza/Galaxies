namespace Galaxies.Core
{
    
    delegate void _EventArg0();
    delegate void _EventArg1<T1>(T1 t1);
    delegate void _EventArg2<T1, T2>(T1 t1, T2 t2);
    delegate void _EventArg3<T1, T2, T3>(T1 t1, T2 t2, T3 t3);

    /// <summary>
    /// Base class for all events.
    /// </summary>
    abstract class EventArg
    {

        /// <summary>
        /// Invokes the event(s).
        /// </summary>
        public abstract bool Invoke();

        /// <summary>
        /// Sets the new arguments.
        /// </summary>
        public virtual void SetArguments(params object[] args)
        {
            //Do nothing here.
            //This won't be marked abstract as not all children of EventArg will have arguments.
        }

        /// <summary>
        /// Add an event (delegate).
        /// </summary>
        public abstract void AddEvent(object @event);

    }

}
