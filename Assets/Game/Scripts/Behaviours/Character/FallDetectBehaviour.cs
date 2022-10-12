using Generics.Behaviours;
using UnityEngine;

namespace Game.Behaviours
{

    public class FallDetectBehaviour : ComponentBase
    {
        [SerializeField] private FallBehaviour _fallBehaviour;
        [SerializeField] private Transform _feetPoint;
        [SerializeField] private LayerMask _groundLayer;

        [Header("Parameters")]
        [SerializeField] private float _rayDistance;

        private void FixedUpdate()
        {
            bool grounded = Physics.Raycast(_feetPoint.position, Vector3.down, _rayDistance, _groundLayer);
            if (grounded && _fallBehaviour.enabled)
            {
                _fallBehaviour.SetEnable(false);
            }
            else if (!grounded && !_fallBehaviour.enabled)
            {
                _fallBehaviour.SetEnable(true);
            }

        }

    }

}