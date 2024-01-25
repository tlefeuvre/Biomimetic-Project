using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public float timeNextScene;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer + timeNextScene)
            SceneManager.LoadScene("SecondScene",LoadSceneMode.Single);

    }
}
