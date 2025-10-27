using System;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private Transform[] controlPoints;
    [Range(0.01f, 1f)]
    [SerializeField] private float steps = 0.01f;
    [Range(0.1f, 1f)]
    [SerializeField] private float gizmosRadius = 0.25f;

    private Vector3 gizmosPosition;
    private void OnDrawGizmos()
    {
        if (controlPoints.Length < 4 || steps <= 0)
        {
            return;
        }

        for (float t = 0; t <= 1; t += steps)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;
            Gizmos.DrawSphere(gizmosPosition, gizmosRadius);
        }

        Gizmos.DrawLine(controlPoints[0].position, controlPoints[1].position);
        Gizmos.DrawLine(controlPoints[2].position, controlPoints[3].position);
    }
}
