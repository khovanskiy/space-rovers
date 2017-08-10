using System;

namespace Astronomy
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