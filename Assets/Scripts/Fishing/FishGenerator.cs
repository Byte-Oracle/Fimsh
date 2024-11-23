using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fishing {
    public class FishGenerator : MonoBehaviour
    {
        public static FishGenerator instance;
        private string _fishingPoolDir = Application.streamingAssetsPath + "/Pools/";
        public List<FishingPool> fishingPools;
        void Start()
        {
            
            if (instance != null && instance != this)
            {
                Destroy(this); 
            }else
            {
                instance = this;
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
                Debug.Log(json);
                fishingPools.Add(JsonUtility.FromJson<FishingPool>(json));
            }

            if (Application.isEditor)
            {
                foreach (FishingPool pool in fishingPools)
                {
                    Debug.Log("Registed Pool: " + pool.poolname);
                }
            }
        }
        
        public Fish GenerateFish(string fishingPool)
        {
            //TODO: Implement weight system to make certain prefixes, attributes, and species rarer
            var pool = fishingPools.Find(fp => fp.poolname == fishingPool);;
            if (pool == null)
            {
                Debug.LogError("Pool could not be found: " + fishingPool);
                return null;
            }
            
            var fish = new Fish();
            var prefix = pool.prefix[Random.Range(0, pool.prefix.Count)];
            var attribute = pool.attribute[Random.Range(0, pool.attribute.Count)];
            var specie = pool.specie[Random.Range(0, pool.specie.Count)];
            
            fish.Specie = specie.name;
            fish.Value += specie.value;
            
            fish.Prefix = prefix.name;
            fish.Value += prefix.value;
            
            fish.Attribute = attribute.name;
            fish.Value += attribute.value;

            if (Application.isEditor)
            {
                Debug.Log("Generated Fish: "+ fish.Prefix + " " + fish.Attribute + " " + fish.Specie);
                Debug.Log("Fish Value is: " + fish.Value);
            }
            
            return fish;
        }
    }
}

