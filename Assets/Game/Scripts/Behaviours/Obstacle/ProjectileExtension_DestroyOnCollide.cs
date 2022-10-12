using Essentials.Extensions;
using UnityEngine;

namespace Game.Behaviours
{

    public class ProjectileExtension_DestroyOnCollide : MonoBehaviour
    {

        #region Unity Methods

        private void OnCollisionEnter(Collision collision)
        {
            this.DoAfterFixedUpdate(() => Destroy(gameObject));
        }

        #endregion

    }

}