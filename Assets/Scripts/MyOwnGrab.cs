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
        isDestroy = true;
        if(juice)
            juice.Play();

        Measures.Instance.AddBrokenTag(this.tag);
        FruitsSpawner.Instance.SpawnNewFruit(this.tag);

        if (RigidbodyList.Count == 0)
        {
            Destroy(gameObject);
            return;
        }
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
}
