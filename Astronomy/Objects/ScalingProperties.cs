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
    public class ScalingProperties
    {
        public float system_scale, self_scale;

        public ScalingProperties(float system_scale = 1, float self_scale = 1)
        {
            this.system_scale = system_scale;
            this.self_scale = self_scale;
        }

        public ScalingProperties()
        {
        }
    }
}
