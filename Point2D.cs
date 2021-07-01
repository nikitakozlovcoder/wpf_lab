using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6
{
    //точка на плоскости
    class Point2D : NotifyPropertyChanged
    {
        private double _x;
        private double _y;
        public double X
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    RisePropertyChanged("X");
                }
               
            }
        }
        public double Y{
            get => _y;
            set 
            {
                if (_y != value)
                {
                    _y = value;
                    RisePropertyChanged("Y");
                }
            } 
        }
        public Point2D() { }
        public Point2D(double x, double y) 
        {
            this.X = x;
            this.Y = y;
        }
        public override string ToString()
        {
            return $"x: {X} y: {Y}";
        }
    }
}
