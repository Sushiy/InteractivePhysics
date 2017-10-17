using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBox : MonoBehaviour
{
    public Vector3 Center
    {
        get { return transform.position; }
    }

    [SerializeField]
    Vector3 m_v3Size;
    public Vector3 Size { get { return m_v3Size; } }
    [SerializeField]
    Color m_color = Color.white;

    [SerializeField]
    float aabb;

    [SerializeField]
    bool drawMikowski = true;

    Vector3[] corners;

    public Vector3 AABBSize { get { return new Vector3(m_v3Size.x + 2 * aabb, m_v3Size.y + 2 * aabb, m_v3Size.z + aabb); } }

    public void OnDrawGizmos()
    {
        //set Boxcolor
        Gizmos.color = m_color;

        //DrawCube
        Gizmos.DrawCube(Center, Size);
        //Set AABBcolor to green
        Gizmos.color = Color.green;
        //Draw AABB
        Gizmos.DrawWireCube(Center, AABBSize);

        DebugExtension.DrawCylinder(Center + Size/2, Center + new Vector3(-Size.x/2, Size.y/2, Size.z/2), Color.green, 1.0f);

        //Draw All Corners
        Gizmos.DrawWireSphere(Center + Size/2, 1.0f);
        Gizmos.DrawWireSphere(Center - Size/2, 1.0f);
        Gizmos.DrawWireSphere(Center + new Vector3(-Size.x / 2, Size.y / 2, Size.z / 2), 1.0f);
        Gizmos.DrawWireSphere(Center + new Vector3(-Size.x / 2, Size.y / 2, -Size.z / 2), 1.0f);
        Gizmos.DrawWireSphere(Center + new Vector3(-Size.x / 2, -Size.y / 2, Size.z / 2), 1.0f);
        Gizmos.DrawWireSphere(Center + new Vector3(Size.x / 2, -Size.y / 2, Size.z / 2), 1.0f);
        Gizmos.DrawWireSphere(Center + new Vector3(Size.x / 2, -Size.y / 2, -Size.z / 2), 1.0f);
        Gizmos.DrawWireSphere(Center + new Vector3(Size.x / 2, Size.y / 2, -Size.z / 2), 1.0f);


        //Reset Color to standardvalue
        Gizmos.color = Color.white;
    }
}
