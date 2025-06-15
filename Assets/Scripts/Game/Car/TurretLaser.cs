using UnityEngine;

namespace Game.Car
{
    // To do (Doesn't work correctly)
    public class TurretLaser : MonoBehaviour
    {
        [SerializeField] private float laserDistance = 15f;
        [Space]
        [SerializeField] private LayerMask laserMask;
        [SerializeField] private LineRenderer lineRenderer;

        private readonly RaycastHit[] _raycastHits = new RaycastHit[1];

        private void Update() => CastLaser();

        private void CastLaser()
        {
            /*var hit = Physics.RaycastNonAlloc(transform.position, -transform.up, _raycastHits,laserDistance, laserMask);

            if(hit > 0)
            {
                DrawLaser(Vector3.zero, -transform.forward * _raycastHits[0].point.z);
                Debug.LogWarning($"_raycastHits[0] {_raycastHits[0].collider.name}");
            }
            else*/
            DrawLaser(Vector3.zero, -transform.forward * laserDistance);
        }

        private void DrawLaser(Vector3 startPoint, Vector3 endPoint)
        {
            lineRenderer.SetPosition(0, startPoint); 
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}