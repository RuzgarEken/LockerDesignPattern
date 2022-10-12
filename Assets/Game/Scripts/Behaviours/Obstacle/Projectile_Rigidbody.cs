using UnityEngine;

namespace Game.Behaviours
{

    public class Projectile_Rigidbody : ProjectileBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [Header("Parameters")]
        [SerializeField] private float _force;

        public override void Set(Vector3 direction)
        {
            _rigidbody.AddForce(direction * _force, ForceMode.VelocityChange);
        }

    }

}