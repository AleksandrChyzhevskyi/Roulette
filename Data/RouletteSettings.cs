using System.Collections.Generic;
using _Development.Scripts.Roulette.Data;
using UnityEngine;

namespace _Development.Scripts.Roulette
{
    [CreateAssetMenu(fileName = "LootOnRoulette", menuName = "Loot/LootOnRoulette")]
    public class RouletteSettings : ScriptableObject
    {
        public RPGLootTable[] RewardSpins = new RPGLootTable[8];

        public List<PlayerRewardMultiplier> PlayerRewardMultipliers =
            new List<PlayerRewardMultiplier>();

        public int WheelSpinCost;
        public RPGCurrency CurrencyIsSpentOnSpins;
        public int QuantityADInDay;
    }
}