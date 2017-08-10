using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrySystem
{
    [Serializable()]
    public abstract class RegistryObject : Game.GCore.EventDispatcher
    {
        public List<string> links = new List<string>();
        public List<RegistryObject> children = new List<RegistryObject>();
        public string my_id, parent_id;
    }
}
