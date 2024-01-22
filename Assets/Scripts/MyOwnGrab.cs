using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOwnGrab : MonoBehaviour
{
    public List<GameObject> RigidbodyList;
    public ParticleSystem juice;
    public bool upperClaw, lowerClaw, isDestroy;
    private Material mat;
    public void Awake()
    {
        GetComponent<BoxCollider>().enabled = false;
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

        foreach (GameObject obj in RigidbodyList)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.useGravity = true;
                rb.AddForce(Vector3.up,ForceMode.Impulse);
            }
            obj.GetComponent<BoxCollider>().isTrigger = false;
        }

        Measures.Instance.AddBrokenTag(this.tag);
        FruitsSpawner.Instance.SpawnNewFruit(this.tag);
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
