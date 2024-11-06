using System.Collections.Generic;
using UnityEngine;

namespace _Development.Scripts.Roulette
{
    public class WheelView : MonoBehaviour
    {
        public List<Slot> Slots;

        public void ShowRewards(Dictionary<int, SlotModel> tables)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                Slots[i].SetModel(tables[i]);
                Slots[i].Show();
            }
        }
    }
}