using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> variantsMain = new List<GameObject>();
    public List<GameObject> variantsTop = new List<GameObject>();
    public float handMagnitudeToExplode = 4;

    public GameObject chestManagerChild;
    private int indexVariant;

    public Transform keySpawner;
    // Start is called before the first frame update
    void Start()
    {
        indexVariant = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObjectHit()
    {
        NewExpManager.Instance.NewHit();

        variantsMain[indexVariant].gameObject.SetActive(false);
        if(variantsTop.Count > 0 )
        {
            variantsTop[indexVariant].gameObject.SetActive(false);

        }
        indexVariant++;

        if (indexVariant < variantsMain.Count)
        {
            variantsMain[indexVariant].gameObject.SetActive(true);
            variantsTop[indexVariant].gameObject.SetActive(true);

        }
        else
        {
            Destroyed();

        }


    }
    public void Destroyed()
    {


        foreach (GameObject var in variantsMain)
        {
            var.gameObject.SetActive(false);
        }
        foreach (GameObject var in variantsTop)
        {
            var.gameObject.SetActive(false);
        }


        NewExpManager.Instance.AddBrokenTag(this.tag);
        NewExpManager.Instance.RemoveFromList(this.gameObject);

        NewExpManager.Instance.NewDestroyedObject();
        Destroy(this.gameObject);
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
