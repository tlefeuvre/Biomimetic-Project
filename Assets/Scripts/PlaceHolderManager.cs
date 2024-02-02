using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceHolderManager : MonoBehaviour
{
    public float pressedPosY = -0.03f;
    public float unpressedPosY = 0;
    private Vector3 initialPos;

    public GameObject buttonObject;
    // Start is called before the first frame update
    void Start()
    {
        initialPos =  Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IsPressed(bool isPressed)
    {
        Debug.Log("IS PRESSED !!!!!! " + isPressed);
        GetComponent<BoxCollider>().enabled = !isPressed;
        if (isPressed)
            buttonObject.transform.localPosition = new Vector3(initialPos.x, initialPos.y + pressedPosY, initialPos.z);
        else
            buttonObject.transform.localPosition = new Vector3(0, 0,0);


    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Hand" || other.tag == "UpperClaw" || other.tag == "LowerClaw")
            if (FruitsSpawner.Instance.isRoundFinished)
            {
            
                Debug.Log("place holder triggered");
                FruitsSpawner.Instance.CallNewRound();
                //gameObject.SetActive(false);
                IsPressed(true);
            }

    }
}
