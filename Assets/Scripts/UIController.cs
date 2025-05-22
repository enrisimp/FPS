using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Singleton estático para asegurar que solo haya una instancia de UIController
    public static UIController instance; // Instancia estática de UIController

    private void Awake()
    {
            instance = this; // Asignar la instancia si no existe
    }

    public TMP_Text ammoText, remainingAmmoText; // Texto de munición

    public TMP_Text healthText; // Texto de salud

    public GameObject deathScreen; // Pantalla de muerte

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateAmmoText(int currentAmmo, int remainingAmmo)
    {
        ammoText.text = currentAmmo.ToString(); // Actualizar el texto de munición
        remainingAmmoText.text = "/" + remainingAmmo.ToString(); // Actualizar el texto de munición restante
    }

    public void UpdateHealthText(float currentHealth)
    {
        healthText.text = "Health: " + Mathf.RoundToInt(currentHealth); // Actualizar el texto de salud
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true); // Activar la pantalla de muerte
        // Time.timeScale = 0; // Pausar el tiempo del juego
        Cursor.lockState = CursorLockMode.None; // Desbloquear el cursor
    }

    public void RestartLevel()
    {
        // Debug.Log("Restarting level..."); // Imprimir en la consola que se está reiniciando el nivel
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reiniciar el nivel actual
    }
}
