using Essentials.Extensions;
using Generics.Behaviours;
using Generics.Packages.Walking;
using UnityEngine;

namespace Generics.Packages.Walking
{

    public class WalkingExtension_RandSpeed : ComponentExtension<WalkingBehaviour>
    {
        [SerializeField] private Vector2 _speedRange;

        #region Unity Methods

        protected override void Awake()
        {
            var speedMovement = BaseComponent.MoveBehaviour as ISpeedMovement;
            if (speedMovement != null)
            {
                speedMovement.SetMoveSpeed(_speedRange.Random());
            }
        }

        #endregion

    }

}