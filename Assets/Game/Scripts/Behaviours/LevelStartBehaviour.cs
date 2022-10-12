using Essentials.Extensions;
using Generics.Behaviours;
using Generics.Packages.Walking;
using UnityEngine;

namespace Game.Behaviours
{

    public class LevelStartBehaviour : ComponentBase
    {
        [SerializeField] private GameAvatarEntity _avatar;
        [SerializeField] private Animator _doorAnimator;
        [SerializeField] private Transform _doorEntryPoint;
        [SerializeField] private Transform _insidePoint;

        [Header("Parameters")]
        [SerializeField] private float _entryDelay;

        private WalkingMoveBehaviour _defaultMoveBehaviour;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            //disable input to animate avatar entry
            _avatar.InputBehaviour.SetEnable(false, "EntryAnimation");
            _avatar.StaminaBehaviour.SetEnable(false, "EntryAnimation");
            
            _defaultMoveBehaviour = _avatar.WalkingBehaviour.MoveBehaviour; //cache default behaviour

            _avatar.WalkingBehaviour.SetMoveBehaviour(_avatar.AgentMoveBehaviour); //change movementBehaviour to make movement with navMeshAgent
            _avatar.WalkingBehaviour.SetEnable(true);
            _avatar.WalkingBehaviour.SetGoal(_doorEntryPoint.position, OnAvatarArriveToEntryPoint);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Debug.Log("Disable Movement");

            _avatar.WalkingBehaviour.SetMoveBehaviour(_defaultMoveBehaviour);
            _avatar.WalkingBehaviour.SetEnable(false); //movement completed disable until next movement input
            _avatar.InputBehaviour.SetEnable(true, "EntryAnimation");
            _avatar.StaminaBehaviour.SetEnable(true, "EntryAnimation");
            _doorAnimator.SetBool("Open", false);
        }

        #endregion

        #region Listeners

        private void OnAvatarArriveToEntryPoint()
        {
            Debug.Log("Arrived to Entry Point");

            _doorAnimator.SetBool("Open", true);
            _avatar.WalkingBehaviour.SetEnable(false);

            this.DoAfterTime(_entryDelay, () =>
            {
                Debug.Log("Enter");
                _avatar.WalkingBehaviour.SetEnable(true);
                _avatar.WalkingBehaviour.SetGoal(_insidePoint.position, () => enabled = false);
            });
        }

        #endregion

    }

}