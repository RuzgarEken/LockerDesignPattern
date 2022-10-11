using Generics.Behaviours;
using UnityEngine;

namespace Generics.Core
{

    public class EntityExtension<T> : ComponentBase
        where T : Entity
    {
        [SerializeField] private T _baseEntity;

        protected T BaseEntity => _baseEntity;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            if (_baseEntity)
            {
                OnBaseEntityActiveStateChanged(_baseEntity, _baseEntity.IsActive);
                _baseEntity.ActiveStateChanged += OnBaseEntityActiveStateChanged;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _baseEntity.ActiveStateChanged -= OnBaseEntityActiveStateChanged;
        }

        #endregion

        #region Listeners

        private void OnBaseEntityActiveStateChanged(Entity entity, bool isActive)
        {
            SetEnable(isActive);
        }

        #endregion

        #region Unity Editor

#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();

            if (!_baseEntity)
            {
                _baseEntity = GetComponent<T>();
            }
        }
#endif

        #endregion

    }

}