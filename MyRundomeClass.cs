namespace Olena_Nikitina
{
    class MyRundomeClass
    {
        private decimal _standardDeviation = 1.0M;
        private decimal _period = 65525M;
        private decimal _mean = 0M;
        private decimal _randX = 167M;
        public decimal getMean()
        {
            return _mean;
        }
        public decimal getStandertDeviation()
        {
            return _standardDeviation;
        }
        public decimal getPeriod()
        {
            return _period;
        }
        public void setStandartDeviation(decimal sDeviation)
        {
            _standardDeviation = sDeviation;
        }
        public void setMean(decimal mean)
        {
            _mean = mean;
        }
        public decimal GetRndVal()
        {
            decimal q = 65536;
            int k1 = 25173;
            int k0 = 13849;
            decimal res = (k1 * _randX + k0) % q;
            _randX = res;
            return res;
        }

    }
}