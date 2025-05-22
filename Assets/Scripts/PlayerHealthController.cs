using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    // Singleton statico para asegurar que solo haya una instancia de PlayerHealthController
    public static PlayerHealthController instance; // Instancia est�tica de PlayerHealthController
   
    private void Awake()
    {
       instance = this; // Asignar la instancia si no existe
    }

    // Variables salud
    public float maxHealth = 100f; // M�xima salud del jugador
    private float currentHealth; // Salud actual del jugador

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth; // Inicializar la salud actual al m�ximo

        UIController.instance.UpdateHealthText(currentHealth); // Actualizar el texto de salud en la interfaz de usuario
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; // Reducir la salud actual por la cantidad de da�o

        if (currentHealth <= 0)
        {
            currentHealth = 0; // Asegurarse de que la salud no sea menor que cero
            
            // Debug.Log ("Player is dead!"); // Imprimir en la consola que el jugador est� muerto

            PlayerController.instance.isDead = true; // Marcar al jugador como muerto

            UIController.instance.ShowDeathScreen(); // Llamar al m�todo para mostrar la pantalla de muerte
        }

        UIController.instance.UpdateHealthText(currentHealth); // Actualizar el texto de salud en la interfaz de usuario
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount; // Aumentar la salud actual por la cantidad de curaci�n

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Asegurarse de que la salud no exceda el m�ximo
        }

        UIController.instance.UpdateHealthText(currentHealth); // Actualizar el texto de salud en la interfaz de usuario
    }

}
