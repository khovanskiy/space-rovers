using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        private List<EventLink> keeper;
        private List<ChildLink> keeper2;
        public ListenersManager()
        {
            keeper = new List<EventLink>();
            keeper2 = new List<ChildLink>();
        }
        public void add(DisplayObjectContainer parent, DisplayObject child)
        {
            parent.addChild(child);
            ChildLink link = new ChildLink();
            link.parent = parent;
            link.child = child;
            keeper2.Add(link);
        }
        public void add(IEventDispatcher d, String type, Function func)
        {
            d.addEventListener(type, func);
            EventLink link=new EventLink();
            link.dispatcher=d;
            link.type=type;
            link.func=func;
            keeper.Add(link);
        }
        public void clearListeners()
        {
            for (int i = 0; i < keeper.Count; ++i)
            {
                EventLink link = keeper[i];
                link.dispatcher.removeEventListener(link.type, link.func);
            }
            keeper.Clear();
        }
        public void clearChilds()
        {
            for (int i = 0; i < keeper2.Count; ++i)
            {
                ChildLink link = keeper2[i];
                link.parent.removeChild(link.child);
            }
            keeper2.Clear();
        }
        public void clearAll()
        {
            clearListeners();
            clearChilds();
        }
    }
}
