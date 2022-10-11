using Essentials.Extensions;
using Generics.Behaviours;
using Generics.Packages.Walking;
using UnityEngine;
using DG.Tweening;

namespace Game.Behaviours
{

    public class PushBehaviour : ComponentBase
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private WalkingBehaviour _walkingBehaviour;

        [Header("Parameters")]
        [SerializeField] private float _forceSpeed;

        private Vector3 _forceVec;
        private Tween _tween;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _walkingBehaviour.SetEnable(false, "Push");

            this.DoAfterFixedUpdate(PushInternal);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _walkingBehaviour.SetEnable(true, "Push");
        }

        #endregion

        #region Utils

        public void Push(Vector3 forceVec)
        {
            _forceVec = forceVec;

            if (!enabled)
            {
                SetEnable(true);
            }
            else
            {
                PushInternal();
            }
        }

        #endregion

        #region Helpers

        private void PushInternal()
        {
            if (_tween != null)
            {
                _tween.Kill();
            }

            _tween = 
                transform
                    .DOBlendableMoveBy(_forceVec, _forceSpeed)
                    .SetRelative()
                    .SetSpeedBased()
                    .OnComplete(() =>
                    {
                        _tween = null;
                        SetEnable(false);
                    });
        }

        #endregion

    }

}