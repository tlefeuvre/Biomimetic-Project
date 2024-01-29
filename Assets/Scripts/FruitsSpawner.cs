using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fruit
{
    public string fruitTag;
    public Transform spawnerPos;
    public GameObject prefab;
}
public class FruitsSpawner : MonoBehaviour
{

    //public List<Fruit> fruits;

    private static FruitsSpawner instance = null;
    public static FruitsSpawner Instance => instance;

    [Header("Parameters")]
    public float timeToWait;
    public int numbOfRounds;
    public int currentRound;
    [Header("Fruits")]
    public List<GameObject> fruitsList;
    /*public List<GameObject> fruitsCat01List;
    /*public List<GameObject> fruitsCat02List;
    public List<GameObject> fruitsCat03List;*/

    [Header("Fruits positions")]
    public Transform spawnerPos;
    /*public Transform spawnerCat01Pos;
    /*public Transform spawnerCat02Pos;
    public Transform spawnerCat03Pos;*/

    public List<GameObject> spawnedFruits;
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

    public void Start()
    {
        currentRound = 0;

        NewRound();
    }

    public void CallNewRound()
    {
        StartCoroutine("WaitForNewRound");
    }
    
    IEnumerator WaitForNewRound()
    {
        yield return new WaitForSeconds(timeToWait);
        NewRound();
    }

    public void NewRound()
    {
        Debug.Log("NewRound");

        foreach ( GameObject oldFruit in spawnedFruits)
        {
            //fruit.SetActive(false);

            Destroy (oldFruit.gameObject);
        }

        spawnedFruits.Clear();
        if(currentRound !=0)
            Measures.Instance.NewTimer(numbOfRounds);

        if (currentRound >= numbOfRounds)
        {
            SaveUserData.Instance.WriteNewUserData();
            return;
        }
        GameObject fruit = fruitsList[Random.Range(0, fruitsList.Count)];
        GameObject newFruit = Instantiate(fruit, spawnerPos.position, Quaternion.identity);
        spawnedFruits.Add(newFruit);

        /*GameObject cat1Fruit = fruitsCat01List[Random.Range(0, fruitsCat01List.Count)];
        GameObject newFruit1 = Instantiate(cat1Fruit, spawnerCat01Pos.position, Quaternion.identity);
        spawnedFruits.Add(newFruit1);

        GameObject cat2Fruit = fruitsCat02List[Random.Range(0, fruitsCat02List.Count )];
        GameObject newFruit2 = Instantiate(cat2Fruit, spawnerCat02Pos.position, Quaternion.identity);
        spawnedFruits.Add(newFruit2);

        GameObject cat3Fruit = fruitsCat03List[Random.Range(0, fruitsCat03List.Count )];
        GameObject newFruit3 = Instantiate(cat3Fruit, spawnerCat03Pos.position, Quaternion.identity);
        spawnedFruits.Add(newFruit3);*/

        currentRound += 1;
        Debug.Log("endnewround");


    }

}


/*public void SpawnNewFruit(string destroyedFruitTag)
    {
        Debug.Log("respawn");
        foreach (Fruit fruit in fruits)
        {
            if(destroyedFruitTag == fruit.fruitTag)
            {
                Instantiate(fruit.prefab, fruit.spawnerPos.position,Quaternion.identity);
            }
        } 


    }*/

/*foreach (Fruit fruit in fruits)
    {
        Instantiate(fruit.prefab, fruit.spawnerPos.position, Quaternion.identity);
    }*/