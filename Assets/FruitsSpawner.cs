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

    public List<Fruit> fruits;

    private static FruitsSpawner instance = null;
    public static FruitsSpawner Instance => instance;

    public List<GameObject> fruitsCat01;
    public List<GameObject> fruitsCat02;
    public List<GameObject> fruitsCat03;
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

    }

}


