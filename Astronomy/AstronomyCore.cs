using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistrySystem;
using Astronomy.Objects;

namespace Astronomy.Core
{
    public static class AstronomyCore
    {
        private static float eps = 1e-9f;

        private static void NormalizeAngle(ref float angle)
        {
            if (angle < 0)
            {
                angle += 2 * (float)Math.PI;
            }
            if (angle > 2 * (float)Math.PI)
            {
                angle -= 2 * (float)Math.PI;
            }
        }
        
        public static void nextSpaceObjectTick(SpaceObject SO, float dt)
        {
            if (SO.relative_movement.spin_cw)
            {
                SO.relative_movement.spin_angle += SO.relative_movement.spin_speed * dt;
            }
            else
            {
                SO.relative_movement.spin_angle -= SO.relative_movement.spin_speed * dt;
            }
            //Console.WriteLine("G " + SO.relative_movement.spin_speed);
            NormalizeAngle(ref SO.relative_movement.spin_angle);

            if (SO.relative_movement.prec_cw)
            {
                SO.relative_movement.orb_angle += SO.relative_movement.precession * dt;
            }
            else
            {
                SO.relative_movement.orb_angle -= SO.relative_movement.precession * dt;
            }
            NormalizeAngle(ref SO.relative_movement.orb_angle);

            if (SO.relative_movement.ang_v > eps)
            {
                float ecos = 1 - SO.relative_movement.eccentr * (float)Math.Cos(SO.relative_movement.ell_angle);
                float da = dt * ecos * ecos * SO.relative_movement.ang_v;

                if (SO.relative_movement.ell_cw)
                {
                    SO.relative_movement.ell_angle += da;
                }
                else
                {
                    SO.relative_movement.ell_angle -= da;
                }
                NormalizeAngle(ref SO.relative_movement.ell_angle);
            }
            float system_angle = SO.relative_movement.ell_angle + SO.relative_movement.orb_angle;
            float ecos2 = 1 - SO.relative_movement.eccentr * (float)Math.Cos(SO.relative_movement.ell_angle);
            float r = (ecos2 / (1 + SO.relative_movement.eccentr));
            SO.x = r * (float)Math.Cos(SO.result.system_angle);
            SO.y = r * (float)Math.Sin(SO.result.system_angle);
        }
        public static String g(int k)
        {
            String temp="";
            for (int i = 0; i < k; i++)
            {
                temp += " ";
            }
            return temp;
        }
        /*public static void updateSpaceObject(string spaceobject_id, int k = 0)
        {
            Registry Reg = Registry.getInstance();
            SpaceObject SO = (SpaceObject) Reg.getElement(spaceobject_id);
            nextSpaceObjectTick(SO, 86400);
            if (SO.parent_id != null)
            {
                SpaceObject SP = (SpaceObject)Reg.getElement(SO.parent_id);
                if (SO.relative_movement.ang_v > eps)
                {
                    float ecos = 1 - SO.relative_movement.eccentr * (float)Math.Cos(SO.relative_movement.ell_angle);
                    float r = (ecos / (1 + SO.relative_movement.eccentr));
                    SO.result.system_angle = SP.result.system_angle + SO.relative_movement.ell_angle + SO.relative_movement.orb_angle;
                    SO.result.self_angle = SO.result.system_angle + SO.relative_movement.spin_angle;
                    SO.result.total_size = SP.result.total_size * SO.scaling_properties.system_scale;
                    SO.result.x = SP.result.x + r * (float)Math.Cos(SO.result.system_angle);
                    SO.result.y = SP.result.y - r * (float)Math.Sin(SO.result.system_angle);
                }
                else
                {
                    SO.result.x = SP.result.x;
                    SO.result.y = SP.result.y;
                    SO.result.total_size = SP.result.total_size * SO.scaling_properties.system_scale;
                    SO.result.system_angle = SP.result.system_angle;
                    SO.result.self_angle = SO.result.system_angle + SO.relative_movement.spin_angle;
                }
            }
            foreach (string inner_id in SO.children)
            {
                if (Reg.getElement(inner_id) is SpaceObject)
                {
                    updateSpaceObject(inner_id,k+1);
                }
            }
        }    

        public static void RenewUniverse(float dx = 0, float dy = 0, float angle = 0, float scale = 1)
        {
            SpaceObject Universe = (SpaceObject) Registry.getInstance().getElement("pTheUniverse");
            Universe.result = new DisplayingProperties(dx, dy, angle, angle, scale);
            updateSpaceObject("pTheUniverse");            
        }*/
    }
}
