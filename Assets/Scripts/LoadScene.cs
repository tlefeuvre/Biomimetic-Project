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

    public List<GameObject> buttonParts = new List<GameObject>();
    // Start is called before the first frame update
     private void Awake()
    {
        //PlayerPrefs.SetInt("handType", (int)handType);
        //PlayerPrefs.SetInt("handId", 1);
        
    }
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        //GetComponent<BoxCollider>().enabled = false;
        
        foreach (GameObject part in buttonParts)
            part.SetActive(false);
        //StartCoroutine(ActivateButton());

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
        //UIHand.SetActive(false);

       
       

       
    }

    IEnumerator ActivateButton()
    {
        Debug.Log("ActivateButton0");
        yield return new WaitForSeconds(10);
        Debug.Log("ActivateButton");
        foreach (GameObject part in buttonParts)
            part.SetActive(true);

        GetComponent<BoxCollider>().enabled = true;



    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene("SecondScene 1", LoadSceneMode.Single);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand" || other.tag == "UpperClaw" || other.tag == "LowerClaw")
        {
            LoadNextScene();
        }
    }
}
