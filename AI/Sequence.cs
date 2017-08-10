using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.AI
{
    public class Sequence : Composite
    {
        protected override int execute()
        {
            Console.WriteLine("Sequence");
            for (int i = 0; i < children.Count; i++)
            {
                int s = children[i].tick();
                if (s != Status.SUCCESS)
                {
                    return s;
                }
            }
            return Status.SUCCESS;
        }
    }
}
