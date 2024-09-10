using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstraintRotation : MonoBehaviour
{
    [Header("Rotation Constraints")]
    public bool xR;
    public bool yR;
    public bool zR;

    public float xRConstraint;
    public float yRConstraint;
    public float zRConstraint;

    [Header("Rotation Offsets")]
    public float xOffset;
    public float yOffset;
    public float zOffset;

   /* [Header("Rotation Max Value")]
    public float xRMaxValue;
    public float yRMaxValue;
    public float zRMaxValue;*/


    [Header("Position Constraints")]
    public bool xP;
    public bool yP;
    public bool zP;
    public float xPConstraint;
    public float yPConstraint;
    public float zPConstraint;


    [Header("Limit rotation")]
    public bool isZInfLimited;
    public bool isZUpfLimited;
    public float ZLimitValue;

    private void Start()
    {
    
    }
    void Update()
    {
        float xA = transform.localEulerAngles.x + xOffset;
        float yA = transform.localEulerAngles.y + yOffset;
        float zA = transform.localEulerAngles.z + zOffset;

       /* if (xRMaxValue != 0 && xA > xRMaxValue)
            xA = xRMaxValue;
        if (yRMaxValue != 0 && yA > yRMaxValue)
            yA = yRMaxValue;
        if (zRMaxValue != 0 && zA > zRMaxValue)
            zA = zRMaxValue;*/
       if(isZInfLimited && zA > ZLimitValue)
            zA = ZLimitValue;

       if(isZUpfLimited)
            Debug.Log(zA  + "/ " + ZLimitValue);

       if(isZUpfLimited && zA-360 < ZLimitValue)
        {
            Debug.Log("inf");
            zA = ZLimitValue;
        }

        transform.localEulerAngles = new Vector3(xR?xRConstraint:xA, yR?yRConstraint:yA,  zA);

        float xPos = transform.localPosition.x;
        float yPos = transform.localPosition.y;
        float zPos = transform.localPosition.z;
        transform.localPosition = new Vector3(xP ? xPConstraint : xPos, yP ? yPConstraint : yPos, zP ? zPConstraint : zPos);

    }
}
