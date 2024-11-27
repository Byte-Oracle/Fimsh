using Fishing;
using ui_fish_renderer;
using UnityEngine;

namespace debug.ui
{
    public class FishGenerationDebugUI : MonoBehaviour
    {
        public void GenerateFish()
        {
            Fish fish = FishGenerator.instance.GenerateFish("Fish Pool 1");
            
            FishRendererManager.instance.SetFish(fish.Specie, fish.Prefix, fish.Attribute);
        }
    }
}
