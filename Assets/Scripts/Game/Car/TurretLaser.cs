using UnityEngine;

namespace Game.Car
{
    public class TurretLaser : MonoBehaviour
    {
        [SerializeField] private float laserDistance = 15f;
        [Space]
        [SerializeField] private LayerMask laserMask;
        [SerializeField] private LineRenderer lineRenderer;

        private RaycastHit[] _raycastHits = new RaycastHit[1];

        private void Update() => CastLaser();

        private void CastLaser()
        {
            var hit = Physics.RaycastNonAlloc(Vector3.zero, -transform.up, _raycastHits,laserDistance, laserMask);

            if(hit > 0)
                DrawLaser(Vector3.zero, -transform.forward * _raycastHits[0].point.z);
            else
                DrawLaser(Vector3.zero, -transform.forward * laserDistance);
        }

        private void DrawLaser(Vector3 startPoint, Vector3 endPoint)
        {
            lineRenderer.SetPosition(0, startPoint); 
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}