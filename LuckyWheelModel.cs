using System.Collections.Generic;
using _Development.Scripts.LootLevel;
using _Development.Scripts.Roulette.Interface;
using BLINK.RPGBuilder.Characters;
using BLINK.RPGBuilder.LogicMono;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;

namespace _Development.Scripts.Roulette
{
    public class LuckyWheelModel : MonoBehaviour
    {
        public WheelRotationController RotationController;
        [SerializeField] private RouletteSettings _rouletteSettings;

        [SerializeField] private AdButton _adButton;
        [SerializeField] private Button _buttonSpin;
        [SerializeField] private WheelView _wheelView;
        [SerializeField] private ViewButtonSpin _viewButtonSpin;
        [SerializeField] private RoulettePrizeDistributionController _prizeDistribution;
        [SerializeField] private int _winnerSlot;

        private int _winnerPrize;

        private Dictionary<int, SlotModel> _slotsModels;
        private IRewardCalculator _rewardCalculator;
        private int _countCurrency;
        private int _maxQuantitySpin;
        private bool _isReadyAd;

        private void OnEnable()
        {
            _buttonSpin.onClick.AddListener(Click);
            _adButton.AdvertisementViewed += RunSpin;
            RotationController.FinishedRotation += _prizeDistribution.GivePrize;
        }

        private void Start()
        {
            CheckReadyUpdateCrystal();
            InitializeSlotModels();
            InitializeView();
            _winnerPrize = SetWinnerPrize(_winnerSlot);
            _prizeDistribution.Initialize(_slotsModels, _winnerPrize);
            _adButton.Initialized(_rouletteSettings.QuantityADInDay);
        }

        private void Update()
        {
            if (CheckTryPaySpin() == false && RotationController.IsSpin == false)
            {
                if (_adButton.TryButtonShow() == false)
                    _isReadyAd = false;
            }
            else
                _adButton.ButtonAD.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _buttonSpin.onClick.RemoveListener(Click);
            _adButton.AdvertisementViewed -= RunSpin;
            RotationController.FinishedRotation -= _prizeDistribution.GivePrize;
        }

        public void SetWinSlot(int winnerSlot) =>
            _winnerSlot = winnerSlot;

        public bool CheckTryPaySpin() =>
            Character.Instance.getCurrencyAmount(_rouletteSettings.CurrencyIsSpentOnSpins) -
            _rouletteSettings.WheelSpinCost >= 0;

        private void Click()
        {
            if (RotationController.IsSpin || CheckTryPaySpin() == false)
            {
                if (_isReadyAd == false && RotationController.IsSpin == false)
                    RPGBuilderEssentials.Instance.Shop.gameObject.SetActive(true);

                return;
            }

            PaySpin();
            RunSpin();
        }

        private void RunSpin()
        {
            _winnerPrize = SetWinnerPrize(_winnerSlot);
            _prizeDistribution.ResetWinnerPrize(_winnerPrize);
            RotationController.StartRotatingWheel(_winnerPrize, _slotsModels.Count);
            GeneralEvents.Instance.OnSpinnedRoulette();
        }

        private void InitializeSlotModels()
        {
            _rewardCalculator = new RewardCalculator();
            _slotsModels = new Dictionary<int, SlotModel>();

            for (int i = 0; i < _wheelView.Slots.Count; i++)
            {
                RPGLootTable lootTable = _rouletteSettings.RewardSpins[i];
                int countWithMultiplier = _rewardCalculator.GetCountWithMultiplier(_rouletteSettings, lootTable);
                int numberSlot = _wheelView.Slots[i].Number;
                _slotsModels.Add(numberSlot, new SlotModel(lootTable, countWithMultiplier));
            }
        }

        private void InitializeView()
        {
            _wheelView.ShowRewards(_slotsModels);
            _viewButtonSpin.SetButtonInfo(_rouletteSettings.WheelSpinCost.ToString(),
                _rouletteSettings.CurrencyIsSpentOnSpins);
        }

        private int SetWinnerPrize(int win)
        {
            if (win <= _slotsModels.Count || win < 0)
                return Random.Range(0, _slotsModels.Count);

            return win;
        }

        private void PaySpin()
        {
            _countCurrency = Character.Instance.getCurrencyAmount(_rouletteSettings.CurrencyIsSpentOnSpins);
            _countCurrency -= _rouletteSettings.WheelSpinCost;
            EconomyUtilities.setCurrencyAmount(_rouletteSettings.CurrencyIsSpentOnSpins, _countCurrency);
            GeneralEvents.Instance.OnAmountOfPlayersCurrencyChanged(_rouletteSettings.CurrencyIsSpentOnSpins,
                _countCurrency);
        }

        private void CheckReadyUpdateCrystal() // TODO доработать обновление после таймера.
        {
        }
    }
}