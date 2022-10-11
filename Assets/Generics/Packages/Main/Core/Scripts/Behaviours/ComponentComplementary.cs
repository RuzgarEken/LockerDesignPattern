
using UnityEngine;

namespace Generics.Behaviours
{

    public class ComponentComplementary<T> : ComponentBase
        where T : ComponentBase
    {
        [SerializeField] private T _complementaryComponent;

        protected T ComplementaryComponent => _complementaryComponent;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            if (_complementaryComponent)
            {
                OnBaseComponentEnableStateChanged(_complementaryComponent, _complementaryComponent.enabled);
                _complementaryComponent.EnableStateChanged += OnBaseComponentEnableStateChanged;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _complementaryComponent.EnableStateChanged -= OnBaseComponentEnableStateChanged;
        }

        #endregion

        #region Listeners

        private void OnBaseComponentEnableStateChanged(ComponentBase componentBase, bool isEnabled)
        {
            SetEnable(!isEnabled);
        }

        #endregion

        #region Unity Editor

#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();

            if (!_complementaryComponent)
            {
                _complementaryComponent = GetComponent<T>();
            }
        }
#endif

        #endregion

    }

}