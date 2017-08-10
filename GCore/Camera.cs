using SharpDX;

namespace Game.GCore
{
    public class Camera
    {
        static Camera _instance;
        public static float x;
        public static float y;
        public static float scaleX = 1;
        public static float scaleY = 1;
        public static float rotationZ;
        public static int width = 800;
        public static int height = 600;
        public const int sw = 1920;
        public const int sh = 1200;
        public static Matrix dpi;

        public Camera()
        {
            dpi = Matrix.Scaling(GraphicCore.getInstance().form.Width / (float) sw,
                GraphicCore.getInstance().form.Height / (float) sh, 1);
        }

        public static Matrix matrix
        {
            get
            {
                return Matrix.Transformation2D(new Vector2(0, 0), 0,
                    new Vector2(scaleX, scaleY), new Vector2(x, y), rotationZ,
                    new Vector2(sw / 2 - x, sh / 2 - y));
            }
        }

        public static Camera getInstance()
        {
            if (_instance == null)
            {
                _instance = new Camera();
            }
            return _instance;
        }

        public static void clear()
        {
            x = 0;
            y = 0;
            scaleX = scaleY = 1;
            rotationZ = 0;
        }
    }
}