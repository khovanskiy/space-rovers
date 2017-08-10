namespace Game.GCore
{
    public class Background : DisplayObjectContainer
    {
        static Background _instance;

        private Background()
        {
            GraphicCore core = GraphicCore.getInstance();
            core.addEventListener(Event.ENTER_FRAME, handleEvent);
            _isDisplayed = true;
            _rendertype = RenderType.BACKGROUND;
        }

        public static Background getInstance()
        {
            if (_instance == null)
            {
                _instance = new Background();
            }
            return _instance;
        }

        protected void handleEvent(Event e)
        {
            dispatchEvent(new Event(this, e.type));
        }
    }
}