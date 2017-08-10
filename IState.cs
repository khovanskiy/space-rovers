namespace Game
{
    public interface IState
    {
        void init();
        void focus();
        void render();
        void defocus();
        void release();
    }
}