using Generics.Behaviours;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Behaviours
{

    public class RestrictionDisplayElementBehaviour : ComponentBase
    {
        [SerializeField] private RectTransform _container;
        [SerializeField] private Text _restrictionDisplayerTextPrefab;

        private Dictionary<string, Text> _activeRestrictors = new Dictionary<string, Text>();

        #region Utils

        public void SetState(string restirictionType, bool add)
        {
            if (add)
            {
                if (!_activeRestrictors.TryGetValue(restirictionType, out var text))
                {
                    text = Instantiate(_restrictionDisplayerTextPrefab, _container);
                    text.text = restirictionType;

                    _activeRestrictors.Add(restirictionType, text);
                }
            }
            else
            {
                if (_activeRestrictors.TryGetValue(restirictionType, out var text))
                {
                    Destroy(text.gameObject);
                    _activeRestrictors.Remove(restirictionType);
                }
            }
        }

        #endregion

    }

}