//using Sirenix.OdinInspector;
using UnityEngine;

namespace Generics.Packages.Walking
{

    public class WalkingMove_Lerp : WalkingMoveBehaviour
    {
        [SerializeField] private bool _useRigidbody;
        [/*ShowIf(nameof(_useRigidbody)), */SerializeField] private Rigidbody _rigidbody;

        [SerializeField] protected float _moveLerpSpeed;

        #region Utils

        public override void Move(float fixedDeltaTime)
        {

            if (_useRigidbody)
            {
                var pos = Vector3.Lerp(_rigidbody.position, Walking.Destination, fixedDeltaTime * _moveLerpSpeed);
                _rigidbody.MovePosition(pos);
            }
            else
            {
                var pos = Vector3.Lerp(transform.position, Walking.Destination, fixedDeltaTime * _moveLerpSpeed);
                transform.position = pos;
            }
        }

        #endregion

        #region Unity Editor

#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_useRigidbody) _rigidbody ??= GetComponent<Rigidbody>();
        }

#endif

        #endregion

    }

}