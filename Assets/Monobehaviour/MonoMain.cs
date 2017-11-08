using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonoMain : MonoBehaviour {

    public MonoBox b;
    public MonoRay r;
    public Text debugText;

    Vector3 hitpoint;
    // Use this for initialization
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            float t;
            RayCylinderIntersection(r, b.Corners[0], b.Corners[4], 1, out t);
        }
    }


    private void OnDrawGizmos()
    {
        if (RayAABBIntersection(ref r, b, out hitpoint))
        {
            Gizmos.color = Color.red;
            bool hitEdgeCapsule;
            if (CheckEdgeCase(r, b, ref hitpoint, out hitEdgeCapsule))
            {
                if(hitEdgeCapsule)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawSphere(hitpoint, 1.0f);
                }
            }
            else
            {
                Gizmos.DrawSphere(hitpoint, 1.0f);
            }
            Gizmos.DrawWireSphere(hitpoint, 1.0f);
            Gizmos.color = Color.white;
        }

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
        _hitpoint = _ray.GetPointOnRay(tMin);

        return true;
    }

    bool CheckEdgeCase(MonoRay _ray, MonoBox _box, ref Vector3 _hitpoint, out bool _hitEdgeCapsule)
    {
        //Check if we are closer than radius to any corner
        float t = 0;
        bool hitEdgeCapsule = false;

        //Upper Edges
        float closestDistance = Mathf.Infinity;
        float distance = DistancePointEdge(_hitpoint, _box.AABBCorners[0], _box.AABBCorners[1]);

        if (distance < closestDistance) closestDistance = distance;
        if (distance <= _box.AABBRadius)
        {
            if(RayCapsuleIntersection(_ray, _box.Corners[0], _box.Corners[1], _box.AABBRadius, out t))
                hitEdgeCapsule = true;
        }
        distance = DistancePointEdge(_hitpoint, _box.AABBCorners[0], _box.AABBCorners[2]);
        if (distance < closestDistance) closestDistance = distance;
        if (!hitEdgeCapsule && distance <= _box.AABBRadius)
        {
            if(RayCapsuleIntersection(_ray, _box.Corners[0], _box.Corners[2], _box.AABBRadius, out t))
                hitEdgeCapsule = true;
        }
        distance = DistancePointEdge(_hitpoint, _box.AABBCorners[3], _box.AABBCorners[1]);
        if (distance < closestDistance) closestDistance = distance;
        if (!hitEdgeCapsule && distance <= _box.AABBRadius)
        {
            if(RayCapsuleIntersection(_ray, _box.Corners[3], _box.Corners[1], _box.AABBRadius, out t))
                hitEdgeCapsule = true;
        }
        distance = DistancePointEdge(_hitpoint, _box.AABBCorners[3], _box.AABBCorners[2]);
        if (distance < closestDistance) closestDistance = distance;
        if (!hitEdgeCapsule && distance <= _box.AABBRadius)
        {
            if(RayCapsuleIntersection(_ray, _box.Corners[3], _box.Corners[2], _box.AABBRadius, out t))
                hitEdgeCapsule = true;
        }

        //Lower Edges
        distance = DistancePointEdge(_hitpoint, _box.AABBCorners[4], _box.AABBCorners[5]);
        if (distance < closestDistance) closestDistance = distance;
        if (!hitEdgeCapsule && distance <= _box.AABBRadius)
        {
            if(RayCapsuleIntersection(_ray, _box.Corners[4], _box.Corners[5], _box.AABBRadius, out t))
                hitEdgeCapsule = true;
        }
        distance = DistancePointEdge(_hitpoint, _box.AABBCorners[4], _box.AABBCorners[6]);
        if (distance < closestDistance) closestDistance = distance;
        if (!hitEdgeCapsule && distance <= _box.AABBRadius)
        {
            if(RayCapsuleIntersection(_ray, _box.Corners[4], _box.Corners[6], _box.AABBRadius, out t))
                hitEdgeCapsule = true;
        }
        distance = DistancePointEdge(_hitpoint, _box.AABBCorners[7], _box.AABBCorners[5]);
        if (distance < closestDistance) closestDistance = distance;
        if (!hitEdgeCapsule && distance <= _box.AABBRadius)
        {
            if(RayCapsuleIntersection(_ray, _box.Corners[7], _box.Corners[5], _box.AABBRadius, out t))
                hitEdgeCapsule = true;
        }
        distance = DistancePointEdge(_hitpoint, _box.AABBCorners[7], _box.AABBCorners[6]);
        if (distance < closestDistance) closestDistance = distance;
        if (!hitEdgeCapsule && distance <= _box.AABBRadius)
        {
            if(RayCapsuleIntersection(_ray, _box.Corners[7], _box.Corners[6], _box.AABBRadius, out t))
                hitEdgeCapsule = true;
        }

        //Standing Edges
        distance = DistancePointEdge(_hitpoint, _box.AABBCorners[0], _box.AABBCorners[4]);
        if (distance < closestDistance) closestDistance = distance;
        if (!hitEdgeCapsule && distance <= _box.AABBRadius)
        {
            if (RayCapsuleIntersection(_ray, _box.Corners[0], _box.Corners[4], _box.AABBRadius, out t))
                hitEdgeCapsule = true;
        }
        distance = DistancePointEdge(_hitpoint, _box.AABBCorners[1], _box.AABBCorners[5]);
        if (distance < closestDistance) closestDistance = distance;
        if (!hitEdgeCapsule && distance <= _box.AABBRadius)
        {
            if (RayCapsuleIntersection(_ray, _box.Corners[1], _box.Corners[5], _box.AABBRadius, out t))
                hitEdgeCapsule = true;
        }
        distance = DistancePointEdge(_hitpoint, _box.AABBCorners[2], _box.AABBCorners[6]);
        if (distance < closestDistance) closestDistance = distance;
        if (!hitEdgeCapsule && distance <= _box.AABBRadius)
        {
            if (RayCapsuleIntersection(_ray, _box.Corners[2], _box.Corners[6], _box.AABBRadius, out t))
                hitEdgeCapsule = true;
        }
        distance = DistancePointEdge(_hitpoint, _box.AABBCorners[3], _box.AABBCorners[7]);
        if (distance < closestDistance) closestDistance = distance;
        if (!hitEdgeCapsule && distance <= _box.AABBRadius)
        {
            Debug.Log("Closest to capsule 3|7");
            if (RayCapsuleIntersection(_ray, _box.Corners[3], _box.Corners[7], _box.AABBRadius, out t))
                hitEdgeCapsule = true;
        }

        debugText.text = ("Closest Dist:" + closestDistance + "/" + _box.AABBRadius + "|" + hitEdgeCapsule);

        if (hitEdgeCapsule)
        {
            hitpoint = _ray.GetPointOnRay(t);        
        }
        _hitEdgeCapsule = hitEdgeCapsule;
        //Tell me if this was an edgecase
        return closestDistance <= _box.AABBRadius;
    }

    bool RayCapsuleIntersection(MonoRay _ray, Vector3 _edge0, Vector3 _edge1, float _radius, out float t)
    {
        float t0 = 0;
        if(RayCylinderIntersection(_ray, _edge0, _edge1, _radius, out t0))
        {
            t = t0;
            return true;
        }
        if (RaySphereIntersection(_ray, _edge0, _radius, out t0) || RaySphereIntersection(_ray, _edge1, _radius, out t0))
        {
            t = t0;
            return true;
        }
        else
        {
            t = 0;
            return false;
        }
    }

    bool RayCylinderIntersection(MonoRay _ray, Vector3 _edge0, Vector3 _edge1, float _radius, out float t)
    {
        t = 0;


        //Define fixed CoordinateSystem
        Vector3 z = (_edge1 - _edge0).normalized;
        Vector3 y = Vector3.up;
        if (z.y != 0)
        {
            y = Vector3.forward;
        }
        Vector3 x = Vector3.Cross(z, y).normalized;
            //CoordinateSystemMatrix
        Matrix4x4 C = new Matrix4x4(new Vector4(x.x, x.y, x.z, 0), new Vector4(y.x, y.y, y.z, 0), new Vector4(z.x, z.y, z.z, 0), new Vector4(0,0,0,1));
        //Debug.Log("Matrix C:\n" + C);

        //Transform Ray and Cylinder to fixed Coordinatesystem (capsule along z axis)
        Vector3 originC = C * _ray.Origin;
        Vector3 dirC = C * _ray.Direction;

        Vector3 edge0C = C * _edge0;
        Vector3 edge1C = C * _edge1;
        if (edge0C.z > edge1C.z)
            Swap(ref edge0C, ref edge1C);
        //Debug.Log("edge0:" + _edge0 + "|" + edge0C);
        //Debug.Log("edge1:" + _edge1 + "|" + edge1C);

        //Project to z-Axis

        Vector2 edge0CP = new Vector2(edge0C.x, edge0C.y);
        Vector2 edge1CP = new Vector2(edge1C.x, edge1C.y);

        Vector2 originCP = new Vector2(originC.x, originC.y);
        Vector2 dirCP = new Vector2(dirC.x, dirC.y);

        //Ray Circle Check
        if(RayCircleIntersection(originCP, dirCP, edge0CP, 1, out t))
        {
            //Debug.Log("CircleIntersection");
            //Interval Check
            Vector3 pC = originC + dirC * t;
            if (pC.z > edge1C.z || pC.z < edge0C.z)
            {
                //Debug.Log("out of Interval");
                return false;
            }
            return true;
        }
        return false;
    }

    bool RaySphereIntersection(MonoRay _ray, Vector3 _center, float _radius, out float t)
    {
        t = 0;
        //Solutions for t if the ray intersects the sphere
        float t0, t1;
        Vector3 L = _ray.Origin - _center;
        float a = Vector3.Dot(_ray.Direction, _ray.Direction);
        float b = 2 * Vector3.Dot(_ray.Direction, L);
        float c = Vector3.Dot(L, L) - _radius * _radius;
        if (!SolveQuadratic(a, b, c, out t0, out t1)) return false;

        if (t0 > t1) Swap(ref t0, ref t1);

        if (t0 < 0)
        {
            t0 = t1; //if t0 is negative let's use t1 instead
            if (t0 < 0) return false; //both t0 and t1 are negative
        }
        t = t0;
        return true;
    }

    //Same as Sphere but with Vector2
    bool RayCircleIntersection(Vector2 _rayOrigin, Vector2 _rayDirection, Vector2 _center, float _radius, out float t)
    {
        t = 0;
        //Solutions for t if the ray intersects the sphere
        float t0, t1;
        Vector2 L = _rayOrigin - _center;
        float a = Vector2.Dot(_rayDirection, _rayDirection);
        float b = 2 * Vector2.Dot(_rayDirection, L);
        float c = Vector2.Dot(L, L) - _radius * _radius;
        if (!SolveQuadratic(a, b, c, out t0, out t1)) return false;

        if (t0 > t1) Swap(ref t0, ref t1);

        if (t0 < 0)
        {
            t0 = t1; //if t0 is negative let's use t1 instead
            if (t0 < 0) return false; //both t0 and t1 are negative
        }
        t = t0;
        return true;
    }

    //Solver for quadratic  function describing the intersection t of ray and sphere
    bool SolveQuadratic(float a, float b, float c, out float x0, out float x1)
    {
        x0 = -1; x1 = -1;
        //Discriminant decides whether there is 0, 1 or 2 intersection points
        float discr = b * b - 4 * a * c;
        //if discr < 0, there is no intersection point
        if (discr < 0)
            return false;
        //if discr == 0, there is only one intersection point
        else if (discr == 0)
        {
            x0 = -0.5f * b / a;
            x1 = x0;
        }
        //if discr > 0, there is 2 intersection points
        else
        {
            float q = (b > 0) ?
                -0.5f * (b + Mathf.Sqrt(discr)) :
                -0.5f * (b - Mathf.Sqrt(discr));
            x0 = q / a;
            x1 = c / q;
        }
        if (x0 > x1) Swap(ref x0, ref x1);

        return true;
    }

    void Swap(ref float a, ref float b)
    {
        float tmp = a;
        a = b;
        b = tmp;
    }

    void Swap(ref Vector3 a, ref Vector3 b)
    {
        Vector3 tmp = a;
        a = b;
        b = tmp;
    }

    float DistancePointEdge(Vector3 _point, Vector3 _seg0, Vector3 _seg1)
    {
        Vector3 v = _seg1 - _seg0;
        Vector3 w = _point - _seg0;

        float c1 = Vector3.Dot(w, v);
        if (c1 <= 0)
            return Vector3.Distance(_point, _seg0);

        float c2 = Vector3.Dot(v, v);
        if (c2 <= c1)
            return Vector3.Distance(_point, _seg1);

        float b = c1 / c2;
        Vector3 pointB = _seg0 + b * v;
        return Vector3.Distance(_point, pointB);
    }
}
