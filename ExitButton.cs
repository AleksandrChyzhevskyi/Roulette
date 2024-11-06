using System.Collections;
using _Development.Scripts.Upgrade.Data;
using BLINK.RPGBuilder.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace _Development.Scripts.Roulette
{
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private WheelRotationController _wheelRotationController;

        private int _countApp;

        private void OnEnable()
        {
            _button.onClick.AddListener(Hide);
            StartCoroutine(ShowButton());

            if (Character.Instance.CharacterData.Level >= 5 && _countApp == 0)
                _countApp = PlayerPrefsAppCount.LoadAppCount();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Hide);
            StopCoroutine(ShowButton());
        }

        private void Hide()
        {
            if (Character.Instance.CharacterData.Level >= 5
                && _countApp == 1)
            {
                AppRating.Instance.RateAndReview();
                _countApp++;
                PlayerPrefsAppCount.SaveAppCount(_countApp);
            }
            
            if (_wheelRotationController.IsSpin == false)
                _wheelRotationController.gameObject.SetActive(false);
        }

        private IEnumerator ShowButton()
        {
            while (_wheelRotationController.IsSpin)
            {
                if (_button.gameObject.activeInHierarchy)
                    _button.gameObject.SetActive(false);

                yield return new WaitForSeconds(0.5f);
            }

            _button.gameObject.SetActive(true);
        }
    }
}