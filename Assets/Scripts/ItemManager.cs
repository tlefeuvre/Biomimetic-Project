using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public AudioSource audioSource;
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


    private bool keySpawned = false;
    public float rotationSpeed = 50f;
    public float levitationHeight = 0.1f;
    private GameObject instantiatedkey;
    private float initialY;
    public float levitationSpeed = 0.5f;
 

    private bool isDestroyed = false;

    private bool isOpened = false;
    private bool Exitcollider = false;

    public bool upperClaw = false;
    public bool lowerClaw = false;

    public bool isGrabbable = false;

    public Transform parentTransform;

    private float velocityTohit = 0.04f;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = this.GetComponent<AudioSource>();
        indexVariant = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (keySpawned)
        //{
        //    instantiatedkey.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        //    float newY = initialY + Mathf.Sin(Time.time * levitationSpeed) * levitationHeight;
        //    Vector3 currentPosition = instantiatedkey.transform.position;
        //    currentPosition.y = newY;
        //    instantiatedkey.transform.position = currentPosition;
        //}
    }
    IEnumerator waiter()
    {

        yield return new WaitForSecondsRealtime(0.3f);
        Exitcollider = false;
    }
    public void ObjectHit()
    {
        Exitcollider = true;
        DebugLogs.Instance.NewHit(transform.tag);

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
            if (transform.tag == "Amphora" || transform.tag == "Amphora2")
            {
                Debug.Log("move parent");
                variantsMain[indexVariant].gameObject.transform.SetParent(parentTransform);
            }
            variantsMain[indexVariant].gameObject.SetActive(true);

        }
        if (indexVariant < variantsTop.Count)
        {
            if (transform.tag == "Amphore" || transform.tag == "Amphore2")
            {
                variantsTop[indexVariant].gameObject.transform.SetParent(parentTransform);
            }
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
                    //AmphoreGrab.SetActive(false);
                }
                
            }
        }
        

    }
    
        public void Destroyed()
    {
        Exitcollider = true;
        DebugLogs.Instance.NewDestroy(transform.tag);


        Debug.Log("Hello");

        NewExpManager.Instance.AddBrokenTag(this.tag);
        NewExpManager.Instance.RemoveFromList(this.gameObject);

        if(!isOpened && !isDestroyed)
            NewExpManager.Instance.NewDestroyedObject(this.gameObject);
        //Destroy(this.gameObject);
        isDestroyed = true;
    }
    public void Opened()
    {
        Debug.Log("trigeriton");
    
        DebugLogs.Instance.NewOpened(transform.tag);
        Debug.Log("itemmanager");


        NewExpManager.Instance.AddBrokenTag(this.tag);
        NewExpManager.Instance.RemoveFromList(this.gameObject);

        if(!isDestroyed && !isOpened)
            NewExpManager.Instance.NewOpenedObject(this.gameObject);

        isOpened = true;

    }
    public void spawnKey(GameObject key)
    {
        keySpawned = true;
        GameObject keyObj = Instantiate(key, keySpawner);
        keyObj.transform.localPosition = Vector3.zero;
        instantiatedkey = keyObj;
        initialY = keyObj.transform.position.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
       

        if (collision.transform.tag == "Hand" || collision.transform.tag  == " UpperClaw" || collision.transform.tag == " LowerClaw" && !Exitcollider)
        {

            Debug.Log(GetComponent<Rigidbody>().velocity.magnitude + "Speed");
            ObjectHit();

        }


        if ((transform.tag == "Amphora" || transform.tag == "Amphora2") && GetComponent<Rigidbody>().velocity.magnitude > velocityTohit && collision.transform.tag == "Floor")
        {
            ObjectHit();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter!");

        if (chestManagerChild )    
        {
            if (chestManagerChild.GetComponent<ChestManager>().GetLookAt())
            {

                return;
            }
        }

        
        if (other.transform.tag == "UpperClaw" && !isGrabbable)
            upperClaw = true;
        if (other.transform.tag == "LowerClaw" && !isGrabbable)
            lowerClaw = true;

        if(upperClaw && lowerClaw && !isGrabbable && !Exitcollider)
        {
            ObjectHit();

        }

        if (other.transform.tag == "Hand" )
        {
            float handSpeed = other.gameObject.GetComponent<HandVelocity>().GetMagnitude();
            if (handSpeed > handMagnitudeToExplode && !Exitcollider)
            {
                
                

                ObjectHit();
                

            }

            Debug.Log(other.gameObject.GetComponent<HandVelocity>().GetMagnitude()+"toucher le item 2 !!!!");

        }

        if ((transform.tag == "Amphora" || transform.tag == "Amphora2") &&  GetComponent<Rigidbody>().velocity.magnitude > velocityTohit && other.transform.tag =="Floor")
        {
            ObjectHit();

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == " UpperClaw")
            upperClaw = false;
        if (other.transform.tag == " LowerClaw")
            lowerClaw = false;
        StartCoroutine(waiter());
    }

    public void isgrab()
    {
        Debug.Log("grabbed");
       isGrabbable = true;

    }
    public void isNotGrab()
    {
        StartCoroutine(isGrabWait());

        
    }

    IEnumerator isGrabWait()
    {

        yield return new WaitForSeconds(10);
        isGrabbable = false;
    }
}
