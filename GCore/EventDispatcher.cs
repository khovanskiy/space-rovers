using System;
using System.Collections.Generic;

namespace Game.GCore
{
    public abstract class EventDispatcher : IEventDispatcher
    {
        protected Dictionary<String, List<Function>> listeners = new Dictionary<string, List<Function>>();
        public string id = "";
        private bool dispaching = false;

        public void addEventListener(String type, Function listener)
        {
            if (!listeners.ContainsKey(type))
            {
                listeners[type] = new List<Function>();
            }
            listeners[type].Add(listener);
        }

        public void removeEventListener(String type, Function listener)
        {
            if (!listeners.ContainsKey(type))
            {
                listeners[type] = new List<Function>();
            }
            listeners[type].Remove(listener);
            dispaching = false;
        }

        public void removeAll()
        {
            foreach (KeyValuePair<String, List<Function>> pair in listeners)
            {
                pair.Value.Clear();
            }
            listeners.Clear();
        }

        virtual public void dispatchEvent(Event e)
        {
            if (listeners.ContainsKey(e.type))
            {
                dispaching = true;
                int n = listeners[e.type].Count;
                for (int i = 0; i < n; i++)
                {
                    if (dispaching)
                    {
                        listeners[e.type][i](e);
                    }
                }
                dispaching = false;
            }
        }

        public bool hasEventListener(String type)
        {
            return listeners[type].Count > 0;
        }
    }
}