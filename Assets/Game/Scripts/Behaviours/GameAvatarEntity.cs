using Generics.Behaviours;
using Generics.Packages.Walking;
using UnityEngine;

namespace Game.Behaviours
{

    public class GameAvatarEntity : Entity
    {
        [SerializeField] private WalkingBehaviour _walkingBehaviour;
        [SerializeField] private WalkingInput_Joystick _inputBehaviour;
        [SerializeField] private WalkingMove_NavMeshAgent _agentMovement;
        [SerializeField] private StaminaBehaviour _staminaBehaviour;
        [SerializeField] private PushBehaviour _pushBehaviour;
        [SerializeField] private StunBehaviour _stunBehaviour;
        [SerializeField] private PullBehaviour _pullBehaviour;

        public WalkingBehaviour WalkingBehaviour => _walkingBehaviour;
        public WalkingInput_Joystick InputBehaviour => _inputBehaviour;
        public WalkingMove_NavMeshAgent AgentMoveBehaviour => _agentMovement;
        public StaminaBehaviour StaminaBehaviour => _staminaBehaviour;
        public PushBehaviour PushBehaviour => _pushBehaviour;
        public StunBehaviour StunBehaviour => _stunBehaviour;
        public PullBehaviour PullBehaviour => _pullBehaviour;

    }

}