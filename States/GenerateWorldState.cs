using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class GenerateWorldState : State
    {
        UITemplate ui;
        public GenerateWorldState()
        {
            
        }
        override public void init()
        {
            ui = new UITemplate(GCore.Resource.getXml("UI\\Templates\\loading.xml"));
            keeper.add(Game.interfaceView, ui.layout);

            WorldFactory wf = new WorldFactory(GCore.Resource.getXml("SETTINGS\\world.xml"));
            wf.generate();

            Player player = new Player();
            RegistrySystem.Registry.getInstance().addElement(player, "player");

            dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "gameplay"));
        }
        override public void render()
        {
        }
        override public void release()
        {
            keeper.clearAll();
            ui.clear();
        }
    }
}
