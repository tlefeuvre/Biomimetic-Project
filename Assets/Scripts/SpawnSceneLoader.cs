using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSceneLoader : MonoBehaviour
{
   
    public GameObject sceneLoader2;
    // Start is called before the first frame update
    void Start()
    {
     


        StartCoroutine(SpawnObject());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnObject()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        sceneLoader2.SetActive(true);

    }
}
