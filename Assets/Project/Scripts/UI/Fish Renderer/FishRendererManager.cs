using UnityEngine;

namespace Project.Scripts.UI.Fish_Renderer
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
            GameObject currentFish = Resources.Load<GameObject>("Prefabs/Fish/" + species);
            Destroy(fishImageObject);
            GameObject fish = Instantiate(currentFish, this.transform);
            fishImageObject = fish;
            fishImageObject.GetComponent<FishImage>().UpdateText(prefix + " " + attribute + " " + species);
        }
    }
}
