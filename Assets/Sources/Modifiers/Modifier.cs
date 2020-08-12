namespace NotionWorld.Modifiers
{
    public abstract class Modifier
    {
        public abstract void TakeEffect();
    }

    public abstract class RollBackModifier : Modifier
    {
        public abstract void RollBackEffect();
    }
}