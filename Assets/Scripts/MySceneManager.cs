using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    [Header("Hand")]
    public List<GameObject> leftHand = new List<GameObject>();
    public List<GameObject> rightHand = new List<GameObject>();


    private HandType handType;

    void Start()
    {
        if(leftHand.Count > 1 && leftHand[0] == null)
        {
            leftHand[0] = GameObject.FindGameObjectWithTag("Missing1");
            leftHand[1] = GameObject.FindGameObjectWithTag("Missing2");
        }
        if (rightHand.Count > 1 && rightHand[0] == null)
        {
            rightHand[0] = GameObject.FindGameObjectWithTag("Missing3");
            rightHand[1] = GameObject.FindGameObjectWithTag("Missing4");
        }
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
        handType = HandType.HAND;
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
