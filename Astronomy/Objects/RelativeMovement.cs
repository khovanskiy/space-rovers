using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Engine.MyParser;

namespace Engine.Astronomy.Objects
{
    [Serializable()]
    public class RelativeMovement
    {
        public float ell_angle, eccentr, ang_v, orb_angle, precession, spin_speed, spin_angle, hor_axis;
        public bool ell_cw, spin_cw, prec_cw;

        public RelativeMovement(float ell_angle = 0, float eccentr = 0, float ang_v = 0, float orb_angle = 0, float precession = 0, 
            float spin_speed = 0, float spin_angle = 0, float hor_axis = 0, bool ell_cw = false, bool spin_cw = false, bool prec_cw = false)
        {
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
