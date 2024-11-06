using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Development.Scripts.Roulette
{
    public class WheelRotationController : MonoBehaviour
    {
        public event Action FinishedRotation;

        [SerializeField] private Transform _wheel;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _rotationTimeMaxSpeed;
        [SerializeField] private float _accelerationTime;
        [SerializeField] private int _numberOfSpins;
        [SerializeField] private Vector2 _offset;

        public bool IsSpin { get; private set; }
        public Transform TransformThrowReward { get; private set; }

        private float _rotSpeed;
        private float _slowdownTime;
        private float _randomAngel;

        public void StartRotatingWheel(int slotWin, int countSlots) =>
            StartCoroutine(SpinWheel(slotWin, countSlots));

        public void SetPositionThrowReward(Transform transform) =>
            TransformThrowReward = transform;

        private IEnumerator SpinWheel(int slotWin, int countSlots)
        {
            SetWin(slotWin, countSlots);
            IsSpin = true;

            float elapsedTime = 0f;
            while (elapsedTime < _accelerationTime)
            {
                _rotSpeed = Mathf.Lerp(0, _rotationSpeed, elapsedTime / _accelerationTime);
                _wheel.rotation *= Quaternion.Euler(0, 0, _rotSpeed * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;
            while (elapsedTime < _rotationTimeMaxSpeed)
            {
                _wheel.rotation *= Quaternion.Euler(0, 0, _rotationSpeed * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            float slowdown = CalculatePrizeAngle();
            elapsedTime = 0f;
            
            while (elapsedTime < _slowdownTime)
            {
                _rotSpeed -= slowdown * Time.deltaTime;
                _wheel.rotation *= Quaternion.Euler(0, 0, _rotSpeed * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            FinishedRotation?.Invoke();
            IsSpin = false;
        }

        private float CalculatePrizeAngle()
        {
            float distance = _numberOfSpins * 360f + _randomAngel - _wheel.rotation.eulerAngles.z;
            _slowdownTime = (2 * distance) / _rotationSpeed;
            float slowdown = _rotationSpeed / _slowdownTime;
            _rotSpeed = _rotationSpeed;
            return slowdown;
        }

        private void SetWin(int win, int countSlots)
        {
            float maxAngel = 360f / countSlots * (win + 1);
            float minAngel = 360f / countSlots * win;
            _randomAngel = Random.Range(minAngel + _offset.x, maxAngel - _offset.y);
        }
    }
}