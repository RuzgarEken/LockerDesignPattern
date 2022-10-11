using Essentials.Extensions;
//using Generics.Packages.Animation;
using Generics.Utility.Lock;
//using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Generics.Behaviours
{

    public class Entity : ComponentBase
    {
        public event Action<Entity, bool> ActiveStateChanging;
        public event Action<Entity, bool> ActiveStateChanged;

        //[SerializeField, BoxGroup("Entity")] protected AnimatorBehaviour _animator;

        //[SerializeField, BoxGroup("Entity")] private AnimationKey _activationAnimationKey = new AnimationKey() { Key = "Activation" };
        //[SerializeField, BoxGroup("Entity")] private AnimationKey _deactivationAnimationKey = new AnimationKey() { Key = "Deactivation" };
        [SerializeField] private ComponentBase[] _extensions;

        private Locker _activeLocker = new Locker();

        //public AnimatorBehaviour Animator => _animator;
        public bool ActiveSelf => gameObject.activeSelf;
        public bool IsActiveInHierarchy => gameObject.activeInHierarchy;
        public bool IsActive { get; private set; }

        private bool _animate = true;

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            foreach (var extension in _extensions)
            {
                this.AddExtension(extension);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            ActiveStateChanging?.Invoke(this, true);

            //if (_animate && _animator != null && _activationAnimationKey.HasAnimation)
            //{
            //    _animator.Animate(_activationAnimationKey, () =>
            //    {
            //        IsActive = true;
            //        ActiveStateChanged?.Invoke(this, true);
            //    });
            //}
            //else
            //{
                IsActive = true;
                ActiveStateChanged?.Invoke(this, true);
            //}
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            IsActive = false;
            ActiveStateChanged?.Invoke(this, false);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _activeLocker.LockStatusChanged -= OnActiveLockStatusChanged;
        }

        #endregion

        #region Utils

        //[Button, BoxGroup("Entity")]
        public virtual void SetActive(bool value, string key = "default", bool animate = true)
        {
            if (!IsInitialized) Initialize();

            _animate = animate;

            if (value)
            {
                if (ActiveSelf)
                {
                    return;
                }
                else if (!_activeLocker.IsLocked)
                {
                    gameObject.SetActive(true);
                    return;
                }
            }

            _activeLocker.Lock(!value, key);
        }

        public void DoAfterActiveStateChanged(Action action)
        {
            Action<Entity, bool> listener = null;
            ActiveStateChanged += listener = (entity, active) =>
            {
                ActiveStateChanged -= listener;
                action?.Invoke();
            };
        }

        #endregion

        #region Listeners

        protected virtual void OnActiveLockStatusChanged(bool locked)
        {
            InternalActive(!locked);
        }

        #endregion

        #region Helpers

        protected override void Initialize()
        {
            _activeLocker.LockStatusChanged += OnActiveLockStatusChanged;

            base.Initialize();
        }

        protected virtual void InternalActive(bool activate)
        {
            if (activate)
            {
                gameObject.SetActive(true);
            }
            else
            {
                Deactivate();
            }
        }

        private void Deactivate()
        {
            ActiveStateChanging?.Invoke(this, false);

            //if (gameObject.activeInHierarchy && _animate && _animator != null && _deactivationAnimationKey.HasAnimation)
            //{
            //    _animator.Animate(_deactivationAnimationKey, OnDeactivationAnimationEnd);
            //}
            //else
                OnDeactivationAnimationEnd();

            void OnDeactivationAnimationEnd()
            {
                gameObject.SetActive(false);
            }
        }

        #endregion

        #region Editor

#if UNITY_EDITOR

        protected override void OnValidate()
        {
            base.OnValidate();

            //if (!_animator) _animator = GetComponent<AnimatorBehaviour>();
        }

#endif

        #endregion

    }

}