using Generics.Behaviours;
using UnityEngine;

namespace Game.Behaviours
{

    public class FallDieBehaviour : ComponentExtension<FallBehaviour>
    {
        [SerializeField] private GameAvatarEntity _avatar;
        [SerializeField] private float _dieHeight;

        private void FixedUpdate()
        {
            if (transform.position.y < _dieHeight)
            {
                _avatar.SetEnable(false, "Dead");
            }
        }

    }

}