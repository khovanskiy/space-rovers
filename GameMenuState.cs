using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game.Engine;

namespace Game
{
    public class GameMenuState : State
    {
        UITemplate ui;
        public GameMenuState()
        {
            //Background.getInstance().addChild(Resource.getBitmap("space.jpg"));
            ui = new UITemplate(Resource.getXml("UI\\Templates\\main_menu.xml"));
            ui.addEventListener(MouseEvent.CLICK, onClick);
            ui.layout.x = 150;
            Game.interfaceView.addChild(ui.layout);
        }
        private void onClick(Event e)
        {
            switch (e.target.id)
            {
                case "new_game":
                    {
                        dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "new_game"));
                    } break;
                case "settings":
                    {
                        dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "settings"));
                    } break;
                case "exit":
                    {
                        dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "intro"));
                    }break;
            }
        }
        override public void logic()
        {
            
        }
        override public void release()
        {
            Game.interfaceView.removeChild(ui.layout);
            ui.clear();
        }
    }
}
