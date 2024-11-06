using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Development.Scripts.Roulette
{
    public class ViewButtonSpin : MonoBehaviour
    {
        public Image currencyIcon;
        public TextMeshProUGUI amountText;

        public void SetButtonInfo(string text, RPGCurrency currency)
        {
            currencyIcon.sprite = currency.entryIcon;
            amountText.text = text;
        }
    }
}