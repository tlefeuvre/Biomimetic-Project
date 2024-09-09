
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public Transform target;
    public GameObject vrHandler;
    public Transform handlePosition;
    public float openSpeed;
    public Vector3 vectorLookAt;
    public bool lookat;
    private Vector3 start;

    private Rigidbody rbHandler;

    public bool isOpened ;

    public bool isBroken ;

    public float angleToOpen;
    private void Start()
    {
        start = transform.localEulerAngles;
        Debug.Log(transform.localEulerAngles);
        lookat = false;

        rbHandler=  vrHandler.GetComponent<Rigidbody>();
        isBroken = false;
        isOpened = false;

    }
    void Update()
    {
        

       
        /*Quaternion targ = Quaternion.LookRotation(target.transform.position - this.transform.position);
        /this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targ, Time.deltaTime);
        Vector3 diff = target.transform.position - target.transform.forward;
        Quaternion changeInRotation = Quaternion.FromToRotation(transform.forward, diff);
        Vector3 euler = changeInRotation.eulerAngles;

       


        Vector3 targetDir = target.transform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        Debug.Log(angle);
        */
        Vector3 lookAtX = new Vector3(target.position.x, transform.position.y, transform.position.z);
        Vector3 lookAtY = new  Vector3(transform.position.x, target.position.y, transform.position.z);
        Vector3 lookAtZ = new Vector3(transform.position.x, transform.position.y, target.position.z);

        bool tmp = true;
        if(handlePosition.position.x > target.position.x)
        {
            tmp = false;
        }
        if(lookat)
        {

            //Vector3 lookatvectEuler = new Vector3(transform.localEulerAngles.x * vectorLookAt.x + start.x * ((vectorLookAt.x+1)%2), transform.localEulerAngles.y * vectorLookAt.y + start.y * ((vectorLookAt.y + 1) % 2), transform.localEulerAngles.z * vectorLookAt.z + start.z * ((vectorLookAt.z + 1) % 2));
            //transform.localEulerAngles = lookatvectEuler;
            //transform.LookAt(target, new Vector3(0, 1, 0));


            Quaternion lookOnLook = Quaternion.LookRotation(target.transform.position - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime* openSpeed);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, start.y, start.z);
            if (!isOpened && transform.localEulerAngles.x < 360-angleToOpen)
            {
                Debug.Log("chestmanager + " + isOpened);
                isOpened = true;
                transform.parent.gameObject.GetComponent<ItemManager>().Opened();
            }
        }
        else
        {
            vrHandler.transform.position = handlePosition.transform.position;
            vrHandler.transform.rotation = handlePosition.transform.rotation;

            rbHandler.velocity = Vector3.zero;
        }

        /*Vector3 difference = target.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        */
    }

    public bool GetLookAt()
    {
        //return lookat to dont destroy chest when opening it
        return lookat;
    }
    public void HandleIsSelected()
    {
        lookat = true;

        Debug.Log("IS GRABB");
    }
    public void HandleNotSelected()
    {
        lookat = false;
        Debug.Log("IS NOT GRABB");

    }

    public void IsBroken()
    {
        isBroken = true;
    }
}
