using Generics.Behaviours;
using UnityEngine;

namespace Generics.Packages.EntityExtensions
{

    public class EntityExtension_ActivationByComponentState : ComponentExtension<ComponentBase>
    {
        [SerializeField] private Entity _entity;
        [SerializeField] private string _reason = "default";

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _entity.SetActive(true, _reason);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _entity.SetActive(false, _reason);
        }

        #endregion

    }

}