using System;
using System.Collections.Generic;

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

        private void onDelete(Event e)
        {
            removeChild((DisplayObject) e.target);
        }

        public void clear()
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] is DisplayObjectContainer)
                {
                    ((DisplayObjectContainer) children[i]).clear();
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

        public override SharpDX.RectangleF getRenderBounds()
        {
            if (shouldBoundsUpdate)
            {
                shouldBoundsUpdate = false;
                float maxY = Single.MinValue;
                float maxX = Single.MinValue;
                float minY = Single.MaxValue;
                float minX = Single.MaxValue;
                for (int i = 0; i < children.Count; i++)
                {
                    SharpDX.RectangleF bounds = children[i].getRenderBounds();
                    if (maxY < bounds.Bottom)
                    {
                        maxY = bounds.Bottom;
                    }
                    if (minY > bounds.Top)
                    {
                        minY = bounds.Top;
                    }
                    if (maxX < bounds.Right)
                    {
                        maxX = bounds.Right;
                    }
                    if (minX > bounds.Left)
                    {
                        minX = bounds.Left;
                    }
                }
                _width = maxX - minX;
                _height = maxY - minY;
                boundsCache = new SharpDX.RectangleF(minX, minY, maxX, maxY);
            }
            return boundsCache;
        }

        override public void update()
        {
            _update();
            for (int i = 0; i < children.Count; i++)
            {
                children[i].update();
            }
        }

        override public void spoilMe()
        {
            shouldLocalUpdate = true;
            spoilUp();
            spoilDown();
        }

        override public void spoilDown()
        {
            shouldBoundsUpdate = true;
            shouldGlobalUpdate = true;
            for (int i = 0; i < children.Count; i++)
            {
                children[i].spoilDown();
            }
        }
    }
}