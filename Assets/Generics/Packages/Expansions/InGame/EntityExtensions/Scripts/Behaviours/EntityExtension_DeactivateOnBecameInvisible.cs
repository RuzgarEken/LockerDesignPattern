using UnityEngine;

namespace Generics.Packages.Walking
{

    public class EntityExtension_DeactivateOnBecameInvisible : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        private void OnEnable()
        {
            //why not
        }

        private void OnBecameInvisible()
        {
            if (enabled) _target.SetActive(false);
        }

    }

}