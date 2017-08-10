using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.GCore
{
    public interface ISource
    {
        SharpDX.Direct2D1.Bitmap getSource();
    }
}
