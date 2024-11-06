using _Development.Scripts.Extensions;
using _Development.Scripts.Roulette.Interface;
using BLINK.RPGBuilder.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Development.Scripts.Roulette
{
    public class Slot : MonoBehaviour
    {
        [Range(0, 7)] public int Number;
        public Image ImageSprite;
        public TextMeshProUGUI Text;

        private ISlotModel _model;

        public void SetModel(ISlotModel model) =>
            _model = model;

        public void Show()
        {
            ImageSprite.sprite = GameDatabase.Instance.GetItems()[_model.Table.GetLootItem().itemID].entryIcon;
            Text.text = _model.RewardCount.ToString();
        }
    }
}