using Generics.Behaviours;
using UnityEngine;

namespace Generics.Core
{

    public class OppositeComponentBehaviour : ComponentExtension<ComponentBase>
    {
        [SerializeField] private ComponentBase _opposite;
        [SerializeField] private string _reason;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _opposite.SetEnable(false, _reason);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _opposite.SetEnable(true, _reason);
        }

        #endregion

    }

}

