using Essentials.Extensions;
using Generics.Behaviours;
using Generics.Packages.Walking;
using UnityEngine;

namespace Game.Behaviours
{

    public class PullBehaviour : ComponentBase
    {
        [SerializeField] private WalkingBehaviour _walkingBehaviour;
        [SerializeField] private float _pullLerpSpeed;

        private float _pullDuration;
        private Transform _puller;
        private Vector3 _offsetToSource;
        private Coroutine _pullRoutine;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _walkingBehaviour.SetEnable(false, "Pull");

            PullInternal();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _walkingBehaviour.SetEnable(true, "Pull");

            if (_pullRoutine != null)
            {
                StopCoroutine(_pullRoutine);
                _pullRoutine = null;
            }
        }

        protected void FixedUpdate()
        {
            if (!_puller)
            {
                SetEnable(false);
                return;
            }

            transform.position = 
                Vector3.Lerp(
                    transform.position, 
                    _puller.position - _offsetToSource, 
                    _pullLerpSpeed * Time.fixedDeltaTime
                );
        }

        #endregion

        #region Utils

        public void Pull(Transform source, float duration)
        {
            _puller = source;
            _pullDuration = duration;

            if (!enabled)
            {
                SetEnable(true);
            }
            else
            {
                PullInternal();
            }
        }

        #endregion

        #region Helpers

        private void PullInternal()
        {
            if (!_puller)
            {
                SetEnable(false);
                return;
            }

            _offsetToSource = (_puller.position - transform.position);

            if (_pullRoutine != null) StopCoroutine(_pullRoutine);

            _pullRoutine = 
                this.DoAfterTime(
                    _pullDuration, 
                    () =>
                    {
                        _pullRoutine = null;
                        SetEnable(false);
                    }
                );
        }

        #endregion

    }

}