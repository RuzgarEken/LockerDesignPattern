//using Sirenix.OdinInspector;
using UnityEngine;

namespace Generics.Packages.Walking
{

    public class WalkingMove_Teleport : WalkingMoveBehaviour
    {
        [SerializeField] private bool _useRigidbody;
        [/*ShowIf(nameof(_useRigidbody)), */SerializeField] private Rigidbody _rigidbody;

        #region Utils

        public override void Move(float fixedDeltaTime)
        {
            //Debug.Log($"Rotate: {targetRotation.eulerAngles}");
            if (_useRigidbody)
            {
                _rigidbody.MovePosition(Walking.Destination);
                
            }
            else
            {
                transform.position = Walking.Destination;
            }
        }

        #endregion

    }

}