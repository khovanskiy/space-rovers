using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IState
    {
        void init();
        void focus();
        void render();
        void defocus();
        void release();
    }
}
