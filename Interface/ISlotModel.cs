namespace _Development.Scripts.Roulette.Interface
{
    public interface ISlotModel
    {
        RPGLootTable Table { get; }
        int RewardCount { get; }
    }
}