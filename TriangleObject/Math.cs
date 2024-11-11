
namespace Triangle.TriangleObject
{
    public class Math
    {
        private Triangle _triangle;

        public Math(Triangle triangle)
        {
            _triangle = triangle;
        }
        public void FindPerimeter()
        {
            _triangle._perimeter = _triangle.a + _triangle.b + _triangle.c;
        }

        // Метод для вычисления площади
        public void FindSurface()
        {
            double p = _triangle._perimeter / 2;
            _triangle._surface = System.Math.Sqrt(p * (p - _triangle.a) * (p - _triangle.b) * (p - _triangle.c));
        }

        public double CalculateAngle(double side1, double side2, double oppositeSide)
        {
            // opposite cos for side1 
            double cosAngle = (side2 * side2 + oppositeSide * oppositeSide - side1 * side1) / (2 * side2 * oppositeSide);
            // finding arc cos for to get arc value in radiants
            double angleInRadians = System.Math.Acos(cosAngle);
            // converting arc value to dergees
            angleInRadians = angleInRadians * (180 / System.Math.PI);
            return angleInRadians;
        }

        // равносторонний
        public bool IsEquilateral()
        {
            return _triangle.a == _triangle.b && _triangle.b == _triangle.c;
        }

        // равнобедренный
        public bool IsIsosceles()
        {
            return (_triangle.a == _triangle.b || _triangle.a == _triangle.c || _triangle.b == _triangle.c) && !IsEquilateral();
        }

        // разносторонний 
        public bool IsScalene()
        {
            return _triangle.a != _triangle.b && _triangle.b != _triangle.c && _triangle.a != _triangle.c;
        }

        // прямоугольный 
        public bool IsRight()
        {
            double a2 = _triangle.a * _triangle.a;
            double b2 = _triangle.b * _triangle.b;
            double c2 = _triangle.c * _triangle.c;

            // Проверяем, что сумма квадратов двух сторон равна квадрату третьей
            return System.Math.Abs(a2 + b2 - c2) < 0.0001 ||
                   System.Math.Abs(a2 + c2 - b2) < 0.0001 ||
                   System.Math.Abs(b2 + c2 - a2) < 0.0001;
        }
    }
}
