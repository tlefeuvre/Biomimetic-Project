using Meta.XR.Editor.Tags;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardHandle : MonoBehaviour
{
    public Transform target;
    public Vector3 start;
    private void Start()
    {
        start = transform.localEulerAngles;
        Debug.Log(transform.localEulerAngles);

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


        transform.LookAt(target, new Vector3(0,1,0));
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, start.y, start.z);


        /*Vector3 difference = target.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        */
    }
}
