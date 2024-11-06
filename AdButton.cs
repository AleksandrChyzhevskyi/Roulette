using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Development.Scripts.Roulette
{
    public class AdButton : MonoBehaviour
    {
        public event Action AdvertisementViewed;
        public Button ButtonAD;

        private int _quantityADInDay;
        private int _counterAD;
        private int _countCurrency;
        private RPGCurrency _currency;

        private void OnEnable()
        {
            ButtonAD.onClick.AddListener(WatchAd);
            IronSourceRewardedVideoEvents.onAdRewardedEvent += OnRewardedAdReceivedRewardEvent;
        }

        public bool TryButtonShow()
        {
            if (Advertising.instance.IsCanShowRewardedAd() == false)
                return false;

            if (CheckCounterADInDay())
            {
                ButtonAD.gameObject.SetActive(true);
                return true;
            }

            return false;
        }

        private void OnDisable()
        {
            ButtonAD.onClick.RemoveListener(WatchAd);
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= OnRewardedAdReceivedRewardEvent;
        }

        public void Initialized(int quantityADInDay)
        {
            _quantityADInDay = quantityADInDay;
            _counterAD = 0;
        }

        private void OnRewardedAdReceivedRewardEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo ironSourceAdInfo) => 
            AdvertisementViewed?.Invoke();

        private void WatchAd()
        {
            _counterAD++;
            Advertising.instance.TryWatchRewardedAd();
            ButtonAD.gameObject.SetActive(false);
        }

        private bool CheckCounterADInDay() =>
            _quantityADInDay > _counterAD;
    }
}