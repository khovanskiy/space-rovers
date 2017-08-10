using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Engine.MyParser;
using Engine.RegistrySystem.Objects;
using Engine.Astronomy.Core;

namespace Engine.Astronomy.Objects
{
    [Serializable()]
    public enum SpaceObjectType
    {
        SpaceSystem = 0,
        Star = 1,
        Gas_Body = 2,
        Solid_Body = 3,
        Asteroid = 4,
        Asteroid_Belt = 5,
    }
    [Serializable()]
    public class SpaceObject : RegistryObject
    {
        public string spaceobject_name;
        public int spaceobject_type;       
        public ScalingProperties scaling_properties;
        public RelativeMovement relative_movement;
        public DisplayingProperties result;

        public SpaceObject(string spaceobject_name, int spaceobject_type, string parent_object_id, 
            ScalingProperties scaling_properties, RelativeMovement relative_movement)
        {
            this.spaceobject_name = spaceobject_name;
            this.spaceobject_type = spaceobject_type;
            this.parent_object_id = parent_object_id;
            this.inner_objects = new List<string>();
            this.scaling_properties = scaling_properties;
            this.relative_movement = relative_movement;
            this.result = new DisplayingProperties();           
        }

        public SpaceObject()
        {
        }

        public override void NextTick(float dt)
        {
            AstronomyCore.NextSpaceObjectTick(this, dt);                        
        }       

    }
}
