using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Game.GCore
{
    public class Mouse : EventDispatcher
    {
        public static int x = 0;
        public static int y = 0;
        private int _delta = 0;
        static Mouse _instance = null;
        private Mouse()
        {
            var core = GraphicCore.getInstance();
            core.form.MouseMove += (sender, args) =>
            {
                x = args.X;
                y = args.Y;
                //SharpDX.Matrix dpi = SharpDX.Matrix.Scaling(core.form.Width / 1920.0f, core.form.Height / 1200.0f, 1);
                //dpi.Invert();
                //x = (int)(x * dpi.M11 + y * dpi.M21 + dpi.M41);
                //y = (int)(x * dpi.M12 + y * dpi.M22 + dpi.M42);
                //x = System.Windows.Forms.Cursor.Position.X;
                //y = System.Windows.Forms.Cursor.Position.Y;
                //x = System.Windows.Forms.Control.MousePosition.X;
                //y = System.Windows.Forms.Control.MousePosition.Y;
                dispatchEvent(new MouseEvent(this, MouseEvent.MOUSE_MOVE, x, y));
            };
            core.form.MouseWheel += (sender, args) =>
            {
                _delta = args.Delta;
                dispatchEvent(new MouseEvent(this, MouseEvent.MOUSE_WHEEL, args.X, args.Y, false, args.Delta));
            };
            core.form.MouseClick += (sender, args) =>
            {
                x = args.X;
                y = args.Y;
                dispatchEvent(new MouseEvent(this, MouseEvent.CLICK, x, y, true));
            };
            core.form.MouseUp += (sender, args) =>
            {
                x = args.X;
                y = args.Y;
                dispatchEvent(new MouseEvent(this, MouseEvent.MOUSE_UP, x, y, false));
            };
            core.form.MouseDown += (sender, args) =>
            {
                x = args.X;
                y = args.Y;
                dispatchEvent(new MouseEvent(this, MouseEvent.MOUSE_DOWN, x, y, true));
            };
        }
        private void handleEvent(Event e)
        {
            if (e.type == Event.ADDED_TO_STAGE)
            {
                ((InteractiveObject)e.target).follow(MouseEvent.CLICK, this);
                ((InteractiveObject)e.target).follow(MouseEvent.MOUSE_DOWN, this);
                ((InteractiveObject)e.target).follow(MouseEvent.MOUSE_UP, this);
                ((InteractiveObject)e.target).follow(MouseEvent.MOUSE_MOVE, this);
                e.target.removeEventListener(Event.ADDED_TO_STAGE, handleEvent);
                e.target.addEventListener(Event.REMOVED_FROM_STAGE, handleEvent);
            }
            else if (e.type == Event.REMOVED_FROM_STAGE)
            {
                ((InteractiveObject)e.target).unfollow(MouseEvent.CLICK, this);
                ((InteractiveObject)e.target).unfollow(MouseEvent.MOUSE_DOWN, this);
                ((InteractiveObject)e.target).unfollow(MouseEvent.MOUSE_UP, this);
                ((InteractiveObject)e.target).unfollow(MouseEvent.MOUSE_MOVE, this);
                e.target.removeEventListener(Event.REMOVED_FROM_STAGE, handleEvent);
                e.target.addEventListener(Event.ADDED_TO_STAGE, handleEvent);
            }
        }
        public void follow(String type, EventDispatcher ed)
        {
            Console.WriteLine("Follow " + type + " " + ed.GetType() + " " + ed.id);
            ed.addEventListener(type, handleEvent);
        }
        public static Mouse getInstance()
        {
            if (_instance == null)
            {
                _instance = new Mouse();
            }
            return _instance;
        }
    }
}
