using Generics.Behaviours;
using UnityEngine;

namespace Game.Behaviours
{

    public class StunnerBehaviour : ComponentBase
    {
        [SerializeField] private float _stunDuration;

        private void OnTriggerEnter(Collider other)
        {
            var avatar = other.attachedRigidbody.GetComponent<GameAvatarEntity>();
            if (!avatar) return;

            avatar.StunBehaviour.Stun(_stunDuration);
        }

    }

}