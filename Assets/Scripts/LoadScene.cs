using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public HandType HandType;
    public float timeNextScene;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
        PlayerPrefs.SetInt("handType", (int)HandType);

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer + timeNextScene)
            SceneManager.LoadScene("SecondScene",LoadSceneMode.Single);

        
    }
    public void SaveHand(int handId)
    {
        PlayerPrefs.SetInt("handId", 0);
        Debug.Log("hand " + handId);

    }
}
