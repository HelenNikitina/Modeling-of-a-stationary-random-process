using Olena_Nikitina;
using System;
using System.Collections.Generic;


namespace Olena_Nikitina
{
    public class MyRNormClass
    {
        MyRundomeClass myRundome = new MyRundomeClass();
        private decimal _maxValue;
        private decimal _range;
        private decimal _fMax;
        private decimal _a, _b;
        private decimal _x1, _x2, _y = -1;
        private List<decimal> listOfValue;
        private int n = 10000;
        public MyRNormClass()
        {
            _maxValue = 3.905M;
            _range = 7.72743M;
            _fMax = fNormDev(myRundome.getMean());
            _a = myRundome.getMean() - (myRundome.getStandertDeviation() * 5);
            _b = myRundome.getMean() + myRundome.getStandertDeviation() * 5;
            listOfValue = new List<decimal>();
        }
        private void setMaxValue()
        {
            decimal max = 0;
            foreach (var item in listOfValue)
            {
                if (item > max) { max = item; }
            }
            _maxValue = max;
        }
        public decimal getMaxValue()
        {
            return _maxValue;
        }
        public decimal getRange()
        {
            return _range;
        }
        private void CalcValues(List<decimal> listOfValue)
        {
            for (int i = 0; i < n; i++)
            {
                getRandX();
                listOfValue.Add(_y);
            }
        }
        public decimal getRandX()
        {
            _y = -1;
            while (_y == -1)
            {
                _x1 = myRundome.GetRndVal();
                _x2 = myRundome.GetRndVal();
                _y = getY(_a, _b, _fMax, _x1, _x2);
            }
            return _y;
        }
        private decimal getY(decimal a, decimal b, decimal fMax, decimal x1, decimal x2)
        {
            decimal x = a + (b - a) * getW(x1);
            decimal y = fMax * getW(x2);
            if (y <= fNormDev(x))
            {
                return x;
            }
            else
            {
                return -1;
            }
        }
        private decimal getW(decimal x)
        {
            return x / myRundome.getPeriod();
        }
        private decimal fNormDev(decimal x)
        {
            decimal c = (decimal)Math.Pow((double)(x - myRundome.getMean()), 2);
            decimal cc = (decimal)Math.Pow((double)myRundome.getStandertDeviation(), 2) * 2;
            decimal res = (decimal)(1 / ((double)myRundome.getStandertDeviation() * Math.Sqrt(2 * Math.PI)) * Math.Pow(Math.Exp(1), -1.0 * (double)(c / cc)));
            return res;
        }
    }
}