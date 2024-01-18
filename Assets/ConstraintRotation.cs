using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintRotation : MonoBehaviour
{
    public bool xR, yR, zR;
    public float xRConstraint, yRConstraint, zRConstraint;
    public float zOffset;

    public bool xP, yP, zP;
    public float xPConstraint, yPConstraint, zPConstraint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xA = transform.localEulerAngles.x;
        float yA = transform.localEulerAngles.y;
        float zA = transform.localEulerAngles.z;
        transform.localEulerAngles = new Vector3(xR?xRConstraint:xA, yR?yRConstraint:yA, zR?zRConstraint:zA+ zOffset);

        float xPos = transform.localPosition.x;
        float yPos = transform.localPosition.y;
        float zPos = transform.localPosition.z;
        transform.localPosition = new Vector3(xP ? xPConstraint : xPos, yP ? yPConstraint : yPos, zP ? zPConstraint : zPos);

    }
}
