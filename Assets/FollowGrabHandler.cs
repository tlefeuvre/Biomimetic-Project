using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGrabHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(target.transform.position);
    }
}
