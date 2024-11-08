﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Triangle
{
    class Triangle
    {
        public double a, b, c;
        private double _angleA, _angleB, _angleC;
        public double _perimeter, _surface;
        public TriangleType Type { get; private set; }
        public Triangle(double A, double B, double C) {

            a = A; b = B; c = C;

            if (ExistTriangle)
            {
                _angleA = CalculateAngle(A, B, C);
                _angleB = CalculateAngle(B, A, C);
                _angleC = CalculateAngle(C, A, B);
                FindPerimeter();
                FindSurface();
                DetermineTriangleType();
            }
            else
            {
                Type = TriangleType.NotExist; 
            }
        }
        public void FindPerimeter()
        {
            _perimeter = a + b + c;
        }
        public void FindSurface()
        {
            double p = _perimeter / 2;
            _surface = Math.Sqrt((p * (p - a) * (p-b) * (p-c)));
        }

        public double CalculateAngle(double side1, double side2, double oppositeSide)
        {
            // opposite cos for side1 
            double cosAngle = (side2 * side2 + oppositeSide * oppositeSide - side1 * side1) / (2 * side2 * oppositeSide);
            // finding arc cos for to get arc value in radiants
            double angleInRadians = Math.Acos(cosAngle);
            // converting arc value to dergees
            angleInRadians = angleInRadians * (180 / Math.PI);
            return angleInRadians;
        }

        private void DetermineTriangleType()
        {
            if (IsEquilateral)
            {
                Type = TriangleType.Equilateral;  
                return; 
            }

            if (IsIsosceles)
            {
                Type = TriangleType.Isosceles; 
                return;
            }

            Type = TriangleType.Scalene;
        }
        public void SaveDataXML()
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                NewLineOnAttributes = false
            };
            string filePath = @"..\..\data.xml";

            try
            {
                if (!File.Exists(filePath))
                {
                    using (XmlWriter writer = XmlWriter.Create(filePath, settings))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("Triangles");
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                XmlElement triangleElem = doc.CreateElement("Triangle");

                var fieldValues = this.GetType()
                     .GetFields()
                     .Select(field => field.GetValue(this))
                     .ToList();

                string[] fieldNames = { "Side a:", "Side b:", "Side c:", "Angle A:", "Angle B:", "Angle C:", "Perimeter:", "Surface:", "Type:" };

                for (int i = 0; i < fieldValues.Count && i < fieldNames.Length; i++)
                {
                    XmlElement fieldElement = doc.CreateElement(fieldNames[i].Replace(" ", ""));
                    fieldElement.InnerText = fieldNames[i] + " " + fieldValues[i]?.ToString();
                    triangleElem.AppendChild(fieldElement);
                }

                doc.DocumentElement.AppendChild(triangleElem);
                doc.Save(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // properties
        public bool IsEquilateral
        {
            get
            {
                return a == b && b == c && a == c;
            }
        }

        public bool IsIsosceles
        {
            get
            {
                return (a == b || a == c || b == c) && !IsEquilateral;
            }
        }

        public bool IsScalene
        {
            get
            {
                return a != b && b != c && a != c;
            }
        }

        public bool IsRight
        {
            get
            {
                return Math.Abs(AngleA - 90) < 0.0001 ||
                       Math.Abs(AngleB - 90) < 0.0001 ||
                       Math.Abs(AngleC - 90) < 0.0001;
            }
        }

        public bool IsObtuse
        {
            get
            {
                return AngleA > 90 || AngleB > 90 || AngleC > 90;
            }
        }

        public bool IsAcute
        {
            get
            {
                return AngleA < 90 && AngleB < 90 && AngleC < 90;
            }
        }

        public double AngleA
        {
            get
            {
                return _angleA;
            }
            set { _angleA = value; }
        }

        public double AngleB
        {
            get
            {
                return _angleB;
            }
            set { _angleB = value; }
        }

        public double AngleC
        {
            get
            {
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
                default:
                    return "Не существует";
            }
        }

    }
    public enum TriangleType
    {
        Equilateral,
        Isosceles,
        Scalene,
        NotExist
    }

}
