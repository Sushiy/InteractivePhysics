using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    Box b;
    Ray r;

    Vector3 hitpoint;
	// Use this for initialization
	void Awake()
    {
        b = new Box(Vector3.zero, new Vector3(3, 3, 3), 1);
        r = new Ray(new Vector3(-3, -2, -3), new Vector3(1, 0, 1));
	}
	
	// Update is called once per frame
	void Update ()
    {
	}


    private void OnDrawGizmos()
    {
        DrawBox(b, true);
        DrawRay(r, false);

        if (RayAABBIntersection(r, b, out hitpoint))
            Gizmos.DrawWireSphere(hitpoint, 0.25f);
    }

    void DrawBox(Box b, bool drawAABB)
    {
        if (b == null) return;
        Gizmos.color = Color.grey;
        //DrawCube
        Gizmos.DrawCube(b.Center, b.Size);
        if(drawAABB)
        {
            Gizmos.color = Color.green;
            //Draw AABB
            Gizmos.DrawWireCube(b.Center, b.AABBSize);
        }
        Gizmos.color = Color.white;
    }

    void DrawRay(Ray r, bool drawPoints)
    {
        if (r == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(r.Origin, r.Direction * 100.0f);
        if(drawPoints)
        {
            Gizmos.DrawWireSphere(r.Origin + r.Direction * 2.0f, 0.25f);
            Gizmos.DrawWireCube(r.Origin + r.Direction * 3.0f, new Vector3(0.5f, 0.5f, 0.5f));
        }
        Gizmos.color = Color.white;
    }

    bool RayAABBIntersection(Ray _ray, Box _box, out Vector3 _hitpoint)
    {
        _hitpoint = Vector3.zero;
        if (_ray == null || _box == null) return false;

        Vector3 minP = _box.Center - b.AABBSize * 0.5f;
        Vector3 maxP = _box.Center + b.AABBSize * 0.5f;

        float tMin = (minP.x - _ray.Origin.x) / _ray.Direction.x;
        float tMax = (maxP.x - _ray.Origin.x) / _ray.Direction.x;

        if (tMax < tMin) Swap(ref tMin, ref tMax);

        float tyMin = (minP.y - _ray.Origin.y) / _ray.Direction.y;
        float tyMax = (maxP.y - _ray.Origin.y) / _ray.Direction.y;

        if (tyMax < tyMin) Swap(ref tyMin, ref tyMax);

        if(tMin > tyMax || tyMin > tMax)
            return false;

        if (tyMin > tMin)
            tMin = tyMin;

        if (tyMax < tMax)
            tMax = tyMax;

        float tzMin = (minP.z - _ray.Origin.z) / _ray.Direction.z;
        float tzMax = (maxP.z - _ray.Origin.z) / _ray.Direction.z;

        if (tzMax < tzMin) Swap(ref tzMin, ref tzMax);

        if (tMin > tzMax || tzMin > tMax)
            return false;

        if (tzMin > tMin)
            tMin = tzMin;

        if (tzMax < tMax)
            tMax = tzMax;

        _hitpoint = _ray.Origin + tMin * _ray.Direction;

        return true;
    }

    void Swap(ref float a, ref float b)
    {
        float tmp = a;
        a = b;
        b = tmp;
    }

}
