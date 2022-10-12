using UnityEngine;

namespace Game.Behaviours
{

    public class SpawnPosToTransformLineDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        private Vector3 _spawnPos;
        private Vector3[] _positions = new Vector3[2];

        #region Unity Methods

        private void OnEnable()
        {
            _spawnPos = transform.position;
            _positions[0] = _spawnPos;
            _positions[1] = _spawnPos;

            _lineRenderer.enabled = true;
        }

        private void OnDisable()
        {
            _lineRenderer.enabled = false;
        }

        private void FixedUpdate()
        {
            _positions[1] = transform.position;

            _lineRenderer.SetPositions(_positions);
        }

        #endregion

    }

}