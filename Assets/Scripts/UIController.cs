using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Singleton est�tico para asegurar que solo haya una instancia de UIController
    public static UIController instance; // Instancia est�tica de UIController

    private void Awake()
    {
            instance = this; // Asignar la instancia si no existe
    }

    public TMP_Text ammoText, remainingAmmoText; // Texto de munici�n

    public TMP_Text healthText; // Texto de salud

    public GameObject deathScreen; // Pantalla de muerte

    public string mainMenuScene; // Nombre de la escena del men� principal

    public GameObject pauseScreen; // Pantalla de pausa

    public InputActionReference pauseAction; // Acci�n de entrada para pausar el juego


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(pauseAction.action.WasPressedThisFrame())
        {
            PauseUnpause(); // Llamar al m�todo para pausar o reanudar el juego
        }
    }

    public void UpdateAmmoText(int currentAmmo, int remainingAmmo)
    {
        ammoText.text = currentAmmo.ToString(); // Actualizar el texto de munici�n
        remainingAmmoText.text = "/" + remainingAmmo.ToString(); // Actualizar el texto de munici�n restante
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
        // Debug.Log("Restarting level..."); // Imprimir en la consola que se est� reiniciando el nivel
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reiniciar el nivel actual
    }

    public void GoToMainMenu()
    {
       SceneManager.LoadScene(mainMenuScene); // Cargar la escena del men� principal

        Time.timeScale = 1f; // Asegurarse de que el tiempo del juego est� en marcha
    }

    public void QuitGame()
    {
        // Debug.Log("Game is exiting..."); // Imprimir en la consola que el juego est� saliendo
        Application.Quit(); // Salir del juego

        // Si estamos en el editor, detener la ejecuci�n
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void PauseUnpause()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true); // Activar la pantalla de pausa
            Time.timeScale = 0; // Pausar el tiempo del juego
            Cursor.lockState = CursorLockMode.None; // Desbloquear el cursor
        }
        else
        {
            pauseScreen.SetActive(false); // Desactivar la pantalla de pausa
            Time.timeScale = 1f; // Reanudar el tiempo del juego
            Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor
        }
    }
}
