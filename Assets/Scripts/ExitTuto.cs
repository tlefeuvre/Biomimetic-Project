using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTuto : MonoBehaviour
{
    public bool isDestroyed = false;
    public bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroyed && isOpen)
        {
            StartCoroutine(Exitlevel());
        }
    }

    //public void Exitlevel()
    //{
    //    SceneManager.LoadScene("FirstSceneNEW", LoadSceneMode.Single);
    //}
    IEnumerator Exitlevel()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("FirstSceneNEW", LoadSceneMode.Single);

    }
}
