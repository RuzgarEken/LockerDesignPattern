using Essentials.Extensions;
using Generics.Behaviours;
using UnityEngine;

namespace Game.Behaviours
{

    public class ShootingBehaviour : ComponentBase
    {
        [SerializeField] private TargetingBehaviour _targetingBehaviour;
        [SerializeField] private Rigidbody _projectilePrefab;
        [SerializeField] private Transform _muzzlePoint;

        [Header("Parameters")]
        [SerializeField] private Vector3 _shootDestinationOffset = Vector3.up;
        [SerializeField] private float _shootingInterval;
        [SerializeField] private float _shootingForce;
        [SerializeField] private ForceMode _shootingForceMode;

        private Coroutine _shootingRoutine;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            OnTargetChanged(_targetingBehaviour.Target);
            _targetingBehaviour.TargetChanged += OnTargetChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _targetingBehaviour.TargetChanged -= OnTargetChanged;

            if (_shootingRoutine != null)
            {
                StopCoroutine(_shootingRoutine);
                _shootingRoutine = null;
            }
        }

        #endregion

        #region Listeners

        private void OnTargetChanged(ComponentBase target)
        {
            if (_shootingRoutine != null) StopCoroutine(_shootingRoutine);

            if (target == null)
            {
                return;
            }

            _shootingRoutine = this.DoAfterTime(_shootingInterval, Shoot);
        }

        #endregion

        #region Helpers

        private void Shoot()
        {
            var targetPos = _targetingBehaviour.Target.transform.position + _shootDestinationOffset;
            var dir = (targetPos - _muzzlePoint.position).normalized;

            var projectile = Instantiate(_projectilePrefab, _muzzlePoint.position, Quaternion.identity);
            projectile.AddForce(dir * _shootingForce, _shootingForceMode);

            _shootingRoutine = this.DoAfterTime(_shootingInterval, Shoot);
        }

        #endregion

    }

}