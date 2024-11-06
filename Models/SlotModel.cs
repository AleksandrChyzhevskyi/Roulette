using _Development.Scripts.Roulette.Interface;

namespace _Development.Scripts.Roulette
{
    public class SlotModel : ISlotModel
    {
        public RPGLootTable Table { get; }
        public int RewardCount { get; }

        public SlotModel(RPGLootTable table, int rewardCount)
        {
            Table = table;
            RewardCount = rewardCount;
        }
    }
}