using BLINK.RPGBuilder.LogicMono;
using UnityEngine;

namespace _Development.Scripts.Roulette
{
    public class ActiveRoulette : MonoBehaviour
    {
        private WheelRotationController _wheelRotationController;

        private void Start()
        {
            _wheelRotationController = RPGBuilderEssentials.Instance.luckyWheelModelObject.RotationController;
            _wheelRotationController.SetPositionThrowReward(transform);
        }

        public void ShowPanel()
        {
            _wheelRotationController.gameObject.SetActive(true);
        }
    }
}