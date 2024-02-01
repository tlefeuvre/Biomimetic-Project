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

    public GameObject UIHand;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("handType", (int)handType);
        PlayerPrefs.SetInt("handId", (int)handId);

    }

    // Update is called once per frame
    void Update()
    {

        
    }
    public void SaveHand(int handId)
    {
        PlayerPrefs.SetInt("handId", handId);
        Debug.Log("hand " + handId);
        GetComponent<MySceneManager>().SetHandVisual();
        UIHand.SetActive(false);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene("SecondScene", LoadSceneMode.Single);

    }

}
