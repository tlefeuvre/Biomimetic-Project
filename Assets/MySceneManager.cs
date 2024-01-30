using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    [Header("Hand")]
    public GameObject normalHand;
    public GameObject clawHand;

    [Header("Others")]
    public HandType handType;


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
        normalHand.SetActive(false);
        clawHand.SetActive(false);

        handType = (HandType)PlayerPrefs.GetInt("handType");
        if(handType == HandType.HAND)
            normalHand.SetActive(true);
        else
            clawHand.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
