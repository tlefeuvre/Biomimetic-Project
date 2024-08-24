using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> variants = new List<GameObject>();
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
        variants[indexVariant].gameObject.SetActive(false);
        indexVariant++;

        if (indexVariant < variants.Count)
        {
            variants[indexVariant].gameObject.SetActive(true);

        }
        else
        {
            Destroyed();

        }


    }
    public void Destroyed()
    {
        NewExpManager.Instance.AddBrokenTag(this.tag);
        NewExpManager.Instance.RemoveFromList(this.gameObject);


        foreach (GameObject var in variants)
        {
            var.gameObject.SetActive(false);
        }
        NewExpManager.Instance.NewDestroyedFruit();
        Destroy(this.gameObject);
    }

    public void spawnKey(GameObject key)
    {
        GameObject keyObj = Instantiate(key, keySpawner);
        keyObj.transform.localPosition = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Hand")
        {
            Debug.Log("toucher le item !!!!");
            ObjectHit();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Hand")
        {
            Debug.Log("toucher le item !!!!");
            ObjectHit();

        }
    }

}
