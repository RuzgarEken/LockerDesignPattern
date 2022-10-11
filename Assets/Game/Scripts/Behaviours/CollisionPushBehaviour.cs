using UnityEngine;

namespace Game.Behaviours
{

    public class CollisionPushBehaviour : MonoBehaviour
    {
        [SerializeField] private float _pushForce;

        #region Unity Methods

        private void OnCollisionEnter(Collision collision)
        {
            var avatar = collision.rigidbody.GetComponent<GameAvatarEntity>();
            if (!avatar) return;

            Debug.Log("Collision");

            var contact = collision.GetContact(0);
            var pushVector = Vector3.ProjectOnPlane(-contact.normal, Vector3.up) * _pushForce;

            avatar.PushBehaviour.Push(pushVector);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider collider)
        {
            var avatar = collider.attachedRigidbody.GetComponent<GameAvatarEntity>();
            if (!avatar) return;

            var vector =
                Vector3.ProjectOnPlane(
                    (avatar.transform.position - transform.position).normalized,
                    Vector3.up
                );

            var pushVector = vector * _pushForce;

            avatar.PushBehaviour.Push(pushVector);
            Destroy(gameObject);
        }

        #endregion

    }

}