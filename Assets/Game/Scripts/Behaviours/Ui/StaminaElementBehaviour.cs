using DG.Tweening;
using Generics.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Behaviours
{

    public class StaminaElementBehaviour : ComponentBase
    {
        [SerializeField] private StaminaBehaviour _stamina;
        [SerializeField] private Slider _slider;

        [Header("Parameters")]
        [SerializeField] private float _fillDuration;

        private Tween _tween;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _stamina.StaminaChanged += OnStaminaChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _stamina.StaminaChanged -= OnStaminaChanged;
        }

        #endregion

        #region Listeners

        private void OnStaminaChanged(float stamina)
        {
            var value = stamina / _stamina.MaxStamina;

            if (_tween != null)
            {
                _tween.Kill();
            }

            _tween = 
                _slider
                    .DOValue(value, _fillDuration)
                    .OnComplete(() => _tween = null);
        }

        #endregion

    }

}