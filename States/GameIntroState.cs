namespace Game
{
    public class GameIntroState : State
    {
        override public void init()
        {
            dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "menu"));
        }
    }
}