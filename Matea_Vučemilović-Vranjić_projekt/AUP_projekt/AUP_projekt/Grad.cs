using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUP_projekt
{
    internal class Grad
    {
        public int ID { get; }
        public double X { get; }
        public double Y { get; }

        public Grad(int id, double x, double y)
        {
            ID = id;
            X = x;
            Y = y;
        }

        public double DistanceTo(Grad drugiGrad)
        {
            double deltaX = X - drugiGrad.X;
            double deltaY = Y - drugiGrad.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
