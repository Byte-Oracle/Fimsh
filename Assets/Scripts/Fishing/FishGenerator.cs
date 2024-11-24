using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public struct WeightedItem
{
    public string Name;
    public int Weight;
    public int Value;
}

public struct WeightedTable
{
    public int TotalWeight;
    public List<WeightedItem> Items;
}

public struct RollResult
{
    public string Specie;
    public string Prefix;
    public string Attribute;
    public int Value;
}

namespace Fishing {
    public class FishGenerator : MonoBehaviour
    {
        public static FishGenerator instance;
        private readonly string _fishingPoolDir = Application.streamingAssetsPath + "/Pools/";
        public List<FishingPool> fishingPools;
        private void Start()
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
        
        #region LoadFishingPoolsFunction
        //Creates a new fishing pool for each fishing pool json
        private void LoadFishingPools(string directory)
        {
            var dirInfo = new DirectoryInfo(directory);
            foreach (var file in dirInfo.GetFiles("*.json"))
            {
                var json = File.ReadAllText(_fishingPoolDir + "/" + file.Name);
                Debug.Log(json);
                fishingPools.Add(JsonUtility.FromJson<FishingPool>(json));
            }

            if (Application.isEditor)
            {
                foreach (FishingPool pool in fishingPools)
                {
                    Debug.Log("Registered Pool: " + pool.poolname);
                }
            }
        }
        #endregion

        #region GenerateFish Function
        public Fish GenerateFish(string fishingPool)
        {
            var pool = fishingPools.Find(fp => fp.poolname == fishingPool);
            if (pool == null)
            {
                Debug.LogError("Pool could not be found: " + fishingPool);
                return null;
            }
            
            #region Table Creation
            //Creates tables for various fish attributes and rolls them returning the result
            var specieTable = new WeightedTable()
            {
                TotalWeight = 0,
                Items = new List<WeightedItem>()
            };
            foreach(var s in pool.specie)
            {
                WeightedItem wi;
                specieTable.TotalWeight += s.weight;
                
                wi.Name = s.name;
                wi.Weight = s.weight;
                wi.Value = s.value;
                specieTable.Items.Add(wi);
            }

            var prefixTable = new WeightedTable()
            {
                TotalWeight = 0,
                Items = new List<WeightedItem>()
            };
            foreach(var p in pool.prefix)
            {
                WeightedItem wi;
                prefixTable.TotalWeight += p.weight;
                
                wi.Name = p.name;
                wi.Weight = p.weight;
                wi.Value = p.value;
                prefixTable.Items.Add(wi);
            }

            var attributeTable = new WeightedTable()
            {
                TotalWeight = 0,
                Items = new List<WeightedItem>()
            };
            foreach(var a in pool.attribute)
            {
                WeightedItem wi;
                attributeTable.TotalWeight += a.weight;
                
                wi.Name = a.name;
                wi.Weight = a.weight;
                wi.Value = a.value;
                attributeTable.Items.Add(wi);
            }

            var result = RollTable(specieTable, prefixTable, attributeTable);
            #endregion
            
            var fish = new Fish();
            var prefix = result.Prefix;
            var attribute = result.Attribute;
            var specie = result.Specie;
            var value = result.Value;

            fish.Specie = specie;
            fish.Prefix = prefix;
            fish.Attribute = attribute;
            fish.Value += value;

            if (Application.isEditor)
            {
                Debug.Log("Generated Fish: "+ fish.Prefix + " " + fish.Attribute + " " + fish.Specie);
                Debug.Log("Fish Value is: " + fish.Value);
            }
            
            return fish;
        }
        #endregion

        #region RollResult Function
        private RollResult RollTable(WeightedTable specieTable, WeightedTable prefixTable, WeightedTable attributeTable)
        {
            //TODO: This feels unholy and I think there is a much better way to make this only require on input but 
            //That is a later issue :')
            
            var result = new RollResult();
            
            var specieRandom = Random.Range(0, specieTable.TotalWeight);
            var prefixRandom = Random.Range(0, prefixTable.TotalWeight);
            var attributeRandom = Random.Range(0, attributeTable.TotalWeight);

            for (var i = 0; i < specieTable.Items.Count; i++)
            {
                if (specieRandom <= specieTable.Items[i].Weight)
                {
                    result.Specie = specieTable.Items[i].Name;
                    result.Value += specieTable.Items[i].Value;
                    break;
                }
                else specieRandom -= specieTable.Items[i].Weight;
            }
            
            for (var i = 0; i < prefixTable.Items.Count; i++)
            {
                if (prefixRandom <= prefixTable.Items[i].Weight)
                {
                    result.Prefix = prefixTable.Items[i].Name;
                    result.Value += prefixTable.Items[i].Value;
                    break;
                }
                else prefixRandom -= prefixTable.Items[i].Weight;
            }
            
            for (var i = 0; i < attributeTable.Items.Count; i++)
            {
                if (attributeRandom <= attributeTable.Items[i].Weight)
                {
                    result.Attribute = attributeTable.Items[i].Name;
                    result.Value += attributeTable.Items[i].Value;
                    break;
                }
                else attributeRandom -= attributeTable.Items[i].Weight;
            }
            
            return result;
        }
        #endregion
    }
}

