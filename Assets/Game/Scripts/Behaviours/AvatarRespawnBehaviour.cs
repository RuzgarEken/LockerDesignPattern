using Generics.Behaviours;
using UnityEngine;

namespace Game.Behaviours
{

    public class AvatarRespawnBehaviour : ComponentBase
    {
        [SerializeField] private GameAvatarEntity _avatar;
        [SerializeField] private Transform _respawnPoint;

        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _avatar.EnableStateChanged += OnAvatarEnableStateChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _avatar.EnableStateChanged -= OnAvatarEnableStateChanged;
        }

        #endregion

        #region Listeners

        private void OnAvatarEnableStateChanged(ComponentBase source, bool state)
        {
            if (state) return;

            _avatar.transform.position = _respawnPoint.position;
            _avatar.SetEnable(true, "Dead");
        }

        #endregion

    }

}