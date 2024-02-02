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
     private void Awake()
    {
        PlayerPrefs.SetInt("handType", (int)handType);
        PlayerPrefs.SetInt("handId", 1);
        
    }
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Renderer>().enabled = false;

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

        ActivateButton();
    }

    public void ActivateButton()
    {
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Renderer>().enabled = true;


    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene("SecondScene", LoadSceneMode.Single);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand" || other.tag == "UpperClaw" || other.tag == "LowerClaw")
        {
            LoadNextScene();
        }
    }
}
