using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOwnGrab : MonoBehaviour
{
    public List<GameObject> RigidbodyList;
    public ParticleSystem juice;
    public bool upperClaw, lowerClaw, isDestroy;
    private Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        if(mat)
        mat.color = Color.white;

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
        if (mat)
            mat.color = Color.red;
        juice.Play();
        foreach (GameObject obj in RigidbodyList)
        {
            obj.GetComponent<Rigidbody>().useGravity = true;
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
