using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bonkClip;
    public AudioClip clawClip;

    private float timer;
    private bool isPlaying;

    private float bonkTimer;
    private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        bonkTimer = Time.time;
        timer = Time.time;
        isPlaying = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timer+5.0f)
            isPlaying = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!audioSource || !isPlaying)
            return;

        if(GetComponent<HandVelocity>() != null )
            velocity = GetComponent<HandVelocity>().GetMagnitude();

        if (tag == "Hand" && collision.gameObject.tag == "Decor" && Time.time > bonkTimer+1.0f && velocity > 2.0f)
        {
            bonkTimer = Time.time;
            PlaySoundClip(bonkClip);
        }
        if((tag == "UpperClaw" && collision.gameObject.tag == "LowerClaw") || (tag == "LowerClaw" && collision.gameObject.tag == "UpperClaw"))
        {
            PlaySoundClip(clawClip);
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!audioSource || !isPlaying)
            return;

        if (GetComponent<HandVelocity>() != null)
            velocity = GetComponent<HandVelocity>().GetMagnitude();

        if (tag == "Hand" && other.gameObject.tag == "Decor" && Time.time > bonkTimer + 1.0f && velocity > 2.0f)
        {
            bonkTimer = Time.time;

            PlaySoundClip(bonkClip);
        }
        if ((tag == "UpperClaw" && other.gameObject.tag == "LowerClaw") || (tag == "LowerClaw" && other.gameObject.tag == "UpperClaw"))
        {
            PlaySoundClip(clawClip);
        }
    }
    public void PlaySoundClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
