namespace Galaxies.Economy
{

    class Balance
    {

        public int GalacticGold { get; private set; }

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
