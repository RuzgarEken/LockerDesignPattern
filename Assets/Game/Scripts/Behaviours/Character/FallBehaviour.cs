using Generics.Behaviours;
using Generics.Packages.Walking;
using UnityEngine;

namespace Game.Behaviours
{

    public class FallBehaviour : ComponentBase
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private WalkingBehaviour _walkinBehaviour;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _walkinBehaviour.SetEnable(false, "Falling");
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            _walkinBehaviour.SetEnable(true, "Falling");
        }

        #endregion

    }

}