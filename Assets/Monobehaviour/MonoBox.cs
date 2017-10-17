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
    public Vector3[] Corners { get { return corners; } }

    Vector3[] aabbCorners;
    public Vector3[] AABBCorners { get { return aabbCorners; } }

    public Vector3 AABBSize { get { return new Vector3(m_v3Size.x + 2.0f * aabb, m_v3Size.y + 2.0f * aabb, m_v3Size.z + 2.0f * aabb); } }
    public float AABBRadius { get { return aabb; } }

    private void Awake()
    {
        corners = new Vector3[] {
            Center + new Vector3(Size.x / 2, Size.y / 2, Size.z / 2),
            Center + new Vector3(Size.x / 2, Size.y / 2, -Size.z / 2),
            Center + new Vector3(-Size.x / 2, Size.y / 2, Size.z / 2),
            Center + new Vector3(-Size.x / 2, Size.y / 2, -Size.z / 2),

            Center + new Vector3(Size.x / 2, -Size.y / 2, Size.z / 2),
            Center + new Vector3(Size.x / 2, -Size.y / 2, -Size.z / 2),
            Center + new Vector3(-Size.x / 2, -Size.y / 2, Size.z / 2),
            Center + new Vector3(-Size.x / 2, -Size.y / 2, -Size.z / 2)
        };

        aabbCorners = new Vector3[]
        {
            Center + new Vector3(AABBSize.x, AABBSize.y, AABBSize.z) * 0.5f,
            Center + new Vector3(AABBSize.x, AABBSize.y, -AABBSize.z) * 0.5f,
            Center + new Vector3(-AABBSize.x, AABBSize.y, AABBSize.z) * 0.5f,
            Center + new Vector3(-AABBSize.x, AABBSize.y, -AABBSize.z) * 0.5f,

            Center + new Vector3(AABBSize.x, -AABBSize.y, AABBSize.z) * 0.5f,
            Center + new Vector3(AABBSize.x, -AABBSize.y, -AABBSize.z) * 0.5f,
            Center + new Vector3(-AABBSize.x, -AABBSize.y, AABBSize.z) * 0.5f,
            Center + new Vector3(-AABBSize.x, -AABBSize.y, -AABBSize.z) * 0.5f

        };
    }

    public void OnDrawGizmos()
    {
        if (corners == null) Awake();

        //set Boxcolor
        Gizmos.color = m_color;

        //DrawCube
        Gizmos.DrawCube(Center, Size);
        //Set AABBcolor to green
        Gizmos.color = Color.green;
        //Draw AABB
        Gizmos.DrawWireCube(Center, AABBSize);

        Gizmos.color = Color.blue;
        DebugExtension.DrawCylinder(corners[0], corners[1], Color.blue, 1.0f);
        DebugExtension.DrawCylinder(corners[0], corners[2], Color.blue, 1.0f);
        DebugExtension.DrawCylinder(corners[3], corners[1], Color.blue, 1.0f);
        DebugExtension.DrawCylinder(corners[3], corners[2], Color.blue, 1.0f);

        DebugExtension.DrawCylinder(corners[4], corners[5], Color.blue, 1.0f);
        DebugExtension.DrawCylinder(corners[4], corners[6], Color.blue, 1.0f);
        DebugExtension.DrawCylinder(corners[7], corners[5], Color.blue, 1.0f);
        DebugExtension.DrawCylinder(corners[7], corners[6], Color.blue, 1.0f);

        DebugExtension.DrawCylinder(corners[0], corners[4], Color.blue, 1.0f);
        DebugExtension.DrawCylinder(corners[1], corners[5], Color.blue, 1.0f);
        DebugExtension.DrawCylinder(corners[2], corners[6], Color.blue, 1.0f);
        DebugExtension.DrawCylinder(corners[3], corners[7], Color.blue, 1.0f);

        //Draw All Corners
        foreach (Vector3 v3 in corners)
            Gizmos.DrawWireSphere(v3, AABBRadius);


        //Reset Color to standardvalue
        Gizmos.color = Color.white;
    }
}
