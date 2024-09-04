using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmphoraManager : MonoBehaviour
{

    public List<GameObject> RigidbodyList;
    public GameObject grabParent;

    public float explodeForce;
    // Start is called before the first frame update
    void Start()
    {
        ActivateGravity();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Awake()
    {
        BoxCollider bx = GetComponent<BoxCollider>();
        if (bx)
        {
            bx.enabled = true;
            bx.isTrigger = false;
        }
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        foreach (GameObject obj in RigidbodyList)
        {
            Rigidbody rbf = obj.GetComponent<Rigidbody>();
            if (rbf)
            {
                rbf.isKinematic = true;
                rbf.useGravity = false;

            }
            BoxCollider bc = obj.GetComponent<BoxCollider>();
            if (bc)
                bc.isTrigger = true;

        }


    }
    public void ActivateGravity()
    {
        grabParent.GetComponent<Rigidbody>().isKinematic = true;
        grabParent.GetComponent<Grabbable>().enabled = false;
        grabParent.GetComponent<HandGrabInteractable>().enabled = false;
        grabParent.GetComponent<GrabAmphore>().enabled = false;
        grabParent.GetComponent<BoxCollider>().enabled = false;

        Debug.Log("ActivateGravity");
        foreach (GameObject obj in RigidbodyList)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.AddForce(Vector3.up * explodeForce);
            }
            obj.GetComponent<BoxCollider>().isTrigger = false;

        }
        StartCoroutine(StopVelocity());
    }

    IEnumerator StopVelocity()
    {
        yield return new WaitForSeconds(1.0f);
        foreach(GameObject obj in RigidbodyList)
        {
            obj.SetActive(false);
            /*Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.velocity = Vector3.zero;
             
            }*/
        }
    }

    

}
