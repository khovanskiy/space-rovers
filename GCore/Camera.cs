using SharpDX;

namespace Game.GCore
{
    public class Camera
    {
        private static Camera _instance;
        private float _x;
        private float _y;
        private float _scaleX = 1;
        private float _scaleY = 1;
        private float _rotationZ;
        public static int width = 800;
        public static int height = 600;
        public const int sw = 1920;
        public const int sh = 1200;
        public static Matrix dpi;

        private Matrix cameraCache = Matrix.Identity;
        private bool shouldCameraUpdate = true;

        public Camera()
        {
            dpi = Matrix.Scaling(GraphicCore.getInstance().form.Width / (float) sw,
                GraphicCore.getInstance().form.Height / (float) sh, 1);
        }

        public Matrix matrix
        {
            get
            {
                if (shouldCameraUpdate)
                {
                    shouldCameraUpdate = false;
                    GraphicCore.MCOUNT++;
                    cameraCache = Matrix.Transformation2D(new Vector2(x, y), 0, new Vector2(scaleX, scaleY),
                        new Vector2(x, y), rotationZ, new Vector2(sw / 2 - x, sh / 2 - y));
                }
                return cameraCache;
            }
        }

        public float x
        {
            get { return _x; }
            set
            {
                _x = value;
                spoilMe();
            }
        }

        public float y
        {
            get { return _y; }
            set
            {
                _y = value;
                spoilMe();
            }
        }

        public float rotationZ
        {
            get { return _rotationZ; }
            set
            {
                _rotationZ = value;
                spoilMe();
            }
        }

        public float scaleX
        {
            get { return _scaleX; }
            set
            {
                _scaleX = value;
                spoilMe();
            }
        }

        public float scaleY
        {
            get { return _scaleY; }
            set
            {
                _scaleY = value;
                spoilMe();
            }
        }

        public void spoilMe()
        {
            shouldCameraUpdate = true;
        }

        public static Camera getInstance()
        {
            if (_instance == null)
            {
                _instance = new Camera();
            }
            return _instance;
        }

        public void clear()
        {
            x = 0;
            y = 0;
            scaleX = scaleY = 1;
            rotationZ = 0;
        }
    }
}