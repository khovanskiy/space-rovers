using System;
using System.Collections.Generic;
using Game.GCore;
using RegistrySystem;

namespace Game.Astronomy.Objects
{
    public class SpaceObject : RegistryObject, ITickable
    {
        public const int SpaceSystem = 0;
        public const int Star = 1;
        public const int Gas_Body = 2;
        public const int Solid_Body = 3;
        public const int Asteroid = 4;
        public const int Asteroid_Belt = 5;

        public float size = 0.1f;
        public string spaceobject_name;
        public int spaceobject_type;
        public RelativeMovement relative_movement;
        public float local_x;
        public float local_y;
        public float local_angle;
        public string src = "";

        public SpaceObject(string spaceobject_name, int spaceobject_type,
            string parent_object_id, RelativeMovement relative_movement)
        {
            this.spaceobject_name = spaceobject_name;
            this.spaceobject_type = spaceobject_type;
            parent_id = parent_object_id;
            links = new List<string>();
            this.relative_movement = relative_movement;
        }

        public SpaceObject()
        {
        }

        public void setType(int type)
        {
            spaceobject_type = type;
        }

        public void nextTick(float dt)
        {
            if (relative_movement.spin_cw)
            {
                relative_movement.spin_angle += relative_movement.spin_speed * dt;
            }
            else
            {
                relative_movement.spin_angle -= relative_movement.spin_speed * dt;
            }
            if (relative_movement.prec_cw)
            {
                relative_movement.orb_angle += relative_movement.precession * dt;
            }
            else
            {
                relative_movement.orb_angle -= relative_movement.precession * dt;
            }

            if (relative_movement.ang_v > 1e-9f)
            {
                float ecos = 1 - relative_movement.eccentr * (float) Math.Cos(relative_movement.ell_angle);
                float da = dt * ecos * ecos * relative_movement.ang_v;

                if (relative_movement.ell_cw)
                {
                    relative_movement.ell_angle += da;
                }
                else
                {
                    relative_movement.ell_angle -= da;
                }

                float r = (ecos / (1 + relative_movement.eccentr)) * relative_movement.radius;
                local_x = r * (float) Math.Cos(relative_movement.ell_angle);
                local_y = -r * (float) Math.Sin(relative_movement.ell_angle);
                local_angle = relative_movement.ell_angle + relative_movement.orb_angle;
            }
            else
            {
                local_x = 0;
                local_y = 0;
                local_angle = 0;
            }
            dispatchEvent(new Event(this, Event.CHANGE));
            for (int i = 0; i < links.Count; i++)
            {
                ((SpaceObject) Registry.getInstance().getElement(links[i])).nextTick(dt);
            }
            //Console.WriteLine("NEXT TICK " + this.spaceobject_name); 
        }
    }
}