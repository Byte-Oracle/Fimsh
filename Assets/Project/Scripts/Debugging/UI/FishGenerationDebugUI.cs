using Project.Scripts.Fishing;
using Project.Scripts.UI.Fish_Renderer;
using UnityEngine;

namespace Project.Scripts.Debugging.UI
{
    public class FishGenerationDebugUI : MonoBehaviour
    {
        public void GenerateFish()
        {
            Fish fish = FishGenerator.Instance.GenerateFish("Spring Breeze");
            
            FishRendererManager.instance.SetFish(fish.Specie, fish.Prefix, fish.Attribute);
        }
    }
}
