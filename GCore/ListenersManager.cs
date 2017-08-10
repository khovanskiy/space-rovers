using System;
using System.Collections.Generic;

namespace Game.GCore
{
    public class ListenersManager
    {
        struct ChildLink
        {
            public DisplayObjectContainer parent;
            public DisplayObject child;
        }

        struct EventLink
        {
            public IEventDispatcher dispatcher;
            public String type;
            public GCore.Function func;
        }

        private List<EventLink> listeners;
        private List<EventLink> listeners_cache;
        private List<ChildLink> children;

        public ListenersManager()
        {
            listeners = new List<EventLink>();
            listeners_cache = new List<EventLink>();
            children = new List<ChildLink>();
        }

        public void add(DisplayObjectContainer parent, DisplayObject child)
        {
            parent.addChild(child);
            ChildLink link = new ChildLink();
            link.parent = parent;
            link.child = child;
            children.Add(link);
        }

        public void add(IEventDispatcher d, String type, Function func)
        {
            d.addEventListener(type, func);
            EventLink link = new EventLink();
            link.dispatcher = d;
            link.type = type;
            link.func = func;
            listeners.Add(link);
        }

        public void resumeListeners()
        {
            for (int i = 0; i < listeners_cache.Count; ++i)
            {
                EventLink link = listeners_cache[i];
                link.dispatcher.addEventListener(link.type, link.func);
                listeners.Add(listeners_cache[i]);
            }
            listeners_cache.Clear();
        }

        public void pauseListeners()
        {
            for (int i = 0; i < listeners.Count; ++i)
            {
                EventLink link = listeners[i];
                link.dispatcher.removeEventListener(link.type, link.func);
                listeners_cache.Add(link);
            }
            listeners.Clear();
        }

        public void clearListeners()
        {
            for (int i = 0; i < listeners.Count; ++i)
            {
                EventLink link = listeners[i];
                link.dispatcher.removeEventListener(link.type, link.func);
            }
            for (int i = 0; i < listeners_cache.Count; ++i)
            {
                EventLink link = listeners_cache[i];
                link.dispatcher.removeEventListener(link.type, link.func);
            }
            listeners.Clear();
            listeners_cache.Clear();
        }

        public void clearChilds()
        {
            for (int i = 0; i < children.Count; ++i)
            {
                ChildLink link = children[i];
                link.parent.removeChild(link.child);
            }
            children.Clear();
        }

        public void clearAll()
        {
            clearListeners();
            clearChilds();
        }
    }
}