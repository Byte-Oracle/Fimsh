using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]

public class FishGenerator : MonoBehaviour
{
    public static FishGenerator Instance;
    private string _fishingPoolDir = Application.streamingAssetsPath + "/Pools/";
    public List<FishingPool> fishingPools;
    void Start()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(this); 
        }else
        {
            Instance = this;
        }
        LoadFishingPools(_fishingPoolDir);
    }
    
    //Creates a new fishing pool for each fishing pool json
    public void LoadFishingPools(string directory)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(directory);
        foreach (var file in dirInfo.GetFiles("*.json"))
        {
            string json = File.ReadAllText(_fishingPoolDir + "/" + file.Name);
            fishingPools.Add(JsonUtility.FromJson<FishingPool>(json));
        }

        if (Application.isEditor)
        {
            foreach (FishingPool pool in fishingPools)
            {
                Debug.Log("Registed Pool: " + pool.name);
            }
        }
    }
    
    public Fish GenerateFish(string fishingPool)
    {
        //TODO: Redo this method using the new json files
        FishingPool pool = fishingPools.Find(fp => fp.name == fishingPool);;
        if (pool == null)
        {
            Debug.LogError("Pool could not be found: " + fishingPool);
            return null;
        }
        
        Fish fish = new Fish();
        fish.Specie = pool.species[Random.Range(0, pool.species.Count)];
        fish.Prefix1 = pool.prefixes1[Random.Range(0, pool.prefixes1.Count)];
        fish.Prefix2 = pool.prefixes2[Random.Range(0, pool.prefixes2.Count)];

        if (Application.isEditor)
        {
            Debug.Log("Generated Fish: "+ fish.Prefix1 + " " + fish.Prefix2 + " " + fish.Specie);
        }
        
        return fish;
    }
}
