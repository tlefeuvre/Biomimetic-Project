using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTutoChest : MonoBehaviour
{
    public ExitTuto exitTuto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //exitTuto.isOpen = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Hand")
        {
            exitTuto.isOpen = true;
        }
      
    }

   
}
