using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceHolderManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Hand" || other.tag == "UpperClaw" || other.tag == "LowerClaw")
            if (FruitsSpawner.Instance.isRoundFinished)
            {
            
                Debug.Log("place holder triggered");
                FruitsSpawner.Instance.CallNewRound();
                gameObject.SetActive(false);
            }

    }
}
