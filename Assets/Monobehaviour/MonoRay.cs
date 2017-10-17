using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoRay : MonoBehaviour
{
    Transform m_transOrigin;
    public Vector3 Origin { get { return m_transOrigin.position ; } }

    Transform m_transDirection;
    public Vector3 Direction { get { return (m_transDirection.position - m_transOrigin.position).normalized; } }

    public float tMin, tMax;

    private float tGizmoSize = 0.25f;

    // Use this for initialization
    void Awake ()
    {
        m_transOrigin = transform.GetChild(0);
        m_transDirection = transform.GetChild(1);
    }

    public Vector3 GetPointOnRay(float t)
    {
        return Origin + Direction * t;
    }

    void OnDrawGizmos()
    {
        if (m_transOrigin == null) Awake();
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Origin, Direction * 100.0f);
        Gizmos.DrawWireSphere(Origin + Direction * tMin, tGizmoSize);
        Gizmos.DrawWireCube(Origin + Direction * tMax, new Vector3(tGizmoSize * 2.0f, tGizmoSize * 2.0f, tGizmoSize * 2.0f));
        Gizmos.color = Color.white;
    }
}
