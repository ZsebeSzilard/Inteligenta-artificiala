using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims
{
    public static class Calculator
    {
        static Random random = new Random();
        public static double GetDistance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2));
        }
        public static int GetRandom(int a)
        {
            return random.Next(a);
        }
        public static int GetRandom(int a, int b)
        {
            return random.Next(a,b);
        }



    }
}
