using Essentials.Extensions;
using Generics.Behaviours;
using Generics.Packages.Walking;
using UnityEngine;

namespace Game.Behaviours
{

    public class PushBehaviour : ComponentBase
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private WalkingBehaviour _walkingBehaviour;

        [Header("Parameters")]
        [SerializeField] private ForceMode _forceMode;
        [SerializeField] private float _pushDecayThreshold;
        [SerializeField] private float _pushDecayTime = 2f;

        private Coroutine _pushDecayTracker;
        private Vector3 _forceVec;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            Debug.Log("Push Enabled");

            _walkingBehaviour.SetEnable(false, "Push");
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;

            PushInternal();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _walkingBehaviour.SetEnable(true, "Push");

            StopCoroutine(_pushDecayTracker);
            _pushDecayTracker = null;
        }

        #endregion

        #region Utils

        public void Push(Vector3 forceVec)
        {
            _forceVec = forceVec;

            if (!enabled)
            {
                SetEnable(true);
            }
            else
            {
                PushInternal();
            }
        }

        #endregion

        #region Helpers

        private void PushInternal()
        {
            Debug.Log("Push Internal");

            _rigidbody.AddForce(_forceVec, _forceMode);

            _pushDecayTracker ??=
                this.DoAfterTime(_pushDecayTime, () =>
                {
                    _pushDecayTracker =
                        this.DoAfterCondition(
                            () => _rigidbody.velocity.magnitude < _pushDecayThreshold,
                            () =>
                            {
                                Debug.Log("Push Decayed");
                                SetEnable(false);
                            }
                        );
                });
        }

        #endregion

    }

}