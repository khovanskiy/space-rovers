using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class GameExitState : State
    {
        public GameExitState()
        {
        }
        public override void init()
        {
            Game.core.form.Close();
        }
    }
}
