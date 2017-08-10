using System;

namespace Game.Astronomy.Objects
{
    [Serializable()]
    public class RelativeMovement
    {
        /// <summary>
        /// Эталонный радиус
        /// </summary>
        public float radius;

        /// <summary>
        /// Локальный угол поворота в радианах
        /// </summary>
        public float ell_angle;

        /// <summary>
        /// Эксцентриситет орбиты
        /// </summary>
        public float eccentr;

        /// <summary>
        /// Эталонная угловая скорость
        /// </summary>
        public float ang_v;

        /// <summary>
        /// Угол поворота самой орбиты к осям координат в радианах
        /// </summary>
        public float orb_angle;

        /// <summary>
        /// Прецессия орбиты
        /// </summary>
        public float precession;

        /// <summary>
        /// Скорость вращения вокруг оси в радиан/dt
        /// </summary>
        public float spin_speed;

        /// <summary>
        /// Текущий угол поворота вокруг оси
        /// </summary>
        public float spin_angle;

        /// <summary>
        /// Угол наклона оси вращения к орбите
        /// </summary>
        public float hor_axis;

        /// <summary>
        /// Движение по орбите по часовой стрелке
        /// </summary>
        public bool ell_cw;

        /// <summary>
        /// Движение вокруг оси по часовой стрелке
        /// </summary>
        public bool spin_cw;

        /// <summary>
        /// Прецессия орбиты поворачивает её по часовой стрелке
        /// </summary>
        public bool prec_cw;

        public RelativeMovement(float radius = 0, float ell_angle = 0, float eccentr = 0, float ang_v = 0,
            float orb_angle = 0, float precession = 0,
            float spin_speed = 0, float spin_angle = 0, float hor_axis = 0, bool ell_cw = false, bool spin_cw = false,
            bool prec_cw = false)
        {
            this.radius = radius;
            this.ell_angle = ell_angle;
            this.eccentr = eccentr;
            this.ang_v = ang_v;
            this.orb_angle = orb_angle;
            this.precession = precession;
            this.spin_speed = spin_speed;
            this.spin_angle = spin_angle;
            this.hor_axis = hor_axis;
            this.ell_cw = ell_cw;
            this.spin_cw = spin_cw;
            this.prec_cw = prec_cw;
        }

        public RelativeMovement()
        {
        }
    }
}