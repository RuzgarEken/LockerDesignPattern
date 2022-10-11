using Essentials.Extensions;
using Generics.Behaviours;
using UnityEngine;

namespace Generics.Packages.EntityExtensions
{

    public class EntityExtension_DestroyAfterTime : ComponentBase
    {
        [SerializeField] private float _delay;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            this.DoAfterTime(_delay, () => Destroy(gameObject));
        }

        #endregion

    }

}