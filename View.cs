using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    abstract public class View : GCore.Sprite
    {
        protected Model model;
        protected GCore.DisplayObjectContainer container;

        public View(Model model, GCore.DisplayObjectContainer container)
        {
            this.model = model;
            this.container = container;
            container.addChild(this);
        }
    }
}
