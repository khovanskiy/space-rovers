using System;

namespace Game.GCore
{
    public class Point
    {
        public float x = 0;
        public float y = 0;

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
        public static float angle(Point from, Point to)
        {
            return (float) (-Math.Atan2(from.x - to.x, from.y - to.y) - Math.PI / 2);
        }

        public float distance(Point pt)
        {
            return distance(this, pt);
        }

        /// <summary>
        /// Возвращает расстояние между точками pt1 и pt2.
        /// </summary>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        public static float distance(Point pt1, Point pt2)
        {
            return (float) Math.Sqrt((pt1.x - pt2.x) * (pt1.x - pt2.x) + (pt1.y - pt2.y) * (pt1.y - pt2.y));
        }

        public override string ToString()
        {
            return "(" + this.x + ", " + this.y + ")";
        }
    }
}