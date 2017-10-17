using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box
{
    Vector3 center;

    public Vector3 Center
    {
        get { return center; }
        set { center = value; }
    }
    Vector3 size;
    public Vector3 Size
    {
        get { return size; }
    }

    float aabb; public Vector3 AABBSize { get { return new Vector3(size.x + 2 * aabb, size.y + 2 * aabb, size.z + aabb); } }

    public Box(Vector3 _center, Vector3 _size, float _aabb)
    {
        center = _center;
        size = _size;
        aabb = _aabb;
    }
}
