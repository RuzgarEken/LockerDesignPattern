
using UnityEngine;

namespace Generics.Packages.Walking
{

    public class WalkingMove_CharacterController : WalkingMoveBehaviour, ISpeedMovement
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private bool _enableControllerOnStateChange;

        [Header("Parameters")]
        [SerializeField] private float _speed;

        public float MoveSpeed => _speed;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _characterController.enabled = true;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _characterController.enabled = false;
        }

        #endregion

        #region Utils

        public override void Move(float fixedDeltaTime)
        {
            _characterController.Move(BaseComponent.Direction * _speed * fixedDeltaTime);
        }

        public void SetMoveSpeed(float speed)
        {
            _speed = speed;
        }

        #endregion

    }
    
}