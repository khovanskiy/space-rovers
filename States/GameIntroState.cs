namespace Game.States
{
    public class GameIntroState : State
    {
        public override void init()
        {
            dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "menu"));
        }
    }
}