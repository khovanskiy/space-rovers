using Game.GCore;

namespace Game.States
{
    public class GameOptionsState : State
    {
        UITemplate ui;

        public override void init()
        {
            //Console.WriteLine("New Game");
            ui = new UITemplate(Resource.getXml("UI\\Templates\\options_menu.xml"));
            ui.addEventListener(MouseEvent.CLICK, onClick);
            ui.layout.x = 0;
            Game.interfaceView.addChild(ui.layout);
        }

        public override void render()
        {
        }

        public override void release()
        {
            Game.interfaceView.removeChild(ui.layout);
            ui.clear();
        }

        private void onClick(Event e)
        {
            switch (e.target.id)
            {
                case "start":
                {
                    dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "generate"));
                }
                    break;
                case "back":
                {
                    dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "menu"));
                }
                    break;
            }
        }
    }
}