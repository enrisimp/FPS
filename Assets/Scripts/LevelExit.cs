using UnityEngine;
using UnityEngine.SceneManagement; // Importar el espacio de nombres para la gesti�n de escenas

public class LevelExit : MonoBehaviour
{
    public string sceneToLoad; // Nombre de la escena a cargar al salir

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // Verificar si el objeto que colisiona tiene la etiqueta "Player"
        {
            SceneManager.LoadScene(sceneToLoad); // Llamar al m�todo para cargar la escena especificada
        }
    }
}
