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

        public WalkingBehaviour WalkingBehaviour => _walkingBehaviour;
        public WalkingInput_Joystick InputBehaviour => _inputBehaviour;
        public WalkingMove_NavMeshAgent AgentMoveBehaviour => _agentMovement;

    }

}