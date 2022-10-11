using Essentials.Extensions;
using Generics.Behaviours;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Behaviours
{

    public class TargetingBehaviour : ComponentBase
    {
        public event Action<ComponentBase> TargetChanged;

        public ComponentBase Target { get; private set; }

        private List<ComponentBase> _targetsInRange = new List<ComponentBase>();

        #region Unity Methods

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<ComponentBase>();
            if (!target || _targetsInRange.Contains(target))
            {
                return;
            }

            _targetsInRange.Add(target);

            if (Target == null)
            {
                Target = target;
                TargetChanged?.Invoke(target);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var target = other.GetComponent<ComponentBase>();
            if (!target || !_targetsInRange.Contains(target))
            {
                return;
            }

            _targetsInRange.Remove(target);

            if (target == Target)
            {
                Target = 
                    _targetsInRange.Count > 0 ? 
                    _targetsInRange.GetRandomElement() : 
                    null;
            }

            TargetChanged?.Invoke(Target);
        }

        #endregion

    }

}