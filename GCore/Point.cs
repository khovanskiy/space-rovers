using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.GCore
{
    public class Point
    {
        public float x=0;
        public float y=0;
        public Point(float x = 0, float y = 0)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// Возвращает угол между векторами в радианах
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        public static float angle(Point pt1, Point pt2)
        {
            Console.WriteLine("Point #1 = " + pt1.x + " " + pt1.y);
            Console.WriteLine("Point #2 = " + pt2.x + " " + pt2.y);
            double l1 = Math.Sqrt(pt1.x * pt1.x + pt1.y * pt1.y);
            double l2 = Math.Sqrt(pt2.x * pt2.x + pt2.y * pt2.y);
            double t = (pt1.x * pt2.x + pt1.y * pt2.y) / (l1*l2);
            Console.WriteLine(l1+" "+l2+" "+t);
            return (float)Math.Acos(t);
        }
        /// <summary>
        /// Возвращает расстояние между точками pt1 и pt2.
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        public static double distance(Point pt1, Point pt2)
        {
            return Math.Sqrt((pt1.x - pt2.x) * (pt1.x - pt2.x) + (pt1.y - pt2.y) * (pt1.y - pt2.y));
        }
    }
}
