using Generics.Behaviours;
using UnityEngine;

namespace Generics.Packages.EntityExtensions
{

    public class EntityExtension_DeactivateParticle : ComponentExtension<Entity>
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private Transform _particlePoint;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            BaseComponent.ActiveStateChanged += OnActiveStateChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            BaseComponent.ActiveStateChanged -= OnActiveStateChanged;
        }

        #endregion

        #region Listeners

        protected virtual void OnActiveStateChanged(Entity _, bool active)
        {
            if (active) return;

            var particle = Instantiate(_particle);
            particle.transform.position = BaseComponent.transform.position;
        }

        #endregion

    }

}