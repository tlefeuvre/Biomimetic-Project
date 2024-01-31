using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    [Header("Hand")]
    public List<GameObject> leftHand = new List<GameObject>();
    public List<GameObject> rightHand = new List<GameObject>();


    private HandType handType;

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
        Debug.Log("set hand visual");

        for (int i = 0; i < leftHand.Count; i++)
        {
            leftHand[i].SetActive(false);
            rightHand[i].SetActive(false);
        }

        handType = (HandType)PlayerPrefs.GetInt("handType");
        HandId handId = (HandId)PlayerPrefs.GetInt("handId");

        if(handId == HandId.LEFT)
            leftHand[(int)handType].SetActive(true);

        if (handId == HandId.RIGHT)
            rightHand[(int)handType].SetActive(true);

       
    }

    public HandType GetHandType()
    {
        return handType;
    }
}
