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

}
