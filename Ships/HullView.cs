using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Ships
{
    public class HullView : GCore.MovieClip
    {
        Hull _model = null;
        public HullView(Hull _model)
        {
            this._model = _model;
            this.load(new string[]{findSource(_model.type)});
            this.moveAxisToCenter();
        }
        private string findSource(int type)
        {
            return "DATA\\Ships\\" + type + ".png";
        }
    }
}
