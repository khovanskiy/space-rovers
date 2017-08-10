using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameNullState : State
    {
        private GCore.TextField tf;
        public GameNullState()
        {
            tf=new GCore.TextField("загрузка...");
            Game.interfaceView.addChild(tf);
            GCore.Resource.getXml("UI\\Templates\\main_menu.xml");
            GCore.Resource.getBitmap("space.jpg");
        }
        override public void release()
        {
            Game.interfaceView.removeChild(tf);
        }
        override public void render()
        {
            dispatchEvent(new StateEvent(this,StateEvent.CHANGE_STATE,"generate"));
        }
    }
}
