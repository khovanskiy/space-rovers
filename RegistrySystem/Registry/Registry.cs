using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Engine.RegistrySystem.Objects;

namespace Engine.RegistrySystem.Registry
{
    [Serializable()]
    public class Registry 
    {
        private static Registry Instance;
        private static int MaxUnprotectedId = 20000;
        private const int IdsAllocation = 10000;
        private Stack<int> FreeIds;
        public Hashtable Data;
        private Registry()
        {
            this.Data = new Hashtable();
            this.FreeIds = new Stack<int>();
            for (int i = MaxUnprotectedId; i > 0; --i) FreeIds.Push(i); 
            // Заполнение стека свободными незащищёнными идентификаторами            
        }
        public static Registry getInstance()
        {
            if (Instance == null) Instance = new Registry();
            return Instance;
        }

        public RegistryObject GetElement(string Id)
        {
            if (this.Data.Contains(Id))
                return (RegistryObject)Data[Id];
            else 
                return null;
        }

        public Object GetRawElement(string Id)
        {
            if (this.Data.Contains(Id))
                return Data[Id];
            else 
                return null;
        }

        private string GetId()
        {
            if (this.FreeIds.Count == 0)
            {
                MaxUnprotectedId += IdsAllocation;
                for (int i = MaxUnprotectedId; i > MaxUnprotectedId - IdsAllocation; --i) this.FreeIds.Push(i);
            }
            return "u" + this.FreeIds.Pop().ToString();
        }

        public string AddElement(RegistryObject Element, string Id = null)
        {
            if (Id == null) Id = GetId();
            Element.my_id = Id;
            if (!this.Data.Contains(Id)) this.Data.Add(Id, Element);
            if (Element.parent_object_id != null)
                if (this.Data.Contains(Element.parent_object_id)) (GetElement(Element.parent_object_id)).inner_objects.Add(Id);
            return Id;
        }

        public void RemoveElement(string Id, bool live_parent = true)
        {
            if (this.Data.Contains(Id))
            {
                if (Id[0] == 'u') this.FreeIds.Push(Int32.Parse(Id.Substring(1, Id.Length - 1)));
                string Removed_parent_id = GetElement(Id).parent_object_id;
                if (Removed_parent_id != null)
                    if (live_parent && this.Data.Contains(Removed_parent_id)) GetElement(Removed_parent_id).inner_objects.Remove(Id);
                this.Data.Remove(Id);
            }
        }

        public void RemoveWithInners(string Id, bool live_parent = true)
        {
            if (this.Data.Contains(Id))
            {
                List<string> Inners = GetElement(Id).inner_objects;
                foreach (string id in Inners) RemoveWithInners(id, false);
                RemoveElement(Id, live_parent);
            }
        }
    }
}
