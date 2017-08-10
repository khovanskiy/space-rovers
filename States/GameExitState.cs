namespace Game.States
{
    public class GameExitState : State
    {
        public override void init()
        {
            Game.core.form.Close();
        }
    }
}