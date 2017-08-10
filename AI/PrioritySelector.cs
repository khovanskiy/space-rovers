using System.Collections.Generic;

namespace Game.AI
{
    public class PrioritySelector : Composite
    {
        private class SortByPriority : IComparer<Behavior>
        {
            public int Compare(Behavior x, Behavior y)
            {
                if (x.priority < y.priority)
                {
                    return 1;
                }
                if (x.priority > y.priority)
                {
                    return -1;
                }
                return 0;
            }
        }

        protected override int execute()
        {
            if (isChildrenUpdated)
            {
                children.Sort(new SortByPriority());
            }
            for (int i = 0; i < children.Count; i++)
            {
                int s = children[i].tick();
                if (s != Status.FAILURE)
                {
                    return s;
                }
            }
            return Status.FAILURE;
        }
    }
}