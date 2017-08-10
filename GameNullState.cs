using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameNullState : State
    {
        private Engine.TextField tf;
        public GameNullState()
        {
            tf=new Engine.TextField("загрузка...");
            Game.interfaceView.addChild(tf);
            Resource.getXml("UI\\Templates\\main_menu.xml");
            Resource.getBitmap("space.jpg");
        }
        override public void release()
        {
            Game.interfaceView.removeChild(tf);
        }
        override public void logic()
        {
            dispatchEvent(new StateEvent(this,StateEvent.CHANGE_STATE,"intro"));
        }
    }
}
