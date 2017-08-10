using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.GCore;

namespace Game
{
    public class UserController : Controller
    {
        public UserController(Model model)
            : base(model)
        {
            Game.mouse.addEventListener(GCore.MouseEvent.CLICK, onClick);
        }
        private void onClick(Event e)
        {

        }
    }
}
