using System;
using Generics.Behaviours;
using Generics.Controllers;
using UnityEngine;

namespace Generics.Packages.InputSystem
{
    
    public class SwipeListenerBehaviour : ComponentBase
    {
        public event Action<Vector2> Performed;
        
        [SerializeField] private float _swipeLengthThreshold;
        [SerializeField] private float _swipeTimeThreshold;
        [SerializeField] private Vector2 _horizontalClamp;
        [SerializeField] private Vector2 _verticalClamp;
        [SerializeField] private bool _normalizeResult;
        [SerializeField] private bool _allowMultipleSwipes;
        
        private Vector2 _startPoint;
        private float _swipeStartTime;
        private Vector2 _previousDir;
        private static Vector2 _zero = Vector2.zero;
        
        #region Unity Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            LegacyInputController.PointerDown += OnPointerDown;
            LegacyInputController.PointerUp   += OnPointerUp;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            LegacyInputController.PointerDown -= OnPointerDown;
            LegacyInputController.PointerUp   -= OnPointerUp;
        }

        #endregion

        #region Listeners

        private void OnPointerDown(Vector2 point)
        {
            ResetSwipe(point);
            
            LegacyInputController.Dragging += OnDragging;
        }

        private void OnPointerUp(Vector2 point)
        {
            LegacyInputController.Dragging -= OnDragging;
            
        }

        private void OnDragging(Vector2 point, Vector2 delta)
        {
            if (Vector3.Dot(_previousDir, delta) < 0 
             || Time.realtimeSinceStartup - _swipeStartTime > _swipeTimeThreshold)
            {
                ResetSwipe(point);
                return;
            }
            
            var vec = point - _startPoint;
            vec.x = Mathf.Clamp(vec.x, _horizontalClamp[0], _horizontalClamp[1]);
            vec.y = Mathf.Clamp(vec.y, _verticalClamp[0], _verticalClamp[1]);
            
            if (vec.magnitude > _swipeLengthThreshold)
            {
                Performed?.Invoke(_normalizeResult ? vec.normalized : vec);

                if (!_allowMultipleSwipes) LegacyInputController.Dragging -= OnDragging;
            }
        }

        private void ResetSwipe(Vector2 point)
        {
            _startPoint = point;
            _swipeStartTime = Time.realtimeSinceStartup;
            _previousDir = _zero;
        }

        #endregion
    }
    
}