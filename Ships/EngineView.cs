using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.GCore;

namespace Game.Ships
{
    public class EngineInfoView : Sprite
    {
        Engine _model = null;
        TextField tf;
        public EngineInfoView(Engine _model)
        {
            this._model = _model;
            tf = new TextField("");
            tf.text = "Power " + _model.power;
            addChild(tf);
        }
    }

}
