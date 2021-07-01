using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lab6
{
    //Треугольник на плоскости
    class Triangle2D : NotifyPropertyChanged
    {
        
        private Point2D[] points = new Point2D[3];     
        public Point2D PointA
        {
            get => points[0];
            set
            {
                points[0] = value;
                RisePropertyChanged("PointA");
            }

        }    
        public Point2D PointB
        {
            get => points[1];
            set
            {
                points[1] = value;
                RisePropertyChanged("PointB");
            }
        }
        public Point2D PointC
        {
            get => points[2];
            set
            {
                points[2] = value;
                RisePropertyChanged("PointC");
            }
        }
        public Triangle2D()
        {
            for (int i = 0; i < 3; i++)
            {
                points[i] = new Point2D();
            }
        }
        //Периметр
        public double Perimeter
        { 
            get 
            {
                return GetCLength() + GetBLength() + GetALength();
            } 
        }
        //Площадь
        public double Square
        {
            get
            {
                double a = GetALength();
                double b = GetBLength();
                double c = GetCLength();
                double p = (a + b + c) / 2.0;

                return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            }
        }
        //Проверка на существование (образуют ли 3 точки треугольник?)
        public bool IsValid
        {
            get
            {
                return (PointA.X * (PointB.Y - PointC.Y) + PointB.X * (PointC.Y - PointA.Y) + PointC.X * (PointA.Y - PointB.Y)) != 0;
            }

        }
        //получить точку по индексу
        public Point2D GetPoint(int idx)
        {
            if (idx > 2)
                throw new IndexOutOfRangeException("Index of point out of range");

            return points[idx];
        }
        //центр тяжести
        public Point2D GravityPoint
        {
            get
            {
                double x = 0;
                double y = 0;
                foreach(Point2D pt in points)
                {
                    x += pt.X;
                    y += pt.Y;
                }
                return new Point2D(x / 3.0, y / 3.0);
            }
        }
        //конструктор с инициализацией всех вершин
        public Triangle2D(Point2D[] pts)
        {
            if(pts.Length < 3)
            {
                throw new ArgumentOutOfRangeException("Array of points shound have at least 3 values");
            }
            for (int i = 0; i < 3; i++)
            {
                points[i] = new Point2D(pts[i].X, pts[i].Y);
            }
                 
        }
        //Сравнить треугольники
        public Relation CompareSquare(Triangle2D other, bool checkForSame = false)
        {
            if (!other.IsValid)
                return Relation.Incorrect;
            if (checkForSame && this == other)
                return Relation.Same;

            if (Square < other.Square)
            {
                return Relation.Smaller;
            }
            else if(Square > other.Square)
            {
                return Relation.Bigger;
            }
            return Relation.Equal;
        }
        //переместить
        public void Move(double x, double y)
        {
            foreach(Point2D pt in points)
            {
                pt.X += x;
                pt.Y += y;
            }
        }
        //масштабировать
        public void Scale(double factor)
        {
            if (factor < 0)
                throw new Exception("Scale factor can`t be negative");
            foreach (Point2D pt in points)
            {
                pt.X *= factor;
                pt.Y *= factor;
            }
        }
        //расстояние между вершинами
        private double GetLengthBetween(Point2D a, Point2D b)
        {
            return Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2)); 
        }
        //Длина стороны C
        public double GetCLength()
        {
            return GetLengthBetween(PointC, PointA);
        }
        //Длина стороны B
        public double GetBLength()
        {
            return GetLengthBetween(PointB, PointC);
        }
        //Длина стороны A
        public double GetALength()
        {
             return GetLengthBetween(PointA, PointB);
        }


    }
}
