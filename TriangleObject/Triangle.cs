using Triangle.Interface;

namespace Triangle.TriangleObject
{
    public class Triangle
    {
        public double a, b, c;
        public double angleAB, angleBC, angleAC;
        public double p, s, heightA, heightB, heightC ; 
        public TriangleType Type { get; private set; } 
        private readonly Math _math;

        public Triangle () { }
        public Triangle(double A, double B, double C)
        {
            a = A; b = B; c = C;
            _math = new Math(this);

            if (ExistTriangle)
            {
                angleAB = _math.FindAngle(b, c, a);
                angleBC = _math.FindAngle(a, b, c);
                angleAC = _math.FindAngle(c, a, b);
                _math.FindPerimeter();
                _math.FindSurface();
                heightA = _math.FindHeight(a);
                heightB = _math.FindHeight(b);
                heightC = _math.FindHeight(c);
                DetermineTriangleType(_math);
            }
            else
            {
                Type = TriangleType.NotExist;
            }
        }

        public float SideA => (float)a;
        public float SideB => (float)b;
        public float SideC => (float)c;

        public float AngleAB => (float)angleAB;
        public float AngleBC => (float)angleBC;
        public float AngleAC => (float)angleAC;

        public float Perimeter => (int)p;
        public float Surface => (int)s;

        public bool ExistTriangle
        {
            get
            {
                return a + b > c && a + c > b && b + c > a;
            }
        }
        public string KasOnOlemas
        {
            get
            {
                return ExistTriangle ? "On olemas" : "Ei ole olemas";
            }
        }

        public void DetermineTriangleType(Math math)
        {
            if (math.IsEquilateral())
                Type = TriangleType.Equilateral;
            else if (math.IsIsosceles())
                Type = TriangleType.Isosceles;
            else if (math.IsRight())
                Type = TriangleType.Right;
            else if (math.IsAcute())
                Type = TriangleType.Acute;
            else if (math.IsObtuse())
                Type = TriangleType.Obtuse;
            else
                Type = TriangleType.NotExist;
        }

        public string TypeEst()
        {
            switch (Type)
            {
                case TriangleType.Equilateral:
                    return "Võrdkülgne"; // Равносторонний
                case TriangleType.Isosceles:
                    return "Võrdhaarne"; // Равнобедренный
                case TriangleType.Right:
                    return "Täisnurkne"; // Прямоугольный
                case TriangleType.Acute:
                    return "Teravnurkne"; // Острый
                case TriangleType.Obtuse:
                    return "Nürinurkne"; // Тупой
                default:
                    return "Tundmatu";
            }
        }
        public enum TriangleType
        {
            Equilateral,
            Isosceles,
            Right,
            Acute,
            Obtuse,
            NotExist
        }
        public double GetSetA
        {
            get
            { return a; }
            set
            { a = value; }
        }
        public double GetSetB
        {
            get
            { return b; }
            set
            { b = value; }
        }
        public double GetSetC
        {
            get
            { return c; }
            set
            { c = value; }
        }
        public double GetSet_hA
        {
            get
            { return heightA; }
            set
            { heightA = value; }
        }
        public double GetSet_hB
        {
            get
            { return heightB; }
            set
            { heightB = value; }
        }
        public double GetSet_hC
        {
            get
            { return heightC; }
            set
            { heightC = value; }
        }
        public string outputP()
        {
            return p.ToString();
        }
        public string outputS()
        {
            return s.ToString("F2");
        }
        public string outputA()
        {
            return a.ToString();
        }
        public string outputB()
        {
            return b.ToString();
        }
        public string outputC()
        {
            return c.ToString();
        }
        public string outputAngleAB()
        {
            return angleAB.ToString("F2");
        }

        public string outputAngleBC()
        {
            return angleBC.ToString("F2");
        }

        public string outputAngleAC()
        {
            return angleAC.ToString("F2");
        }

        public string outputHeightA()
        {
            return heightA.ToString("F2");
        }

        public string outputHeightB()
        {
            return heightB.ToString("F2");
        }

        public string outputHeightC()
        {
            return heightC.ToString("F2");
        }

    }
}
