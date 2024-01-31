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
    public bool isRoundFinished;
    [Header("Fruits")]
    public List<GameObject> fruitsCat01List;
    public List<GameObject> fruitsCat02List;
    public List<GameObject> fruitsCat03List;

    [Header("Fruits positions")]
    public Transform spawnerCat01Pos;
    public Transform spawnerCat02Pos;
    public Transform spawnerCat03Pos;

    public List<GameObject> spawnedFruits = new List<GameObject>();

    [Header("New")]
    public List <GameObject> fruitsList = new List<GameObject>();
    public List <Transform> spawnersPosList = new List<Transform>();
    public GameObject placeHolder;
    public List<Animator> tableAnimator = new List<Animator>();
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
        isRoundFinished = false;
        currentRound = 0;
        placeHolder.SetActive(false);
        SpawnNewRound();

        //NewRound();
    }

    public void CallNewRound()
    {
        
        StartCoroutine("WaitForNewRound");

    }

    IEnumerator WaitForNewRound()
    {
        //gravity
        foreach (GameObject fruit in spawnedFruits)
        {
            fruit.GetComponent<FruitManager>().ActivateGravity();
        }


        //open animation
        yield return new WaitForSeconds(.1f);
        foreach (Animator animator in tableAnimator)
        {
            Debug.Log("animator set bool true");
            animator.SetBool("IsOpen", true);
        }

        //close animation
        yield return new WaitForSeconds(.2f);
        foreach (Animator animator in tableAnimator)
        {
            Debug.Log("animator set bool false");

            animator.SetBool("IsOpen", false);
        }

        //NewRound();
        yield return new WaitForSeconds(timeToWait);
        SpawnNewRound();
    }

    public void NewRound()
    {
        Debug.Log("NewRound");

        foreach ( GameObject fruit in spawnedFruits)
        {
            //fruit.SetActive(false);

            Destroy (fruit.gameObject);
        }

        spawnedFruits.Clear();
        if(currentRound !=0)
            Measures.Instance.NewTimer(numbOfRounds);

        if (currentRound >= numbOfRounds)
        {
            SaveUserData.Instance.WriteNewUserData();
            return;
        }


        GameObject cat1Fruit = fruitsCat01List[Random.Range(0, fruitsCat01List.Count)];
        GameObject newFruit1 = Instantiate(cat1Fruit, spawnerCat01Pos.position, Quaternion.identity);
        spawnedFruits.Add(newFruit1);

        GameObject cat2Fruit = fruitsCat02List[Random.Range(0, fruitsCat02List.Count )];
        GameObject newFruit2 = Instantiate(cat2Fruit, spawnerCat02Pos.position, Quaternion.identity);
        spawnedFruits.Add(newFruit2);

        GameObject cat3Fruit = fruitsCat03List[Random.Range(0, fruitsCat03List.Count )];
        GameObject newFruit3 = Instantiate(cat3Fruit, spawnerCat03Pos.position, Quaternion.identity);
        spawnedFruits.Add(newFruit3);

        currentRound += 1;
        Debug.Log("endnewround");


    }
    public void SpawnNewRound()
    {

        foreach (GameObject fruit in spawnedFruits)
        {
            //fruit.SetActive(false);

            Destroy(fruit.gameObject);
        }

        spawnedFruits.Clear();
        if (currentRound != 0)
            Measures.Instance.NewTimer(numbOfRounds);

        if (currentRound >= numbOfRounds)
        {
            SaveUserData.Instance.WriteNewUserData();
            return;
        }

        List<GameObject> copyFruitsList = new List<GameObject>(fruitsList);
        foreach (Transform spawner in spawnersPosList)
        {
            int rand = Random.Range(0, copyFruitsList.Count);
            GameObject newFruit = Instantiate(copyFruitsList[rand], spawner.position, Quaternion.identity);
            spawnedFruits.Add(newFruit);
            copyFruitsList.RemoveAt(rand);
        }
        currentRound += 1;
        FruitsSpawner.Instance.isRoundFinished = false;
        FruitsSpawner.Instance.SetPlaceHolder(false);

        Debug.Log("end new spawn round");
    }
    public void SetPlaceHolder(bool isActive)
    {
        placeHolder.SetActive(isActive);
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