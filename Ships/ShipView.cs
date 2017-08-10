using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.GCore;

namespace Game.Ships
{
    public class ShipView
    {
        /*public Ship _model;
        MovieClip hull_view = new MovieClip();
        public ShipView(Ship model, DisplayObjectContainer container)
        {
            _model = model;
            _model.addEventListener(Event.CHANGE, handler);
            _model.addEventListener(ShipEvent.PART_CHANGE, handler);
            this.addChild(hull_view);
            hull_view.addEventListener(MouseEvent.CLICK, onClick);
            reloadHull();
        }
        private void onClick(Event e)
        {
            e.target = this;
            dispatchEvent(e);
        }
        private void reloadHull()
        {
            if (_model.getHull() != null)
            {
                hull_view.removeEventListener(MouseEvent.CLICK, onClick);
                removeChild(hull_view);
                hull_view = new MovieClip();
                hull_view.addEventListener(MouseEvent.CLICK, onClick);
                hull_view.load(new string[] { "DATA\\Ships\\" + _model.getHull().type + ".png" });
                hull_view.width = 50;
                hull_view.moveAxisToCenter();
                hull_view.stop();
                addChild(hull_view);
            }
        }
        ~ShipView()
        {
            hull_view.removeEventListener(MouseEvent.CLICK, onClick);
            _model.removeEventListener(Event.CHANGE, handler);
            _model.removeEventListener(ShipEvent.PART_CHANGE, handler);
        }
        public void handler(Event e)
        {
            switch (e.type)
            {
                case Event.CHANGE:
                    {
                        this.x = _model.pos.x;
                        this.y = _model.pos.y;
                        this.rotationZ = _model.angle;
                    } break;
                case ShipEvent.PART_CHANGE:
                    {
                        reloadHull();
                    } break;
            }
        }*/
    }
}
