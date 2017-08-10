using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.GCore
{
    public class Interface : DisplayObjectContainer
    {
        static Interface _instance = null;
        
        private Interface()
        {
            GraphicCore core = GraphicCore.getInstance();
            core.addEventListener(Event.ENTER_FRAME,handleEvent);
            _isDisplayed = true;
            _rendertype = RenderType.INTERFACE;
        }
        
        public static Interface getInstance()
        {
            if (_instance == null)
            {
                _instance = new Interface();
            }
            return _instance;
        }
        protected void handleEvent(Event e)
        {
            dispatchEvent(new Event(this, e.type));
        }
    }
}
