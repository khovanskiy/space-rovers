using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.AI
{
    public class Selector : Composite
    {
        protected override int execute()
        {
            for (int i = 0; i < children.Count; ++i)
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
