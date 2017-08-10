namespace Game.Ships
{
    public abstract class Component : RegistrySystem.RegistryObject
    {
        public int level = 0;
        public int type = 0;
        public int quality = 0;
        public float score = 0f;
        public abstract Component likeIt();
    }
}
