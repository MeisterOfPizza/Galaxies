namespace Galaxies.Economy
{

    class Balance
    {

        public int GalacticGold { get; private set; }

        public Balance()
        {
            GalacticGold = 0;
        }

        public Balance(int galacticGold)
        {
            this.GalacticGold = galacticGold;
        }

        public bool Extract(int amount)
        {
            if (GalacticGold >= amount)
            {
                GalacticGold -= amount;

                return true;
            }

            return false;
        }

        public void Deposit(int amount)
        {
            GalacticGold += amount;
        }

    }

}
