using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NewExpManager : MonoBehaviour
{

    private static NewExpManager instance = null;
    public static NewExpManager Instance => instance;


    public int nbDestroyedObjects = 0;
    public int nbOpenedObjects = 0;
    public int nbHits = 0;
    public List<string> brokeOpenOrderTags = new List<string>();
    public List<GameObject> allObjects = new List<GameObject>();

    public GameObject keyPrefab;
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
    }

    void Update()
    {
        
    }
    public void RemoveFromList(GameObject obj)
    {
        allObjects.Remove(obj);
    }

    public void AddBrokenTag(string tag)
    {
        brokeOpenOrderTags.Add(tag);
    }

    public void NewDestroyedObject()
    {
        nbDestroyedObjects++;
        CheckObjectsStates();


    }
    public void NewOpenedObject()
    {
        Debug.Log("newexpmanager");

        nbOpenedObjects++;
        CheckObjectsStates();

    }
    public void CheckObjectsStates()
    {
        if (nbDestroyedObjects + nbOpenedObjects >= 5)
        {
            Debug.Log("spawn key");
            foreach (GameObject obj in allObjects)
            {
                obj.GetComponent<ItemManager>().spawnKey(keyPrefab);
            }

        }
    }
    public void NewHit()
    {
        nbHits++;
    }

    public void ExpFinished()
    {

    }
}
