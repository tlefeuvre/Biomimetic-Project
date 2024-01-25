using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDirection : MonoBehaviour
{
    public int force;

    private Vector3 newPos;
    private Vector3 oldPos;
    private Vector3 velocity;

    private float initTimer;

    // Start is called before the first frame update
    void Start()
    {
        initTimer = Time.time;
        newPos = transform.position;
        oldPos = transform.position;
    }

    void FixedUpdate()
    {
        newPos = transform.position;  
        velocity = (newPos - oldPos) / Time.fixedDeltaTime;  
        oldPos = newPos;
        //Debug.Log(velocity);
        //Debug.Log(velocity.magnitude);

    }

    void Update()
    {
     
    }
 
    public void Throw()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (!rb)
            return;

        rb.AddForce(velocity * force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time < initTimer + 2.0f)
            return;

        Debug.Log("collision ??");
        if(velocity.magnitude > 0.5f)
        {
            GetComponent<FruitManager>().DestroyObject();
        }
    }
}
