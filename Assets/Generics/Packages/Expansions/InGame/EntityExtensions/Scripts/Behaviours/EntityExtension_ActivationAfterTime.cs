using Essentials.Extensions;
using Generics.Behaviours;
using UnityEngine;

namespace Generics.Packages.EntityExtensions
{

    public class EntityExtension_ActivationAfterTime : ComponentBase
    {
        [SerializeField] private float _time;
        [SerializeField] private Entity _target;
        [SerializeField] private bool _activeState;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            this.DoAfterTime(_time, () => _target.SetActive(_activeState));
        }

        #endregion

    }

}