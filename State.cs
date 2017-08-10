namespace Game
{
    public abstract class State : GCore.EventDispatcher, IState
    {
        protected GCore.ListenersManager keeper = new GCore.ListenersManager();
        private bool _isRendering = true;

        protected void setRendering(bool t)
        {
            _isRendering = t;
        }

        public bool getRendering()
        {
            return _isRendering;
        }

        /// <summary>
        /// Метод, вызывающийся при добавлении в очередь состояний.
        /// </summary>
        /*public void finalInit()
        {
            init();
        }*/
        virtual public void init()
        {
        }

        /*public void finalFocus()
        {
            focus();
        }*/
        virtual public void focus()
        {
        }

        /// <summary>
        /// Метод, вызывающийся при каждой перерисовке экрана
        /// </summary>
        /*public void finalRender()
        {
            render();
        }*/
        virtual public void render()
        {
        }

        /*public void _defocus()
        {
            keeper.clearListeners();
            defocus();
        }*/
        virtual public void defocus()
        {
            keeper.clearListeners();
        }

        /// <summary>
        /// Метод, вызывающийся при удалении объекта из очереди состояний
        /// </summary>
        /*public void finalRelease()
        {
            release();
        }*/
        virtual public void release()
        {
        }
    }
}