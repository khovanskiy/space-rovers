using Game.GCore;

namespace Game.States
{
    public class GameNullState : State
    {
        private TextField tf;

        public GameNullState()
        {
            tf = new TextField("загрузка...");
            Game.interfaceView.addChild(tf);
            Resource.getXml("UI\\Templates\\main_menu.xml");
            Resource.getBitmap("space.jpg");
        }

        public override void init()
        {
            dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "intro"));
        }

        public override void release()
        {
            Game.interfaceView.removeChild(tf);
        }
    }
}