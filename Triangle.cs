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
        private double _angleA, _angleB, _angleC;
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

        public double CalculateAngle(double side1, double side2, double oppositeSide)
        {
            // opposite cos for side1 
            double cosAngle = (side2 * side2 + oppositeSide * oppositeSide - side1 * side1) / (2 * side2 * oppositeSide);
            // finding arc cos for for getting corner in radiants
            double angleInRadians = Math.Acos(cosAngle);
            // converting corner into dergees
            angleInRadians = angleInRadians * (180 / Math.PI);
            return angleInRadians;
        }
         
        // properties
        public double AngleA
        {
            get
            {
                if (_angleA == 0)
                    _angleA = CalculateAngle(a, b, c); // angle opposite for A
                return _angleA;
            }
            set { _angleA = value; }
        }

        public double AngleB
        {
            get
            {
                if (_angleB == 0)
                    _angleB = CalculateAngle(b, a, c); // angle opposite for B
                return _angleB;
            }
            set { _angleB = value; }
        }

        public double AngleC
        {
            get
            {
                if (_angleC == 0)
                    _angleC = CalculateAngle(c, a, b); // angle opposite for C
                return _angleC;
            }
            set { _angleC = value; }
        }
        public double GetSetA
        {
            get { return a; }
            set { a = value; }
        }
        public double GetSetB
        {
            get { return b; }
            set { b = value; }
        }
        public double GetSetC
        {
            get { return c; }
            set { c = value; }
        }
        public bool ExistTriangle
        {
            get
            {
                if ((a + b > c && a + c > b && b + c > a))
                    return true;
                return false;
            }
        }

    }
}
