using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject glitter;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Grabbed()
    {
        glitter.SetActive(false);
        NewExpManager.Instance.KeyGrabbed();
    }
}
