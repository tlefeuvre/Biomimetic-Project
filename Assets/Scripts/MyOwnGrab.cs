using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOwnGrab : MonoBehaviour
{
    public List<GameObject> RigidbodyList;
    public ParticleSystem juice;
    public bool upperClaw, lowerClaw, isDestroy;

    public float force;
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
            rb.useGravity = true;

        }

        foreach (GameObject obj in RigidbodyList)
        {
            Rigidbody rbf = obj.GetComponent<Rigidbody>();
            if (rbf)
            {
                rbf.isKinematic = true;
                rbf.useGravity = false;

            }
            obj.GetComponent<BoxCollider>().isTrigger = true;

        }

        StartCoroutine("ActivateCollider"); 
    }

    IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(5.0f);
        GetComponent<BoxCollider>().enabled = true;

    }
    void Update()
    {
        if(!isDestroy && upperClaw &&  lowerClaw)
        {
            Debug.Log("destroy");

            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        Debug.Log("fonction DestroyObject");

        isDestroy = true;
        if(juice)
            juice.Play();

        Measures.Instance.AddBrokenTag(this.tag);
        FruitsSpawner.Instance.CallNewRound();

        if (RigidbodyList.Count == 0)
            StartCoroutine("SpawnNewFruit");

        foreach (GameObject obj in RigidbodyList)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.AddForce(Vector3.up* force, ForceMode.Impulse);
            }
            obj.GetComponent<BoxCollider>().isTrigger = false;
        }


        Debug.Log("fin fonction DestroyObject");

    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (other.tag == "UpperClaw") 
            upperClaw = true;

        if(other.tag =="LowerClaw")
            lowerClaw = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "UpperClaw")
            upperClaw = false;

        if (other.tag == "LowerClaw")
            lowerClaw = false;
    }

    IEnumerator SpawnNewFruit()
    {
        Debug.Log("SpawnNewFruit debut");

        yield return new WaitForSeconds(1.0f);
        Debug.Log("SpawnNewFruit fin");

        Destroy(gameObject);
        

    }
}
