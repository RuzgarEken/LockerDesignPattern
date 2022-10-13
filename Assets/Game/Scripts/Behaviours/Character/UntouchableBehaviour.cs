using Essentials.Extensions;
using Generics.Behaviours;
using UnityEngine;
using DG.Tweening;

namespace Game.Behaviours
{

    public class UntouchableBehaviour : ComponentBase
    {
        [SerializeField] private ComponentBase[] _restrictionBehaviours;
        [SerializeField] private StaminaBehaviour _staminaBehaviour;
        [SerializeField] private Renderer _characterRenderer;
        [SerializeField] private Material _fadeMaterial;
        [SerializeField] private Material _defaultMaterial;

        [Header("Parameters")]
        [SerializeField] private float _duration = 2f;
        [SerializeField] private float _fadeInterval = 0.3f;

        private Tween _fadeTween;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _staminaBehaviour.SetStamina(_staminaBehaviour.MaxStamina);
            _staminaBehaviour.SetEnable(false, "Untouchable");
            foreach (var restrictor in _restrictionBehaviours)
            {
                restrictor.SetEnable(false, "Untouchable");
            }

            _characterRenderer.material = _fadeMaterial;
            _fadeMaterial.SetColor("_Color", _fadeMaterial.color.Alpha(1f));
            _fadeTween =
                _fadeMaterial
                    .DOFade(0.5f, _fadeInterval)
                    .SetLoops(-1, LoopType.Yoyo);

            this.DoAfterTime(_duration, () => SetEnable(false));
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _fadeTween.Kill();

            _characterRenderer.material = _defaultMaterial;

            _staminaBehaviour.SetEnable(true, "Untouchable");
            foreach (var restrictor in _restrictionBehaviours)
            {
                restrictor.SetEnable(true, "Untouchable");
                restrictor.SetEnable(false); //disable default state
            }
        }

        #endregion

        #region Utils

        public void ClearRestrictions()
        {
            SetEnable(true);
        }

        #endregion

    }

}