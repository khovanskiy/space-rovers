using RegistrySystem;

namespace Game
{
    public class GenerateWorldState : State
    {
        UITemplate ui;

        public override void init()
        {
            ui = new UITemplate(GCore.Resource.getXml("UI\\Templates\\loading.xml"));
            keeper.add(Game.interfaceView, ui.layout);

            WorldFactory wf = new WorldFactory(GCore.Resource.getXml("SETTINGS\\world.xml"));
            wf.generate();

            Player player = new Player();
            Registry.getInstance().addElement(player, "player");

            dispatchEvent(new StateEvent(this, StateEvent.CHANGE_STATE, "gameplay"));
        }

        public override void render()
        {
        }

        public override void release()
        {
            keeper.clearAll();
            ui.clear();
        }
    }
}