using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Util;

namespace Fishing {
    public class FishGenerator : MonoBehaviour
    {
        public static FishGenerator instance;
        private readonly string _fishingPoolDir = Application.streamingAssetsPath + "/Pools/";
        
        public List<FishingPool> fishingPools;
        private WeightedTableUtil _tableUtil;
        private RollResult _rollResult;
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
        
        public Fish GenerateFish(string fishingPool)
        {
            var pool = fishingPools.Find(fp => fp.poolname == fishingPool);
            if (pool == null)
            {
                Debug.LogError("Pool could not be found: " + fishingPool);
                return null;
            }
            
            #region Table Creation and Fish Construction
            //Creates tables for various fish attributes, rolls them, and then creates a fish along the way.
            var fish = new Fish();
            _tableUtil = new WeightedTableUtil();
            
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
            
            _rollResult = _tableUtil.RollTable(specieTable);
            fish.Specie = _rollResult.Name;
            fish.Value += _rollResult.Value;
            
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
            _rollResult = _tableUtil.RollTable(prefixTable);
            fish.Prefix = _rollResult.Name;
            fish.Value += _rollResult.Value;
            
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
            _rollResult = _tableUtil.RollTable(attributeTable);
            fish.Attribute = _rollResult.Name;
            fish.Value += _rollResult.Value;
            

            if (Application.isEditor)
            {
                Debug.Log("Generated Fish: "+ fish.Prefix + " " + fish.Attribute + " " + fish.Specie);
                Debug.Log("Fish Value is: " + fish.Value);
            }
            
            return fish;
            #endregion
        }
    }
}
