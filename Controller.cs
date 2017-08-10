using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    abstract public class Controller
    {
        protected Model model;
        public Controller(Model model)
        {
            this.model = model;
        }
    }
}
