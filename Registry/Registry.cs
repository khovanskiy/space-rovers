using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace RegistrySystem
{
    [Serializable()]
    public class Registry 
    {
        private static Registry _instance;
        private static int maxUnprotectedID = 20000;
        private const int IdsAllocation = 10000;
        private Stack<int> freeIDs;
        public Hashtable data;
        private Registry()
        {
            this.data = new Hashtable();
            this.freeIDs = new Stack<int>();
            for (int i = maxUnprotectedID; i > 0; --i)
            {
                freeIDs.Push(i);
            }
            // Заполнение стека свободными незащищёнными идентификаторами            
        }
        public static Registry getInstance()
        {
            if (_instance == null)
            {
                _instance = new Registry();
            }
            return _instance;
        }
        /// <summary>
        /// Возвращает элемент по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RegistryObject getElement(string id)
        {
            if (this.data.Contains(id))
            {
                return (RegistryObject)data[id];
            }
            return null;
        }
        public void clear()
        {
            data.Clear();
            this.freeIDs = new Stack<int>();
            for (int i = maxUnprotectedID; i > 0; --i)
            {
                freeIDs.Push(i);
            }
        }
        public Object GetRawElement(string Id)
        {
            if (this.data.Contains(Id))
            {
                return data[Id];
            }
            return null;
        }
        /// <summary>
        /// Возвращает незанятый ID
        /// </summary>
        /// <returns></returns>
        private string getNextId()
        {
            if (this.freeIDs.Count == 0)
            {
                maxUnprotectedID += IdsAllocation;
                for (int i = maxUnprotectedID; i > maxUnprotectedID - IdsAllocation; --i)
                {
                    this.freeIDs.Push(i);
                }
            }
            return "u" + this.freeIDs.Pop().ToString();
        }

        public string addElement(RegistryObject element, string id = null)
        {
            if (id == null || id == "")
            {
                id = getNextId();
            }
            element.my_id = id;
            Console.WriteLine("Add to registry id="+id+" ");
            if (!this.data.Contains(id))
            {
                this.data.Add(id, element);
            }
            if (element.parent_id != null)
            {
                if (this.data.Contains(element.parent_id))
                {
                    (getElement(element.parent_id)).links.Add(id);
                }
            }
            return id;
        }

        public void removeElement(string id, bool live_parent = true)
        {
            if (this.data.Contains(id))
            {
                if (id[0] == 'u')
                {
                    this.freeIDs.Push(Int32.Parse(id.Substring(1, id.Length - 1)));
                }
                string Removed_parent_id = getElement(id).parent_id;
                if (Removed_parent_id != null)
                {
                    if (live_parent && this.data.Contains(Removed_parent_id))
                    {
                        getElement(Removed_parent_id).links.Remove(id);
                    }
                }
                this.data.Remove(id);
            }
        }

        public void RemoveWithInners(string Id, bool live_parent = true)
        {
            if (this.data.Contains(Id))
            {
                List<string> children = getElement(Id).links;
                foreach (string id in children)
                {
                    RemoveWithInners(id, false);
                }
                removeElement(Id, live_parent);
            }
        }
    }
}
