using UnityEngine;

namespace Generics.Packages.Walking
{

    public class EntityExtension_DestroyOnBecameInvisible : MonoBehaviour
    {
        [SerializeField] private GameObject _target;

        private void OnBecameInvisible()
        {
            if (enabled) Destroy(_target);
        }

    }

}