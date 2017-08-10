using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.GCore;

namespace Game.Ships
{
    public class CannonInfoView : Sprite
    {
        Cannon _model = null;
        TextField damage, rate, radius;
        public CannonInfoView(Cannon _model)
        {
            this._model = _model;
            damage = new TextField("Damage " + _model.damage);
            rate = new TextField("Rate " + _model.rate);
            rate.y = damage.height;
            radius = new TextField("Radius " + _model.radius);
            radius.y = rate.y + rate.height;
            addChild(damage);
            addChild(rate);
            addChild(radius);
        }
    }

}
