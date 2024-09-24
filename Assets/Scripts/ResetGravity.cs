using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGravity : MonoBehaviour
{
    // The Rigidbody component of the GameObject
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();

        // Make sure the Rigidbody starts with gravity turned off
        rb.useGravity = false;
    }

    // This function is called when this GameObject collides with another
    private void OnCollisionEnter(Collision collision)
    {
        // Enable gravity on the Rigidbody when a collision happens
        rb.useGravity = true;
    }
}
