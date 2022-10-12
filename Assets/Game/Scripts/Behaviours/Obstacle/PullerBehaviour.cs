using Generics.Behaviours;
using UnityEngine;

namespace Game.Behaviours
{

    public class PullerBehaviour : ComponentBase
    {
        [SerializeField] private float _pullDuration;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.attachedRigidbody) return;

            var avatar = other.attachedRigidbody.GetComponent<GameAvatarEntity>();
            if (!avatar) return;

            avatar.PullBehaviour.Pull(transform, _pullDuration);
        }

    }

}