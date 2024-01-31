using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float amplitude;
    public float speed;
    public List<RoundedBoxProperties> waves = new List<RoundedBoxProperties>();
    private List<float> wavesB = new List<float>();

    private float initBorder;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var wave in waves)
        {
            wavesB.Add(wave.BorderOuterRadius);
        }


    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0;i<wavesB.Count;i++)
        {
            waves[i].BorderOuterRadius = wavesB[i] + Mathf.Sin(Time.time * speed) * amplitude;

           
        }
        
    }
}
