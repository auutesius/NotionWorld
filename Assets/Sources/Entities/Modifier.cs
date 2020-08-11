namespace NotionWorld.Entities
{
    public abstract class Modifier
    {
        public abstract void TakeEffect(Entity entity);
    }

    public abstract class RollBackModifier : Modifier
    {
        public abstract void RollBackEffect(Entity entity);
    }
}