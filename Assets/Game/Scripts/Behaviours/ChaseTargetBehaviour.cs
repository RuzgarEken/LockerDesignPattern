using Generics.Behaviours;
using Generics.Packages.Walking;
using UnityEngine;

namespace Game.Behaviours
{

    public class ChaseTargetBehaviour : ComponentBase
    {
        [SerializeField] private TargetingBehaviour _targetingBehaviour;
        [SerializeField] private WalkingInput_FollowTarget _followInput;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _targetingBehaviour.TargetChanged += OnTargetChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _targetingBehaviour.TargetChanged -= OnTargetChanged;
        }

        #endregion

        #region Listeners

        private void OnTargetChanged(ComponentBase target)
        {
            _followInput.SetTarget(target);
        }

        #endregion


    }

}