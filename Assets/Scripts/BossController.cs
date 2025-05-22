using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform[] ammoPoints; // Array de puntos de municiones

    public GameObject ammoPickup; // Prefab de Ammo

    public float ammoSpawnTime; // Tiempo de espera entre cada munici�n
    private float ammoCounter; // Contador de tiempo entre municiones

    public float checkInterval; // Intervalo de tiempo para verificar el estado del boss
    private float checkCounter; // Contador de tiempo para verificar el estado del boss

    public GameObject levelExit; // Referencia al objeto de salida del nivel

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ammoCounter = ammoSpawnTime; // Inicializa el contador de municiones
    }

    // Update is called once per frame
    void Update()
    {
        ammoCounter -= Time.deltaTime; // Resta el tiempo transcurrido al contador
        if (ammoCounter <= 0) // Si el contador llega a cero
        {
            ammoCounter = ammoSpawnTime; // Reinicia el contador
            //SpawnAmmo(); // Llama a la funci�n para generar munici�n
            Instantiate(ammoPickup, ammoPoints[Random.Range(0, ammoPoints.Length)].position, Quaternion.identity); // Genera munici�n en un punto aleatorio el rango maximo excluido
            
        }
        checkCounter -= Time.deltaTime;
        // Resta el tiempo transcurrido al contador de verificaci�n
        if (checkCounter <= 0) // Si el contador de verificaci�n llega a cero
        {
            checkCounter = checkInterval; // Reinicia el contador de verificaci�n
            if (FindFirstObjectByType<EnemyController>() == null) // Busca el primer objeto de tipo EnemyController para ve si hay algun enemigo
            {
                Debug.Log("El jefe ha sido derrotado"); // Si no hay enemigos, muestra un mensaje en la consola

                // Aqu� puedes agregar la l�gica para manejar la derrota del jefe
                gameObject.SetActive(false); // Desactiva el objeto del jefe
                levelExit.SetActive(true); // Activa el objeto de salida del nivel
            }
                
        }
    }
}
