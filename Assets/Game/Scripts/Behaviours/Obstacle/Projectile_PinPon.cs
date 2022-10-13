using DG.Tweening;
using UnityEngine;

namespace Game.Behaviours
{

    public class Projectile_PinPon : ProjectileBehaviour
    {
        [SerializeField] private float _range;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _shootEase;

        [Header("Pull")]
        [SerializeField] private Ease _pullEase;

        private Tween _tween;
        private Vector3 _startingPos;
        private bool _pulling;

        #region Initialization

        public override void Set(Vector3 direction)
        {
            _startingPos = transform.position;

            _pulling = false;

            _tween = 
                transform
                    .DOMove(direction * _range, _duration)
                    .SetEase(_pullEase)
                    .SetRelative()
                    .SetLoops(2, LoopType.Yoyo);
        }

        #endregion

        #region Unity Methods

        private void OnTriggerEnter(Collider other)
        {
            if (_pulling) return;

            if (_tween != null)
            {
                _tween.Kill();
            }

            _pulling = true;

            var distance = Vector3.Distance(_startingPos, transform.position);
            var duration = _duration * distance / _range;

            transform
                .DOMove(_startingPos, duration)
                .SetEase(_pullEase);
        }

        #endregion

    }

}