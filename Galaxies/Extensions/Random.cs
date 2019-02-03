namespace Galaxies.Extensions
{

    /// <summary>
    /// Extension class to make <see cref="System.Random"/> easily available from anywhere.
    /// </summary>
    static class Random
    {

        private static System.Random _Random { get; set; }

        static Random()
        {
            _Random = new System.Random();
        }

        public static int Next()
        {
            return _Random.Next();
        }

        public static int Next(int max)
        {
            return _Random.Next(max);
        }

        public static int Next(int min, int max)
        {
            return _Random.Next(min, max);
        }

        public static double NextDouble()
        {
            return _Random.NextDouble();
        }

        public static double NextDouble(double min, double max)
        {
            return _Random.NextDouble() * (max - min) + min;
        }

        public static float NextFloat(float min, float max)
        {
            return (float)_Random.NextDouble() * (max - min) + min;
        }

    }

}
