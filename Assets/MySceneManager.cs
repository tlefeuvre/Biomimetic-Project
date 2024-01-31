using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    [Header("Hand")]
    public List<GameObject> leftHand = new List<GameObject>();
    public List<GameObject> rightHand = new List<GameObject>();


    [Header("Others")]
    public HandType handType;
    public HandId handId;


    private static MySceneManager instance = null;
    public static MySceneManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
       

        SetHandVisual();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHandVisual()
    {
        for (int i = 0; i < leftHand.Count; i++)
        {
            leftHand[i].SetActive(false);
            rightHand[i].SetActive(false);
        }

        handType = (HandType)PlayerPrefs.GetInt("handType");
        handId= (HandId)PlayerPrefs.GetInt("handId");

        if(handId == HandId.LEFT)
            leftHand[(int)handType].SetActive(true);

        if (handId == HandId.RIGHT)
            rightHand[(int)handType].SetActive(true);

       
    }
}
