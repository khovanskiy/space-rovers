namespace Game
{
    public class GameNullState : State
    {
        public GameNullState()
        {
            GCore.Resource.getXml("UI\\Templates\\main_menu.xml");
        }

        override public void release()
        {
        }

        override public void render()
        {
            dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "intro"));
        }
    }
}