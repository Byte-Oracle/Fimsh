using System;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using Random = UnityEngine.Random;


public class FishGenerator : MonoBehaviour
{
    public static FishGenerator Instance;
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this); 
        }else
        {
            Instance = this;
        }
    }
    
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
