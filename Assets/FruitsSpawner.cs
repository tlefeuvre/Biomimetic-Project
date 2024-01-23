using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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

    [Header("Fruits")]
    public List<GameObject> fruitsCat01List;
    public List<GameObject> fruitsCat02List;
    public List<GameObject> fruitsCat03List;

    [Header("Fruits positions")]
    public Transform spawnerCat01Pos;
    public Transform spawnerCat02Pos;
    public Transform spawnerCat03Pos;

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
        foreach (Fruit fruit in fruits)
        {
            Instantiate(fruit.prefab, fruit.spawnerPos.position, Quaternion.identity);
        }
    }
    public void SpawnNewFruit(string destroyedFruitTag)
    {
        Debug.Log("respawn");
        foreach (Fruit fruit in fruits)
        {
            if(destroyedFruitTag == fruit.fruitTag)
            {
                Instantiate(fruit.prefab, fruit.spawnerPos.position,Quaternion.identity);
            }
        } 


    }

    public void NewRound()
    {
        GameObject cat1Fruit = fruitsCat01[Random.Range(0,fruitsCat01.Count-1)];
        GameObject fruit = Instantiate(cat1Fruit, fruit.spawnerPos.position, Quaternion.identity);

    }

}


