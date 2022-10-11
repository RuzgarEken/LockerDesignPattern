//using Sirenix.OdinInspector;
using UnityEngine;

namespace Generics.Packages.Walking
{

    public interface ISpeedMovement
    {
        float MoveSpeed { get; }

        void SetMoveSpeed(float speed);
    }

    public class WalkingMove_Speed : WalkingMoveBehaviour, ISpeedMovement
    {
        [SerializeField] private bool _useRigidbody;
        [/*ShowIf(nameof(_useRigidbody)), */SerializeField]private Rigidbody _rigidbody;

        [Header("Parameters")]
        [SerializeField] protected float _moveSpeed;

        public float MoveSpeed => _moveSpeed;

        private Vector3 _targetPos;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _targetPos = transform.position;
        }

        #endregion

        #region Helpers

        public override void Move(float fixedDeltaTime)
        {
            _targetPos += Walking.Direction * _moveSpeed * fixedDeltaTime;

            if (_useRigidbody)
            {
                _rigidbody.MovePosition(_targetPos);

            }
            else
            {
                transform.position = _targetPos;
            }

            Walking.Direction = (Walking.Destination - transform.position).normalized;
        }

        #endregion

        #region Utils

        public virtual void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;

        #endregion

    }

}