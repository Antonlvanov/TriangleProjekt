using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle
{
    class Triangle
    {
        public double a, b, c;
        public Triangle(double A, double B, double C) {
            a = A; b = B; c = C;
        }
        public double Perimeter()
        {
            double p = 0;
            p = a + b + c;
            return p;
        }
        public double Surface()
        {
            double s, p = 0;
            p = (a + b + c) / 2;
            s = Math.Sqrt((p * (p - a) * (p-b) * (p-c)));
            return s;
        }
        public string outputA() { return Convert.ToString(a); }
        public string outputB() { return Convert.ToString(b); }
        public string outputC() { return Convert.ToString(c); }

        // properties
        public double GetSetA
        {
            get { return a; }
            set { a = value; }
        }
        public double GetSetB
        {
            get { return b; }
            set { a = value; }
        }
        public double GetSetC
        {
            get { return c; }
            set { a = value; }
        }
        public bool ExistTriangle
        {
            get
            {
                if ((a > b + c) && (b > a + c) && (c > a + b))
                    return true;
                return false;
            }
        }
    }
}
