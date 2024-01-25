using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    public List<GameObject> RigidbodyList;
    public ParticleSystem juice;
    public bool upperClaw, lowerClaw, isDestroy;

    public float explosionForce;

    private Vector3 newPos;
    private Vector3 oldPos;
    private Vector3 velocity;

    public int throwForce;
    private float initTimer;



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
            BoxCollider bc = obj.GetComponent<BoxCollider>();
            if(bc)
                bc.isTrigger = true;

        }

        //StartCoroutine("ActivateCollider"); 
    }

    public float magnitudeToExplode;
    public float handMagnitudeToExplode;

    /*IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(5.0f);
        GetComponent<BoxCollider>().enabled = true;

    }*/

    private void Start()
    {
        initTimer = Time.time;


        newPos = transform.position;
        oldPos = transform.position;
    }
    void Update()
    {
        if(!isDestroy && upperClaw &&  lowerClaw)
        {
            Debug.Log("destroy");

            DestroyObject();
        }


    }
    private void FixedUpdate()
    {
        newPos = transform.position;
        velocity = (newPos - oldPos) / Time.fixedDeltaTime;
        oldPos = newPos;
    }


    public void DestroyObject()
    {
        if (isDestroy)
            return;

        Debug.Log("fonction DestroyObject");

        isDestroy = true;
        if(juice)
            juice.Play();

        Measures.Instance.AddBrokenTag(this.tag);
        FruitsSpawner.Instance.CallNewRound();

        if (RigidbodyList.Count == 0)
            StartCoroutine("SpawnNewFruit");
        else
            GetComponent<BoxCollider>().isTrigger = true;

        foreach (GameObject obj in RigidbodyList)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.AddForce(Vector3.up* explosionForce, ForceMode.Impulse);
            }
            obj.GetComponent<BoxCollider>().isTrigger = false;
        }


        Debug.Log("fin fonction DestroyObject");

    }

    public void Throw()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (!rb)
            return;

        rb.AddForce(velocity * throwForce);
    }
    IEnumerator SpawnNewFruit()
    {
        yield return new WaitForSeconds(1.0f);

        //this.GetComponent<Renderer>().enabled = false;
        Destroy(gameObject);


    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "UpperClaw") 
            upperClaw = true;

        if(other.tag =="LowerClaw")
            lowerClaw = true;
        if(other.tag == "RightHand")
        {

            float handSpeed = other.gameObject.GetComponent<HandVelocity>().GetMagnitude();
            if(handSpeed > handMagnitudeToExplode)
            {
                DestroyObject();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "UpperClaw")
            upperClaw = false;

        if (other.tag == "LowerClaw")
            lowerClaw = false;
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (Time.time < initTimer + 2.0f)
            return;

        Debug.Log("collision ??");
        if (velocity.magnitude > magnitudeToExplode)
        {
            DestroyObject();
        }
    }
    

}
