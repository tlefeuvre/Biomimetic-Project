using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAmphore : MonoBehaviour
{
    public Vector3 distanceFromParent;
    private GameObject parent;
    public Vector3 startPos;
    public bool isBeingGrab;

    public Transform spawnPos;
    void Start()
    {
        parent = transform.parent.gameObject;

        distanceFromParent = parent.transform.position - transform.position;

        startPos = transform.localPosition;
    }

    void Update()
    {
        if (isBeingGrab)
        {
            //parent.transform.position = transform.position +  distanceFromParent;
            //parent.transform.localRotation = transform.localRotation;
        }
        else
        {
            transform.position = spawnPos.position;

        }

    }

    public void IsBeingGrab()
    {
        //parent.GetComponent<Rigidbody>().isKinematic = true;
        //parent.GetComponent<Rigidbody>().useGravity = false;

        Debug.Log("isBeingGrab");
        isBeingGrab = true;
    }
    public void IsNotBeingGrab()
    {
        //parent.GetComponent<Rigidbody>().useGravity = true;
       // parent.GetComponent<Rigidbody>().isKinematic = false;
        
        isBeingGrab = false;

    }

    public void Replace()
    {
        transform.position = spawnPos.position;
    }
}
