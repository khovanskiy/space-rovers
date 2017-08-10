using System;
using System.Collections.Generic;
using Game.GCore;

namespace Game
{
    public class StateContext
    {
        private List<State> _states;

        public StateContext()
        {
            _states = new List<State>(10);
            turnOn("null");
            Game.core.addEventListener(Event.ENTER_FRAME, onRender);
        }

        private void onRender(Event e)
        {
            for (int i = 0; i < _states.Count; i++)
            {
                if (_states[i] != null)
                {
                    if (_states[i].getRendering())
                    {
                        _states[i].render();
                    }
                }
            }
        }

        private void turnOn(String name)
        {
            State s = StateFactory.create(name);
            s.addEventListener(StateEvent.CHANGE_STATE, handler);
            s.addEventListener(StateEvent.TURN_ON, handler);
            s.addEventListener(StateEvent.TURN_OFF, handler);
            if (_states.Count > 0)
            {
                _states[_states.Count - 1].defocus();
            }
            _states.Add(s);
            s.init();
            s.focus();
        }

        private void turnOff()
        {
            if (_states.Count > 1)
            {
                State last = _states[_states.Count - 1];
                last.removeAll();
                last.defocus();
                last.release();
                _states.RemoveAt(_states.Count - 1);
                last = _states[_states.Count - 1];
                last.focus();
            }
        }

        private void changeState(String name)
        {
            for (int i = 0; i < _states.Count; i++)
            {
                _states[i].removeAll();
                _states[i].defocus();
                _states[i].release();
            }
            _states.Clear();
            GC.Collect();
            turnOn(name);
        }

        private void handler(Event e)
        {
            Console.WriteLine("Handle " + e.type);
            StateEvent ev = (StateEvent) e;
            Console.WriteLine("Next " + ev.next);
            switch (ev.type)
            {
                case StateEvent.TURN_ON:
                {
                    turnOn(ev.next);
                }
                    break;
                case StateEvent.TURN_OFF:
                {
                    turnOff();
                }
                    break;
                case StateEvent.CHANGE_STATE:
                {
                    changeState(ev.next);
                }
                    break;
            }
        }

        /*private void setState(State state)
        {
            state.addEventListener(StateEvent.CHANGE_STATE, changeState);
            state.addEventListener(StateEvent.TURN_ON,
            if (_state != null)
            {
                _state.release();
                _state.removeAll();
            }
            _state = state;
            _state.init();
        }*/
    }
}