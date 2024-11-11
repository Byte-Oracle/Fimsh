using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

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
            Debug.Log("Bobber casted");
        }
    }

    public void OnDebugCatchFish(InputAction.CallbackContext context)
    {
        if (context.performed && Application.platform == RuntimePlatform.WindowsEditor)
        {
            Fish fish = FishGenerator.Instance.GenerateFish();
            Debug.Log(fish.name);
            Debug.Log(fish.size);
            Debug.Log(fish.specie);
        }
    }
}
