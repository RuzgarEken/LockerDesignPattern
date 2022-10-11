using System;
using UnityEngine;
using Essentials.Extensions;
using Generics.Behaviours;

namespace Generics.Packages.Walking
{

    public class WalkingBehaviour : ComponentBase
    {
        public event Action<Vector3> DestinationChanged;
        public event Action ArrivedToDestination;

        [SerializeField] private WalkingMoveBehaviour _moveBehaviour;
        [SerializeField] private WalkingRotationBehaviour _rotationBehaviuor;
        [SerializeField] private bool _trackArrive;

        [Header("Parameters")]
        [SerializeField] private float _arriveThreshold;

        [HideInInspector] public Vector3 Direction;

        protected Vector3 _destination;
        protected Coroutine _arriveTracker;

        public float ArriveThreshold => _arriveThreshold;
        public WalkingMoveBehaviour MoveBehaviour => _moveBehaviour;
        public WalkingRotationBehaviour RotationBehaviour => _rotationBehaviuor;
        public Vector3 Destination => _destination;

        #region Unity Methods

        protected virtual void FixedUpdate()
        {
            var fixedDeltaTime = Time.fixedDeltaTime;

            if (_rotationBehaviuor) _rotationBehaviuor.Rotate(fixedDeltaTime);
            if(_moveBehaviour)      _moveBehaviour.Move(fixedDeltaTime);

#if DEBUG_MODE
            //GizmosHelper.DrawLine(new LineGizmoModel(this.GetInstanceID().ToString(), transform.position, Color.yellow, transform.position + transform.forward, 0.1f));
#endif
        }

        #endregion

        #region Utils
       
        public virtual bool SetGoal(Vector3 destination, Action onArrive = null)
        {
            if (!_moveBehaviour.IsDestinationValid(destination))
            {
                return false;
            }

            _destination = destination;

#if DEBUG_MODE
            GizmosHelper.DrawSphere(new SphereGizmoModel($"Destination_{this.GetInstanceID()}", destination, new Color(1f, 0f, 0f, 0.5f), 0.2f));
#endif
            if (Vector3.Distance(destination, transform.position) <= _arriveThreshold)
            {
                if (onArrive != null || _trackArrive)
                {
                    OnArrive();
                }

                return true;
            }


            Direction = (_destination - transform.position).normalized;

            if (_arriveTracker != null)
            {
                StopCoroutine(_arriveTracker);
                _arriveTracker = null;
            }

            if (onArrive != null || _trackArrive)
            {
                _arriveTracker =
                    this.DoAfterCondition(
                        () => (transform.position - _destination).magnitude <= _arriveThreshold,
                        OnArrive
                    );
            }

            DestinationChanged?.Invoke(_destination);

            return true;

            void OnArrive()
            {
                onArrive?.Invoke();
                ArrivedToDestination?.Invoke();
            }
        }

        #endregion

        #region Helpers

        public void DoAfterArrive(Action action)
        {
            Action listener = null;
            ArrivedToDestination += listener = () =>
            {
                ArrivedToDestination -= listener;
                action?.Invoke();
            };
        }

        public void SetMoveBehaviour(WalkingMoveBehaviour moveBehaviour)
        {
            _moveBehaviour.enabled = false;
            _moveBehaviour = moveBehaviour;
            _moveBehaviour.enabled = true;
        }

        public void SetRotationBehaviour(WalkingRotationBehaviour rotationBehaviour)
        {
            _rotationBehaviuor.enabled = false;
            _rotationBehaviuor = rotationBehaviour;
            _rotationBehaviuor.enabled = true;
        }

        public void SetArriveThreshold(float arriveThreshold)
        {
            _arriveThreshold = arriveThreshold;
        }

        #endregion

        #region Unity Editor

#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();

            _moveBehaviour ??= GetComponent<WalkingMoveBehaviour>();
            _rotationBehaviuor ??= GetComponent<WalkingRotationBehaviour>();
        }

#endif

        #endregion

    }

}