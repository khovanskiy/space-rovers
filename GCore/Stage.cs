using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GCore
{
    public class Stage : DisplayObjectContainer
    {
        static Stage _instance = null;

        private Stage()
        {
            GraphicCore core = GraphicCore.getInstance();
            core.addEventListener(Event.ENTER_FRAME, handleEvent);
            _isDisplayed = true;
            _rendertype = RenderType.STAGE;
        }
        public static Stage getInstance()
        {
            if (_instance == null)
            {
                _instance = new Stage();
            }
            return _instance;
        }
        protected void handleEvent(Event e)
        {
            dispatchEvent(new Event(this, e.type));
        }
    }
}
