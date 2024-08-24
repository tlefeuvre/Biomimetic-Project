using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewExpManager : MonoBehaviour
{

    private static NewExpManager instance = null;
    public static NewExpManager Instance => instance;

    public int nbDestroyedFruit = 0;
    public List<string> brokeOrderTags = new List<string>();
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
        brokeOrderTags.Add(tag);
    }

    public void NewDestroyedFruit()
    {
        nbDestroyedFruit++;
        if(nbDestroyedFruit >= 5)
        {
            Debug.Log("spawn key");
            foreach(GameObject obj in allObjects)
            {
                obj.GetComponent<ItemManager>().spawnKey(keyPrefab);
            }

        }

    }

    public void ExpFinished()
    {

    }
}
