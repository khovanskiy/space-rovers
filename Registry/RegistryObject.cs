using System;
using System.Collections.Generic;
using Game.GCore;

namespace Game.Registry
{
    [Serializable()]
    public abstract class RegistryObject : EventDispatcher
    {
        public List<string> links = new List<string>();
        public List<RegistryObject> children = new List<RegistryObject>();
        public string my_id, parent_id;
    }
}
