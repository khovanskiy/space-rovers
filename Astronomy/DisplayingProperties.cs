using System;


namespace Astronomy
{
    [Serializable()]
    public class DisplayingProperties
    {
        public float x, y, self_angle, system_angle
            ; //x, y - координаты вывода изображения, self_angle - угол поворота изображения, system_angle - угол поворота системы, не влияет на вывод

        public float total_size
            ; //total_size - размер(например, по диагонали) изображения, которое будет выведено на экран

        public DisplayingProperties(float x = 0, float y = 0, float self_angle = 0, float system_angle = 0,
            float total_size = 0)
        {
            this.x = x;
            this.y = y;
            this.self_angle = self_angle;
            this.system_angle = system_angle;
            this.total_size = total_size;
        }

        public DisplayingProperties()
        {
        }
    }
}