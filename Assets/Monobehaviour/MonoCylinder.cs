using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class MonoCylinder : MonoBehaviour {

    Vector3 velocity;
    public Vector3 angularVelocity;
    public Vector3 torque;

    public float mass;
    public Vector3 momentsOfInertia;

    public Vector3 force;
    public Vector3 forcePosition;

	// Use this for initialization
	void Start ()
    {
        momentsOfInertia.x = mass * 0.5f * 0.5f * (0.5f * 0.5f + 0.125f*0.125f / 3.0f);
        momentsOfInertia.z = momentsOfInertia.x;
        momentsOfInertia.y = mass * 0.5f * 0.125f * 0.125f;

    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        velocity += (force / mass) * Time.fixedDeltaTime;
        torque = Vector3.Cross(forcePosition, force);
        angularVelocity += new Vector3(torque.x / momentsOfInertia.x, torque.y / momentsOfInertia.y, torque.z / momentsOfInertia.z) * Time.fixedDeltaTime;
        //Apply Velocities
        transform.localPosition += velocity;
        transform.localRotation *= Quaternion.Euler(angularVelocity);
	}
}
