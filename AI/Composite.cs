using System.Collections.Generic;

namespace Game.AI
{
    public abstract class Composite : Behavior
    {
        protected List<Behavior> children = new List<Behavior>();
        private bool _isChanged = false;

        public void addChild(Behavior child)
        {
            children.Add(child);
            _isChanged = true;
        }

        public void removeChild(Behavior child)
        {
            children.Remove(child);
            _isChanged = true;
        }

        protected bool isChildrenUpdated
        {
            get { return _isChanged; }
        }
    }
}