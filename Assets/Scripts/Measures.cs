using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandType
{
    HAND,
    CLAW
}

public enum HandId
{
    LEFT,
    RIGHT
}
public class Measures : MonoBehaviour
{
    [Header("Total Time")]
    public bool isTimer;
    public float totalElapsedTime;

    [Header("Fruits order")]
    public List<string> brokeOrder = new List<string>();


    [Header("Round Time")]
    public List<float> roundElapsedTime = new List<float>();
    public float elapsedTime;

 

    private static Measures instance = null;
    public static Measures Instance => instance;
    // Start is called before the first frame update
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
        isTimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
            elapsedTime += Time.deltaTime;
    }

    public void NewTimer(int nbRoundMaw)
    {
        roundElapsedTime.Add(elapsedTime);
        elapsedTime = 0;

        //fin des rounds
        if (nbRoundMaw == roundElapsedTime.Count)
            foreach (var time in roundElapsedTime)
                totalElapsedTime += time;

    }
    public void AddBrokenTag(string tag)
    {
        brokeOrder.Add(tag);
    }

    public void EndOfExp()
    {

    }

}
