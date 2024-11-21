using System;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;


public class FishGenerator : MonoBehaviour
{
    public static FishGenerator Instance;
    public string fishJson = File.ReadAllText(Application.streamingAssetsPath + "/Fish.json");
    public FishingPool fishingPool;
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this); 
        }else
        {
            Instance = this;
        }
        LoadFish(fishingPool, fishJson);
    }

    public void LoadFish(FishingPool pool, String json)
    {
        JsonUtility.FromJsonOverwrite(json, pool);
    }
    
    
    //TODO: Replace this with data from the json file instead of the Fish class, and delete the fish class
    public Fish GenerateFish()
    {
        Fish fish = new Fish();

        int chosenFish = Random.Range(0, Enum.GetValues((typeof(Fish.Species))).Length);
        fish.specie = (Fish.Species)chosenFish;
        fish.name = fish.specie.ToString();
        fish.size = Random.Range(0, 100);
        
        return fish;
    }
}
