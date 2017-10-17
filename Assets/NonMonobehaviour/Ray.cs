using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray
{
    Vector3 origin;
    public Vector3 Origin { get { return origin; } }

    Vector3 direction;
    public Vector3 Direction { get { return direction; } }

    public Ray(Vector3 _origin, Vector3 _direction)
    {
        origin = _origin;
        direction = _direction;
    }
}
