using Essentials.Extensions;
using Generics.Behaviours;
using System;
using UnityEngine;

namespace Generics.Packages.Walking
{

    public class WalkingInput_FollowTarget : ComponentBase
    {
        [SerializeField] private WalkingBehaviour _walkingBehaviour;
        [SerializeField] private ComponentBase _followTarget;
        [SerializeField] private float _threshold = 0.1f;
        [SerializeField] private int _frameSkip;

        private int _frameCounter;
        private Transform _followTargetTransform;
        private Action _extensionDisposer;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _frameCounter = UnityEngine.Random.Range(0, _frameSkip);

            SetTarget(_followTarget);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _walkingBehaviour.SetEnable(true, "FollowTarget");
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _walkingBehaviour.SetEnable(false, "FollowTarget");
        }

        private void FixedUpdate()
        {
            _frameCounter += 1;
            if (_frameCounter < _frameSkip) return;

            _frameCounter = 0;

            if (Vector3.Distance(_followTargetTransform.position, transform.position) > _threshold)
            {
                if (!_walkingBehaviour.enabled)
                {
                    _walkingBehaviour.SetEnable(true, "FollowTarget");
                }

                _walkingBehaviour.SetGoal(_followTargetTransform.position);
            }
            else
            {
                _walkingBehaviour.SetEnable(false, "FollowTarget");
            }
        }

        #endregion

        #region Utils

        public void SetTarget(ComponentBase target)
        {
            _extensionDisposer?.Invoke();

            _followTarget = target;
            if (_followTarget == null)
            {
                SetEnable(false, "FollowTarget");
                return;
            }

            _followTargetTransform = target.transform;
            _walkingBehaviour.SetGoal(_followTargetTransform.position);

            SetEnable(true, "FollowTarget");
            _extensionDisposer = target.AddExtension(this);
        }

        public void SetTargetTransform(Transform target)
        {
            _followTargetTransform = target;
            SetEnable(true, "FollowTarget");
            _walkingBehaviour.SetGoal(_followTargetTransform.position);
        }

        #endregion

    }

}