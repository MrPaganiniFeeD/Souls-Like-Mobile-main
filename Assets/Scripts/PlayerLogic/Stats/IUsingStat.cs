namespace DefaultNamespace.Stats
{
    public interface IUsingStat
    {
        bool TryUse(int cost);
        bool CheckValue(int value);
    }
}