using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Essentials.Utilities;

namespace Generics.Controllers
{

    public class LegacyInputController : SingletonBehaviour<LegacyInputController>
        ,IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IDragHandler
    {
        public static event Action<Vector2> Clicked; 
        public static event Action<Vector2> PointerDown; 
        public static event Action<Vector2> PointerUp; 
        public static event Action<Vector2> Holding;
        /// <summary> Position, Delta </summary>
        public static event Action<Vector2, Vector2> Dragging;

        private bool _holding = false;

        #region Unity Methods

        private void OnDisable()
        {
            _holding = false;    
        }

        private void Update()
        {
            if(_holding)
            {
                Holding?.Invoke(Input.mousePosition);
            }

        }

        #endregion

        #region Input Listeners

        public void OnDrag(PointerEventData eventData)
        {
            Dragging?.Invoke(eventData.position, eventData.delta);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(eventData.position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PointerDown?.Invoke(eventData.position);

            _holding = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUp?.Invoke(eventData.position);

            _holding = false;
        }

        #endregion

    }

}