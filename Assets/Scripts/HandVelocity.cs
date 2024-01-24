using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVelocity : MonoBehaviour
{
    private Vector3 newPos;
    private Vector3 oldPos;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        newPos = transform.position;
        oldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newPos = transform.position;
        velocity = (newPos - oldPos) / Time.fixedDeltaTime;
        oldPos = newPos;
        Debug.Log(velocity.magnitude);
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }
    public float GetMagnitude()
    {
        return velocity.magnitude;
    }
}
