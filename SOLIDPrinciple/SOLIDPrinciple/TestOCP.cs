using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDPrinciple
{
    
    /*A software module/class is open for extension and closed for modification*/

    public class Rectangle
    {
        public double Height { get; set; }
        public double Width { get; set; }
    }
    public class Circle
    {
        public double Radius { get; set; }
    }

    /// <summary>
    /// Using this class always need modifications if new shape is introduced....This does not uses OCP.
    /// </summary>
    public class AreaCalculator
    {
        public double TotalArea(object[] arrObjects)
        {
            double area = 0;
            Rectangle objRectangle;
            Circle objCircle;
            foreach (var obj in arrObjects)
            {
                if (obj is Rectangle)
                {
                    objRectangle = (Rectangle)obj;
                    area += objRectangle.Height * objRectangle.Width;
                }
                else
                {
                    objCircle = (Circle)obj;
                    area += objCircle.Radius * objCircle.Radius * Math.PI;
                }
            }
            return area;
        }
    }

    /// <summary>
    /// Adding abstract class and method and using it as a base class for Circle,Rectangle and other shpes
    /// will follows OCP.
    /// </summary>
    public abstract class Shape
    {
        public abstract double Area();
    }

    public class RectangleWithOCP : Shape
    {
        public double Height { get; set; }
        public double Width { get; set; }

        public override double Area()
        {
            return Height * Width;
        }
    }
    public class CircleWithOCP : Shape
    {
        public double Radius { get; set; }

        public override double Area()
        {
            return Radius * Radius * Math.PI;
        }
    }

    /// <summary>
    /// Every shape contains its area with its own way of calculation 
    /// functionality and our AreaCalculator class will become simpler than before.
    /// IN this we application is closed for modifications but can be extended.....
    /// </summary>
    public class AreaCalculatorWithOCP
    {
        public double TotalArea(Shape[] arrShapes)
        {
            double area = 0;
            foreach (var objShape in arrShapes)
            {
                area += objShape.Area();
            }
            return area;
        }
    }
    

}
