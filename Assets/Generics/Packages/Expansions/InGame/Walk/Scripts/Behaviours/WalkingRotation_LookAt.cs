
using UnityEngine;

namespace Generics.Packages.Walking
{


    public class WalkingRotation_LookAt : WalkingRotationBehaviour
    {
        private Transform _lookAt;

        #region Utils

        public override void Rotate(float fixedDeltaTime)
        {
            if (_lookAt)
            {
                transform.LookAt(_lookAt, Vector3.up);
            }
        }

        public void SetLookAt(Transform lookAt)
        {
            _lookAt = lookAt;
        }

        #endregion

    }

}