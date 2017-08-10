using System;
using System.Collections.Generic;

namespace Game.RegistrySystem.Objects
{
    [Serializable()]
    public abstract class RegistryObject 
    {
        public List<string> inner_objects;
        public string my_id, parent_object_id;
        public abstract void NextTick(float dt);        
    }
}
