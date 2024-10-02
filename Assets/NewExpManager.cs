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
    public GameObject finishIndication;

    private bool expIsFinished;
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
        expIsFinished = false;
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

    public void NewDestroyedObject(GameObject usedObject)
    {
        nbDestroyedObjects++;
        CheckObjectsStates(usedObject);


    }
    public void NewOpenedObject(GameObject usedObject)
    {
  

        nbOpenedObjects++;
        CheckObjectsStates(usedObject);

    }
    public void CheckObjectsStates(GameObject usedObject)
    {
        if (!expIsFinished && nbDestroyedObjects + nbOpenedObjects >= 10)
        {

            Debug.Log("spawn key");
            usedObject.GetComponent<ItemManager>().spawnKey(keyPrefab);
            
            //foreach (GameObject obj in allObjects)
            //{
            //    obj.GetComponent<ItemManager>().spawnKey(keyPrefab);
            //}
            ExpFinished();

        }
    }
    public void NewHit()
    {
        nbHits++;
    }

    public void ExpFinished()
    {
        if (!expIsFinished)
        {
            Debug.Log("write excel");
            expIsFinished =true;
            finishIndication.SetActive(true);
            PlayerPrefs.SetInt("NumberHits", nbHits);
            PlayerPrefs.SetInt("NumberOpened", nbOpenedObjects);
            SaveUserData.Instance.WriteNewUserData();
        }
        else
        {
            finishIndication.SetActive(true);
        }
    }

    public void KeyGrabbed()
    {
        finishIndication.SetActive(true);

    }
}
