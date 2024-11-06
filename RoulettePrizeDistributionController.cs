using System.Collections.Generic;
using _Development.Scripts.Extensions;
using _Development.Scripts.LootLevel;
using UnityEngine;

namespace _Development.Scripts.Roulette
{
    public class RoulettePrizeDistributionController : MonoBehaviour
    {
        private int _winnerPrize;
        private IRewardCalculator _rewardCalculator;
        private Dictionary<int, SlotModel> _slotsModels;

        public void Initialize(Dictionary<int, SlotModel> slotsModels, int winnerPrize)
        {
            _slotsModels = slotsModels;
            _winnerPrize = winnerPrize;
            _rewardCalculator = new RewardCalculator();
        }

        public void ResetWinnerPrize(int winnerPrize) =>
            _winnerPrize = winnerPrize;

        public void GivePrize() =>
            _rewardCalculator.PutInInventory(_slotsModels[_winnerPrize].Table.GetLootItem(),
                _slotsModels[_winnerPrize].RewardCount);
    }
}