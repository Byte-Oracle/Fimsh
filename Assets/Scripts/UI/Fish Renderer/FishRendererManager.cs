using ui.fish_renderer;
using UnityEngine;

namespace ui_fish_renderer
{
    public class FishRendererManager : MonoBehaviour
    {
        public GameObject fishImageObject;
        
        public static FishRendererManager instance;
        void Start()
        {
            if (instance == null && instance != this)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetFish(string species, string prefix, string attribute)
        {
            GameObject currentFish = Resources.Load<GameObject>("Fish/" + species);
            Destroy(fishImageObject);
            GameObject fish = Instantiate(currentFish, this.transform);
            fishImageObject = fish;
            fishImageObject.GetComponent<FishImage>().UpdateText(prefix + " " + attribute + " " + species);
        }
    }
}
