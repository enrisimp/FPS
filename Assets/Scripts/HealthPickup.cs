using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthAmount = 25f; // Cantidad de salud a restaurar al recoger el objeto

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.Heal(healthAmount); // Llamar al m�todo Heal() en PlayerHealthController

            Destroy(gameObject); // Destruir el objeto de recogida de salud
        }
    }
}
