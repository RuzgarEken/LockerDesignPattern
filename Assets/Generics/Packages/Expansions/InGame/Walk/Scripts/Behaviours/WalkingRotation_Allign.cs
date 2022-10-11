using Generics.Packages.Walking;
using UnityEngine;

namespace Generics.Packages.Walking
{

    public class WalkingRotation_Allign : WalkingRotationBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _rotationOffset;
        [SerializeField] private float _lerpSpeed;

        private Quaternion _offsetQuaternion;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _offsetQuaternion = Quaternion.Euler(_rotationOffset);
        }

        #endregion

        #region Utils

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public override void Rotate(float fixedDeltaTime)
        {
            var targetRotation = _target.rotation * _offsetQuaternion;

            transform.rotation = 
                Quaternion.Lerp(transform.rotation, targetRotation, fixedDeltaTime * _lerpSpeed);
        }

        #endregion
            
    }

}