using Essentials.Extensions;
using Generics.Behaviours;
using Generics.Packages.Walking;
using UnityEngine;

namespace Game.Behaviours
{

    public class StunBehaviour : ComponentBase
    {
        [SerializeField] private WalkingBehaviour _walkingBehaviour;

        private float _stunDuration;
        private Coroutine _stunRoutine;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _walkingBehaviour.SetEnable(false, "Stun");

            InternalStun();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _walkingBehaviour.SetEnable(true, "Stun");

            StopCoroutine(_stunRoutine);
            _stunRoutine = null;
        }

        #endregion

        #region Utils

        public void Stun(float duration)
        {
            _stunDuration = duration;

            if (enabled) return;

            SetEnable(true);
        }

        #endregion

        #region Helpers

        private void InternalStun()
        {
            _stunRoutine = this.DoAfterTime(_stunDuration, () => SetEnable(false));
        }

        #endregion

    }

}