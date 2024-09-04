using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Recorder.OutputPath;

public class ItemManager : MonoBehaviour
{
    private AudioSource audioSource;
    public List<GameObject> variantsMain = new List<GameObject>();
    public List<GameObject> variantsTop = new List<GameObject>();
    public int objectIndex;
    public AudioClip[] woodSounds;
    public AudioClip[] metalSounds;
    public AudioClip[] potterySounds;
    public AudioClip[] damageSoundlist;
    private AudioClip DamageSound;
    public float handMagnitudeToExplode = 4;

    public GameObject chestManagerChild;
    public GameObject amphoraManagerChild;
    public int indexVariant;

    public Transform keySpawner;
    public GameObject AmphoreGrab;

    private bool isDestroyed = false;

    private bool isOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        indexVariant = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObjectHit()
    {
        if (!isDestroyed)
        {
            NewExpManager.Instance.NewHit();

        }

        if (indexVariant < variantsMain.Count-1)
            variantsMain[indexVariant].gameObject.SetActive(false);


        if(variantsTop.Count > 0 && variantsTop.Count-1 > indexVariant)
        {
            variantsTop[indexVariant].gameObject.SetActive(false);

        }
        
        indexVariant++;

        //activate
        if (indexVariant < variantsMain.Count)
        {
            variantsMain[indexVariant].gameObject.SetActive(true);

        }
        if (indexVariant < variantsTop.Count)
        {
            variantsTop[indexVariant].gameObject.SetActive(true);
           

        }


        if (indexVariant >= variantsMain.Count -1)
        {
            if (!isDestroyed)
            {
                Destroyed();
                if (chestManagerChild)
                {
                    chestManagerChild.GetComponent<ChestManager>().IsBroken();
                }
                if (AmphoreGrab)
                {
                    AmphoreGrab.SetActive(false);
                }
            }
        }


    }
    public void Destroyed()
    {
        switch (objectIndex)
        {
            case 1:
                damageSoundlist = potterySounds; ;
                break;
            case 2:
                damageSoundlist = woodSounds; ;
                break;
            case 3:
                damageSoundlist = metalSounds; ;
                break;
        }
        int index = Random.Range(0, damageSoundlist.Length);
        DamageSound = damageSoundlist[index];

        audioSource.clip = DamageSound;
        audioSource.Play();

        NewExpManager.Instance.AddBrokenTag(this.tag);
        NewExpManager.Instance.RemoveFromList(this.gameObject);

        if(!isOpened && !isDestroyed)
            NewExpManager.Instance.NewDestroyedObject();
        //Destroy(this.gameObject);
        isDestroyed = true;
    }
    public void Opened()
    {
        Debug.Log("itemmanager");


        NewExpManager.Instance.AddBrokenTag(this.tag);
        NewExpManager.Instance.RemoveFromList(this.gameObject);

        if(!isDestroyed && !isOpened)
            NewExpManager.Instance.NewOpenedObject();

        isOpened = true;

    }
    public void spawnKey(GameObject key)
    {
        GameObject keyObj = Instantiate(key, keySpawner);
        keyObj.transform.localPosition = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Hand" || collision.transform.tag  == " UpperClaw" || collision.transform.tag == " LowerClaw")
        {

            Debug.Log("toucher le item collision !!!!");
            ObjectHit();

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (chestManagerChild )    
        {
            if (chestManagerChild.GetComponent<ChestManager>().GetLookAt())
            {

                return;
            }
        }

        
        if (other.transform.tag == "Hand" || other.transform.tag == " UpperClaw" || other.transform.tag == " LowerClaw")
        {
            float handSpeed = other.gameObject.GetComponent<HandVelocity>().GetMagnitude();
            if (handSpeed > handMagnitudeToExplode)
            {
                
                

                ObjectHit();
                

            }

            Debug.Log("toucher le item !!!!");

        }
        
      
    }

}
