namespace Game.GCore
{
    public class Keyboard : EventDispatcher
    {
        public const int A = 65;
        public const int B = 66;
        public const int C = 67;
        public const int D = 68;
        public const int E = 69;

        public const int ENTER = 13;
        public const int SPACE = 32;
        public const int ESCAPE = 27;
        public const int F5 = 116;
        public const int F6 = 117;
        static Keyboard _instance;

        private Keyboard()
        {
            var core = GraphicCore.getInstance();
            core.form.KeyDown += (sender, args) =>
            {
                dispatchEvent(new KeyboardEvent(this, KeyboardEvent.KEY_DOWN, args.KeyValue));
            };
            core.form.KeyUp += (sender, args) =>
            {
                dispatchEvent(new KeyboardEvent(this, KeyboardEvent.KEY_UP, args.KeyValue));
            };
        }

        public static Keyboard getInstance()
        {
            if (_instance == null)
            {
                _instance = new Keyboard();
            }
            return _instance;
        }
    }
}