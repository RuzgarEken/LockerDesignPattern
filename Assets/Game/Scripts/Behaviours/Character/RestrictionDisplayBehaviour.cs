using Generics.Behaviours;
using UnityEngine;

namespace Game.Behaviours
{

    public class RestrictionDisplayBehaviour : ComponentExtension<ComponentBase>
    {
        [SerializeField] private RestrictionDisplayElementBehaviour _element;
        [SerializeField] private string _restrictionType;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _element.SetState(_restrictionType, true);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _element.SetState(_restrictionType, false);
        }

        #endregion

    }

}