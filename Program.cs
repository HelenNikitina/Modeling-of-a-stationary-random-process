using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Olena_Nikitina
{
    class Program
    {
        static private decimal _alfa = 0.3M;
        static private decimal _beta = 0.8M;
        static private decimal _b2 = _alfa * _alfa + _beta * _beta;
        static private decimal _eps = 0.05M;
        static private decimal _sigma = 0.035M;
        static private decimal _D = _sigma * _sigma;
        static private decimal _time = 300;

        static private decimal _N0 = 1;
        static private decimal _dt = 0.1M;

        static List<decimal> _list_x1= new List<decimal>();
        static List<decimal> _list_x2= new List<decimal>();
        static MyRNormClass mnc = new MyRNormClass();

        static string pathX1 = "x1.txt";
        static string pathX2 = "x2.txt";

        
        
        static void Main(string[] args)
        {
            decimal c = (decimal)Math.Sqrt((double)(2 * _D * _alfa));
            int stepCount;
            do
            {
                stepCount = (int)(_time / _dt);
                setFerstValues();
                Calculation(stepCount, c);
            }
            while (!test(getSigma(_list_x1)));
            {
                _list_x1.Clear();
                _list_x2.Clear();
                _dt = _dt * 0.95M;

                stepCount = (int)(_time / _dt);
                setFerstValues();
                Calculation(stepCount, c);
            }
            PrintResult();
            WriteFile(pathX1, _list_x1);
            WriteFile(pathX2, _list_x2);
        }
        static void Calculation(int count, decimal c)
        {

            for (int i = 1; i <= count; i++)
            {
                _list_x1.Add(_list_x1[i - 1] + (_list_x2[i - 1] + c * getWhiteNoise()) * _dt);
                _list_x2.Add(_list_x2[i - 1] + (c * ((decimal)Math.Sqrt((double)_b2) - 2 * _alfa) * getWhiteNoise() - 2 * _alfa * _list_x2[i - 1] - _b2 * _list_x1[i - 1]) * _dt);
            }
        }
        static void PrintResult()
        {
            Console.WriteLine("dt = " + _dt + " sigma X1 = " + getSigma(_list_x1));
        }
        static void print(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("x1 = " + _list_x1[i] + " x2 = " + _list_x2[i]);
            }
        }
        static decimal getWhiteNoise()
        {
            decimal whiteNoise = mnc.getRandX() * (decimal)Math.Sqrt((double)(_N0 / _dt));
            return whiteNoise;
        }
        static void setFerstValues()
        {
            if (_list_x1.Count == 0 && _list_x2.Count == 0)
            {
                _list_x1.Add(_sigma);
                _list_x2.Add(0);
            }
        }
        static decimal getMean(List<decimal> x)
        {
            decimal sum = 0.0M;
            foreach (var item in x)
            {
                sum += item;
            }
            return sum / x.Count;
        }
        static decimal getSigma(List<decimal> x)
        {
            decimal mean = getMean(x);
            decimal sum = 0.0M;
            foreach (var item in x)
            {
                decimal temp = (item - mean) * (item - mean);
                sum += temp;
            }
            return (decimal)Math.Sqrt((double)(sum / x.Count));
        }
        static bool test(decimal sygmaX1)
        {
            decimal t = Math.Abs((_sigma - sygmaX1) / _sigma);
            if (t <= _eps)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static private void WriteFile(string path, List<decimal> list)
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "", Encoding.Unicode);
                Console.WriteLine("File created !");
            }
            foreach (var lists in list)
            {
                String temp = lists.ToString() + Environment.NewLine;
                File.AppendAllText(path, temp, Encoding.Unicode);
            }
        }
    }
}




