using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOwnGrab : MonoBehaviour
{
    public bool upperClaw, lowerClaw, isDestroy;
    private Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.color = Color.white;

    }

    void Update()
    {
        if(!isDestroy && upperClaw &&  lowerClaw)
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        Debug.Log("destroy");
        isDestroy = true;
        mat.color = Color.red;
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
