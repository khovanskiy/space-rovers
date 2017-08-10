using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game.GCore;

namespace Game
{
    public class GameModeContext
    {
        private State _state;
        public GameModeContext()
        {
            setState(new GameNullState());
            Game.core.addEventListener(Event.ENTER_FRAME, onRender);
        }
        private void onRender(Event e)
        {
            _state.render();
        }
        private void setState(State state)
        {
            state.addEventListener(StateEvent.CHANGE_STATE, changeState);
            if (_state != null)
            {
                _state.release();
                _state.removeEventListener(StateEvent.CHANGE_STATE, changeState);
            }
            _state = state;
            _state.init();
        }
        private void changeState(Event e)
        {
            StateEvent se=(StateEvent)e;
            
        }
    }
}
