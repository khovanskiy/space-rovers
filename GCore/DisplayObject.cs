using System;
using SharpDX;

namespace Game.GCore
{
    public abstract class DisplayObject : EventDispatcher
    {
        protected float _x;
        protected float _y;
        protected float _rotationZ;
        protected float _scaleX = 1f;
        protected float _scaleY = 1f;
        protected bool _visible = true;
        protected float _alpha = 1f;
        protected float _width = 0f;
        protected float _height = 0f;
        protected float _tx;
        protected float _ty;

        protected bool _dvisible = true;
        protected float _dalpha = 1f;
        protected Matrix _dmatrix = Matrix.Identity;

        protected bool _isLoaded = false;
        protected bool _isDisplayed;
        public bool ratio = true;
        protected RenderType _rendertype;

        public DisplayObjectContainer parent;

        public RenderType getRenderType()
        {
            return _rendertype;
        }

        public bool setParent(DisplayObjectContainer p)
        {
            if (check(this, p))
            {
                parent = p;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Преобразует объект Point из координат рабочей области (глобальных) в координаты отображаемого объекта (локальные).
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public Point globalToLocal(Point pt)
        {
            Matrix m = Camera.matrix * matrix; //Порядок важен! (см. свойства матриц)
            float lx = pt.x;
            float ly = pt.y;
            float gx = lx * m.M11 + ly * m.M21 + m.M41;
            float gy = lx * m.M12 + ly * m.M22 + m.M42;
            return new Point(gx, gy);
        }

        /// <summary>
        /// Преобразует объект Point из координат экранного объекта (локальных) в координаты рабочей области (глобальные).
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public Point localToGlobal(Point pt)
        {
            Matrix m = getGlobalMatrix(); //this.matrix * Camera.matrix;//Порядок важен! (см. свойства матриц)
            m.Invert();
            float lx = pt.x;
            float ly = pt.y;
            float gx = lx * m.M11 + ly * m.M21 + m.M41;
            float gy = lx * m.M12 + ly * m.M22 + m.M42;
            return new Point(gx, gy);
        }

        public bool removeParent(DisplayObjectContainer p)
        {
            if (parent == p)
            {
                parent = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Находится ли объект в списке отображения
        /// </summary>
        virtual public bool isDisplayed
        {
            get { return _isDisplayed; }
        }

        /// <summary>
        /// Готов ли объект к отображению после загрузки или какой-либо другой инициализации
        /// </summary>
        virtual public bool isLoaded
        {
            get { return _isLoaded; }
        }

        /// <summary>
        /// Попадает ли точка (x,y) в область отображения объекта
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool hitTestPoint(float x, float y)
        {
            RectangleF r = getBounds();
            return (r.Top <= y && y <= r.Bottom && r.Left <= x && x <= r.Right);
        }

        /// <summary>
        /// Ширина объекта в пикселях
        /// </summary>
        virtual public float width
        {
            get
            {
                var res = getGlobalMatrix();
                return _height * Math.Abs(res.M12) + _width * Math.Abs(res.M11);
            }
            set
            {
                scaleX = value / _width;
                if (ratio)
                {
                    scaleY = scaleX;
                }
            }
        }

        /// <summary>
        /// Высота объекта в пикселях
        /// </summary>
        virtual public float height
        {
            get
            {
                var res = getGlobalMatrix();
                return _width * Math.Abs(res.M12) + _height * Math.Abs(res.M11);
            }
            set
            {
                scaleY = value / _height;
                if (ratio)
                {
                    scaleX = scaleY;
                }
            }
        }

        public void setAxis(float tx, float ty)
        {
            _tx = tx;
            _ty = ty;
        }

        public void moveAxisToCenter()
        {
            setAxis(_width / 2, _height / 2);
        }

        /// <summary>
        /// Возвращает итоговую матрицу для вывода объекта на экран
        /// </summary>
        /// <returns></returns>
        public Matrix getGlobalMatrix()
        {
            if (_rendertype == RenderType.STAGE)
            {
                return matrix * Camera.matrix * Camera.dpi;
            }
            return matrix * Camera.dpi;
        }

        /// <summary>
        /// Возвращает рамку отображения для данного объекта
        /// </summary>
        /// <returns></returns>
        public RectangleF getBounds()
        {
            Matrix p = getGlobalMatrix();
            float nx = 0;
            float ny = 0;
            float x1 = float.MaxValue;
            float y1 = float.MaxValue;
            float x2 = float.MinValue;
            float y2 = float.MinValue;
            nx = p.M41;
            ny = p.M42;
            x1 = Math.Min(x1, nx);
            y1 = Math.Min(y1, ny);
            x2 = Math.Max(x2, nx);
            y2 = Math.Max(y2, ny);
            nx = p.M21 * _height + p.M41;
            ny = p.M22 * _height + p.M42;
            x1 = Math.Min(x1, nx);
            y1 = Math.Min(y1, ny);
            x2 = Math.Max(x2, nx);
            y2 = Math.Max(y2, ny);
            nx = p.M11 * _width + p.M41;
            ny = p.M12 * _width + p.M42;
            x1 = Math.Min(x1, nx);
            y1 = Math.Min(y1, ny);
            x2 = Math.Max(x2, nx);
            y2 = Math.Max(y2, ny);
            nx = p.M11 * _width + p.M21 * _height + p.M41;
            ny = p.M12 * _width + p.M22 * _height + p.M42;
            x1 = Math.Min(x1, nx);
            y1 = Math.Min(y1, ny);
            x2 = Math.Max(x2, nx);
            y2 = Math.Max(y2, ny);
            return new RectangleF(x1, y1, x2, y2);
        }

        public bool visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                update();
            }
        }

        public float x
        {
            get { return _x; }
            set
            {
                _x = value;
                update();
            }
        }

        public float y
        {
            get { return _y; }
            set
            {
                _y = value;
                update();
            }
        }

        public float rotationZ
        {
            get { return _rotationZ; }
            set
            {
                _rotationZ = value;
                update();
            }
        }

        public void setScaleXY(float s)
        {
            scaleX = scaleY = s;
        }

        public float scaleX
        {
            get { return _scaleX; }
            set
            {
                _scaleX = value;
                update();
            }
        }

        public float scaleY
        {
            get { return _scaleY; }
            set
            {
                _scaleY = value;
                update();
            }
        }

        virtual public float alpha
        {
            get { return _alpha * _dalpha; }
            set
            {
                _alpha = value;
                update();
            }
        }

        /// <summary>
        /// Возвращает матрицу перехода для локальной системы координат
        /// </summary>
        public Matrix matrix
        {
            get
            {
                return Matrix.Transformation2D(new Vector2(_tx, _ty), 0, new Vector2(_scaleX, _scaleY),
                           new Vector2(_tx, _ty), _rotationZ, new Vector2(_x - _tx, _y - _ty)) * _dmatrix;
            }
        }

        /// <summary>
        /// Обновить все свойства объекта незамедлительно
        /// </summary>
        virtual public void update()
        {
            _update();
        }

        protected void _update()
        {
            if (parent != null)
            {
                if (!isDisplayed && parent.isDisplayed)
                {
                    dispatchEvent(new Event(this, Event.ADDED_TO_STAGE));
                }
                else if (isDisplayed && !parent.isDisplayed)
                {
                    _isDisplayed = false;
                    dispatchEvent(new Event(this, Event.REMOVED_FROM_STAGE));
                }
                _isDisplayed = parent.isDisplayed;
                _dvisible = parent.visible;
                _dalpha = parent.alpha;
                _dmatrix = parent.matrix;
                _rendertype = parent.getRenderType();
            }
            else
            {
                if (isDisplayed)
                {
                    _isDisplayed = false;
                    dispatchEvent(new Event(this, Event.REMOVED_FROM_STAGE));
                }
            }
        }

        private bool check(DisplayObject child, DisplayObjectContainer parent)
        {
            if (parent == null)
                return true;
            if (child == parent)
                return false;
            return check(child, parent.parent);
        }
    }
}