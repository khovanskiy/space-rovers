using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class DisplayTextAI:AI.Behavior
    {
        String text = "";
        public DisplayTextAI(String text)
        {
            this.text = text;
        }
        protected override int execute()
        {
            Console.WriteLine("Write: " + text);
            return AI.Status.SUCCESS;
        }
    }
}
