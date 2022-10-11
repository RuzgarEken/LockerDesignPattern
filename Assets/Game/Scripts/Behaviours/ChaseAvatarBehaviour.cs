using Generics.Behaviours;
using Generics.Packages.Walking;
using UnityEngine;

namespace Game.Behaviours
{

    public class ChaseAvatarBehaviour : ComponentBase
    {
        [SerializeField] private WalkingInput_FollowTarget _followInput;

        private Transform _target;

        #region Unity Methods

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<ComponentBase>();
            if (!target) return;

            _target = target.transform;
            _followInput.SetTarget(target);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform != _target) return;

            _target = null;
            _followInput.SetTarget(null);
        }

        #endregion


    }

}