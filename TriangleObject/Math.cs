
namespace Triangle.TriangleObject
{
    public class Math
    {
        private Triangle tr;

        public Math(Triangle triangle)
        {
            tr = triangle;
        }
        public void FindPerimeter()
        {
            tr.p = tr.a + tr.b + tr.c;
        }

        public void FindSurface()
        {
            double p = tr.p / 2;
            tr.s = System.Math.Sqrt(p * (p - tr.a) * (p - tr.b) * (p - tr.c));
        }

        public double FindAngle(double side1, double side2, double oppositeSide)
        {
            // opposite cos for side1 
            double cosAngle = (side2 * side2 + oppositeSide * oppositeSide - side1 * side1) / (2 * side2 * oppositeSide);
            // finding arc cos for to get arc value in radiants
            double angleInRadians = System.Math.Acos(cosAngle);
            // converting arc value to dergees
            angleInRadians = angleInRadians * (180 / System.Math.PI);
            return angleInRadians;
        }
        public double FindHeight(double side)
        {
            double height = (2 * tr.s) / side;
            return System.Math.Round(height, 2); // Ограничиваем до 2 знаков после запятой
        }


        // равносторонний
        public bool IsEquilateral()
        {
            return tr.a == tr.b && tr.b == tr.c;
        }

        // равнобедренный
        public bool IsIsosceles()
        {
            return (tr.a == tr.b || tr.a == tr.c || tr.b == tr.c) && !IsEquilateral();
        }

        // прямоугольный 
        public bool IsRight()
        {
            double a2 = tr.a * tr.a;
            double b2 = tr.b * tr.b;
            double c2 = tr.c * tr.c;

            return System.Math.Abs(a2 + b2 - c2) < 0.0001 ||
                   System.Math.Abs(a2 + c2 - b2) < 0.0001 ||
                   System.Math.Abs(b2 + c2 - a2) < 0.0001;
        }

        // остроугольный
        public bool IsAcute()
        {
            return tr.angleAB < 90 && tr.angleBC < 90 && tr.angleAC < 90;
        }
        // тупоугольный
        public bool IsObtuse()
        {
            return tr.angleAB > 90 || tr.angleBC > 90 || tr.angleAC > 90;
        }


    }
}
