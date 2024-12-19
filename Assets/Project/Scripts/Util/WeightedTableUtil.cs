using System.Collections.Generic;
using UnityEngine;

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
    public string Name;
    public int Value;
}

namespace Project.Scripts.Util
{
    public class WeightedTableUtil
    {
        public RollResult RollTable(WeightedTable table)
        {
            var result = new RollResult();

            var randomRoll = Random.Range(0, table.TotalWeight);

            for (var i = 0; i < table.Items.Count; i++)
            {
                if (randomRoll <= table.Items[i].Weight)
                {
                    result.Name = table.Items[i].Name;
                    result.Value = table.Items[i].Value;
                    break;
                }
                else randomRoll -= table.Items[i].Weight;
            }
            return result;
        }
    }
}

