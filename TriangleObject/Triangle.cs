using Triangle.Interface;

namespace Triangle.TriangleObject
{
    public class Triangle
    {
        public double a, b, c;
        public double _angleAB, _angleBC, _angleAC;
        public double _perimeter, _surface;
        public TriangleType Type { get; private set; }
        private Math _math;
        public DataManager dataManager;

        public Triangle(double A, double B, double C)
        {
            a = A; b = B; c = C;
            _math = new Math(this);

            if (ExistTriangle)
            {
                _angleAB = _math.CalculateAngle(b, c, a);
                _angleBC = _math.CalculateAngle(a, b, c);
                _angleAC = _math.CalculateAngle(c, a, b);

                _math.FindPerimeter();
                _math.FindSurface();
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

        public float AngleAB => (float)_angleAB;
        public float AngleBC => (float)_angleBC;
        public float AngleAC => (float)_angleAC;

        public float Perimeter => (float)_perimeter;
        public float Surface => (float)_surface;

        public bool ExistTriangle
        {
            get
            {
                return a + b > c && a + c > b && b + c > a;
            }
        }

        public void DetermineTriangleType(Math math)
        {
            if (math.IsEquilateral())
                Type = TriangleType.Equilateral;
            else if (math.IsIsosceles())
                Type = TriangleType.Isosceles;
            else if (math.IsScalene())
                Type = TriangleType.Scalene;
            else if (math.IsRight())
                Type = TriangleType.Right;
            else
                Type = TriangleType.NotExist;
        }

        public string TypeRusky()
        {
            switch (Type)
            {
                case TriangleType.Equilateral:
                    return "Равносторонний";
                case TriangleType.Isosceles:
                    return "Равнобедренный";
                case TriangleType.Scalene:
                    return "Разносторонний";
                case TriangleType.Right:
                    return "Прямоугольный";
                default:
                    return "Не существует";
            }
        }
        public enum TriangleType
        {
            Equilateral,
            Isosceles,
            Scalene,
            Right,
            NotExist
        }
    }
}
