using Project.Scripts.Fishing;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Player
{
    public class PlayerHandler : MonoBehaviour
    {
        public Transform castLocation;
    
        public GameObject bobber;

        public float bobberCastSpeed = 1.5f;
    
        public void OnCast(InputAction.CallbackContext context)
        {
            if (context.performed && bobber != null)
            {
                GameObject spawned = Instantiate(bobber, castLocation.transform.position, Quaternion.identity);
                spawned.GetComponent<Rigidbody2D>().AddForce(new Vector2(bobberCastSpeed, 0) * 100);
                UnityEngine.Debug.Log("Bobber casted");
            }
        }

        public void OnDebugCatchFish(InputAction.CallbackContext context)
        {
            if (context.performed && Application.platform == RuntimePlatform.WindowsEditor)
            {
                FishGenerator.Instance.GenerateFish("Fish Pool 1");
            }
        }
    }
}

