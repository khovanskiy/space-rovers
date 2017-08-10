using Game.GCore;

namespace Game
{
    public class NewGameState : State
    {
        UITemplate ui;

        public NewGameState()
        {
        }

        override public void init()
        {
            ui = new UITemplate(Resource.getXml("UI\\Templates\\new_game_menu.xml"));
            ui.addEventListener(MouseEvent.CLICK, onClick);
            ui.layout.x = 0;
            Game.interfaceView.addChild(ui.layout);
        }

        override public void render()
        {
        }

        override public void release()
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