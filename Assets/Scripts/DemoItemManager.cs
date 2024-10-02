using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class DemoItemManager : MonoBehaviour
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
    public ExitTuto exitTuto;




    public GameObject chestManagerChild;
    public GameObject amphoraManagerChild;
    public int indexVariant;

    public Transform keySpawner;
    public GameObject AmphoreGrab;

    private bool isDestroyed = false;

    private bool isOpened = false;
    private bool Exitcollider = false;
    public Transform parentTransform;
    public Volume postProcess;
    private Vignette vg;
    private float velocityTohit = 0.04f;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = this.GetComponent<AudioSource>();
        indexVariant = 0;
        postProcess.profile.TryGet(out vg);
        vg.intensity.value = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator waiter()
    {

        yield return new WaitForSecondsRealtime(0.5f);
        Exitcollider = false;
    }
    public void ObjectHit()
    {
        Exitcollider = true;
        DebugLogs.Instance.NewHit(transform.tag);

       
       

        //if (!isDestroyed)
        //{
        //    NewExpManager.Instance.NewHit();

        //}

        if (indexVariant < variantsMain.Count - 1)
            variantsMain[indexVariant].gameObject.SetActive(false);


        if (variantsTop.Count > 0 && variantsTop.Count - 1 > indexVariant)
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


        if (indexVariant >= variantsMain.Count - 1)
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
        exitTuto.isDestroyed = true;
        //SceneManager.LoadScene("FirstSceneNEW", LoadSceneMode.Single);
        DebugLogs.Instance.NewDestroy(transform.tag);


        Debug.Log("Hello");

        NewExpManager.Instance.AddBrokenTag(this.tag);
        NewExpManager.Instance.RemoveFromList(this.gameObject);

        if (!isOpened && !isDestroyed)
            NewExpManager.Instance.NewDestroyedObject(this.gameObject);
        //Destroy(this.gameObject);
        isDestroyed = true;
    }
    
   

  

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Hand" && !Exitcollider)
        {
            float handSpeed = other.gameObject.GetComponent<HandVelocity>().GetMagnitude();
           
            if (handSpeed > handMagnitudeToExplode)
            {

              
                ObjectHit();
                Debug.Log("boom");

            }

        

        }

       

    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(waiter());
    }

}

