using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [Header("Parameters")]
    public HandType handType;
    public HandId handId;
    public float timeNextScene;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
        PlayerPrefs.SetInt("handType", (int)handType);
        PlayerPrefs.SetInt("handId", (int)handId);

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
        GetComponent<MySceneManager>().SetHandVisual();
    }


}
