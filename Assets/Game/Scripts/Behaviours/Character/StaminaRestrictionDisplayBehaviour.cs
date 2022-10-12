using Generics.Behaviours;
using UnityEngine;

namespace Game.Behaviours
{

    public class StaminaRestrictionDisplayBehaviour : ComponentExtension<StaminaBehaviour>
    {
        [SerializeField] private RestrictionDisplayElementBehaviour _element;
        [SerializeField] private string _restrictionType;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            BaseComponent.CanUseStateChanged += OnStaminaUseStateChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            BaseComponent.CanUseStateChanged -= OnStaminaUseStateChanged;
        }

        #endregion

        #region Listeners

        private void OnStaminaUseStateChanged(bool state)
        {
            _element.SetState(_restrictionType, !state);
        }

        #endregion

    }

}