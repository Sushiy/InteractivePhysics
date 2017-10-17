using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoMain : MonoBehaviour {

    public MonoBox b;
    public MonoRay r;

    Vector3 hitpoint;
    // Use this for initialization
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnDrawGizmos()
    {
        if (RayAABBIntersection(ref r, b, out hitpoint)) ;
            //Gizmos.DrawWireSphere(hitpoint, 0.25f);
    }

    bool RayAABBIntersection(ref MonoRay _ray, MonoBox _box, out Vector3 _hitpoint)
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

        if (tMin > tyMax || tyMin > tMax)
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

        _ray.tMin = tMin;
        _ray.tMax = tMax;
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
