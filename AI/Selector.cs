namespace Game.AI
{
    public class Selector : Composite
    {
        protected override int execute()
        {
            foreach (var t in children)
            {
                var s = t.tick();
                if (s != Status.FAILURE)
                {
                    return s;
                }
            }
            return Status.FAILURE;
        }
    }
}