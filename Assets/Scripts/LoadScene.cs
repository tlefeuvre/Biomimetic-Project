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
        PlayerPrefs.SetInt("handType", (int)MySceneManager.Instance.handType);
        PlayerPrefs.SetInt("handId", (int)MySceneManager.Instance.handId);

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer + timeNextScene)
            SceneManager.LoadScene("SecondScene",LoadSceneMode.Single);

        
    }
    public void SaveHand(int handId)
    {
        PlayerPrefs.SetInt("handId", handId);
        Debug.Log("hand " + handId);
        MySceneManager.Instance.SetHandVisual();
    }
}
