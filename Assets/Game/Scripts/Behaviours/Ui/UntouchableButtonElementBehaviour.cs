using Generics.Behaviours;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Behaviours
{

    public class UntouchableButtonElementBehaviour : ComponentBase
    {
        [SerializeField] private UntouchableBehaviour _untouchableBehaviour;
        [SerializeField] private Button _button;
        [SerializeField] private Image _fillImage;
        [SerializeField] private float _cooldown;

        public void OnButtonClick()
        {
            _button.enabled = false;

            _untouchableBehaviour.ClearRestrictions();

            StartCoroutine(CooldownRoutine());
        }

        private IEnumerator CooldownRoutine()
        {
            var timer = 0f;

            while (true)
            {
                yield return null;

                timer += Time.deltaTime;
                _fillImage.fillAmount = 1 - timer / _cooldown;

                if (timer >= _cooldown)
                {
                    break;
                }
            }

            _button.enabled = true;
        }

    }

}