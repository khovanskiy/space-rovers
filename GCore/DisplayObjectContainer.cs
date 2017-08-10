using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GCore
{
    abstract public class DisplayObjectContainer : DisplayObject
    {
        public List<DisplayObject> children = new List<DisplayObject>();
        protected float tx = 0;
        protected float ty = 0;

        public DisplayObject addChild(DisplayObject child)
        {
            if (child.setParent(this))
            {
                child.update();
                children.Add(child);
            }
            return child;
        }
        public void clear()
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] is DisplayObjectContainer)
                {
                    ((DisplayObjectContainer)children[i]).clear();
                }
                removeChild(children[i]);
            }
            children.Clear();
        }
        public void removeChild(DisplayObject child)
        {
            if (child.removeParent(this))
            {
                child.update();
                children.Remove(child);
            }
        }
        public List<DisplayObject> getChildrenList()
        {
            return this.children;
        }
        override public void update()
        {
            _update();
            for (int i = 0; i < children.Count; i++)
            {
                children[i].update();
            }
        }
    }
}
