using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Measures : MonoBehaviour
{
    public bool isTimer;
    public float elapsedTime;
    public List<string> brokeOrder = new List<string>();

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


        isTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
            elapsedTime += Time.deltaTime;
    }
    public void SetTimer()
    {
        isTimer = true;
    }
    public void UnsetTimer()
    {
        isTimer = false;
    }
    public void AddBrokenTag(string tag)
    {
        brokeOrder.Add(tag);
    }

}
