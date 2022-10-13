using Generics.Behaviours;
using Generics.Packages.Walking;
using System;
using UnityEngine;

namespace Game.Behaviours
{

    public class StaminaBehaviour : ComponentBase
    {
        public event Action<float> StaminaChanged;
        public event Action<bool> CanUseStateChanged;

        [SerializeField] private WalkingBehaviour _walkingBehaviour;

        [Header("Parameters")]
        [SerializeField] private float _maxStamina;
        [SerializeField] private float _minStaminaToMove;
        [SerializeField] private float _staminaDecreaseSpeed;
        [SerializeField] private float _staminaIncreaseSpeed;

        private float _stamina;
        private float _deltaStamina;

        public float Stamina => _stamina;
        public float MaxStamina => _maxStamina;

        #region Unity Methods

        private void Start()
        {
            _stamina = _maxStamina;

            StaminaChanged?.Invoke(_stamina);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _walkingBehaviour.EnableStateChanged += OnWalkingStateChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _walkingBehaviour.EnableStateChanged -= OnWalkingStateChanged;

            _walkingBehaviour.SetEnable(true, "Stamina");
        }

        private void FixedUpdate()
        {
            if (_deltaStamina > 0 && _stamina == _maxStamina)
            {
                return;
            }

            bool canMove = _stamina < _minStaminaToMove;

            _stamina = Mathf.Clamp(_stamina + _deltaStamina * Time.fixedDeltaTime, 0f, _maxStamina);

            if (_stamina == 0f)
            {
                _walkingBehaviour.SetEnable(false, "Stamina");

                CanUseStateChanged?.Invoke(false);
            }
            else if (!canMove && _stamina >= _minStaminaToMove)
            {
                _walkingBehaviour.SetEnable(true, "Stamina");
                CanUseStateChanged?.Invoke(true);
            }

            StaminaChanged?.Invoke(_stamina);
        }

        #endregion

        #region Utils

        public void SetStamina(float stamina)
        {
            _stamina = stamina;

            StaminaChanged?.Invoke(_stamina);
        }

        #endregion

        #region Listeners

        private void OnWalkingStateChanged(ComponentBase source, bool state)
        {
            _deltaStamina = state ? -_staminaDecreaseSpeed : _staminaIncreaseSpeed;
        }

        #endregion

    }

}