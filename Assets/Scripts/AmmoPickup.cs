using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindFirstObjectByType<WeaponsController>().GetAmmo(); // Llamar al método GetAmmo() en WeaponsController

            Destroy(gameObject);
        }
    }
}
