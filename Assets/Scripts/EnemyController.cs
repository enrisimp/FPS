using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private PlayerController player; // Referencia al objeto del jugador

    public float moveSpeed; // Velocidad de movimiento del enemigo

    public Rigidbody theRB; // Rigidbody reference

    public float chaseRange = 15f, stopCloseRange = 4f; // Rango de persecución y rango de parada del enemigo

    private float strafeAmount; // Cantidad de movimiento lateral aleatorio del enemigo

    public Animator anim; // Animator reference

    public Transform[] patrolPoints; // Puntos de patrullaje del enemigo
    private int currentPatrolPoint; // Índice del punto de patrullaje actual del enemigo
    public Transform pointsHolder; // Objeto padre que contiene los puntos de patrullaje
    
    public float pointWaitTime = 3f; // Tiempo de espera en cada punto de patrullaje
    public float waitCounter; // Contador de espera en cada punto de patrullaje


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerController>(); // Encontrar el objeto del jugador en la escena

        strafeAmount = Random.Range(-.75f, .75f); // Cantidad de movimiento lateral aleatorio del enemigo

        pointsHolder.SetParent(null); // Obtener el objeto padre que contiene los puntos de patrullaje

        waitCounter = Random.Range(.75f, 1.25f) * pointWaitTime; // Inicializar el contador de espera en un rango aleatorio entre 0.75 y 1.25 segundos
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = theRB.linearVelocity.y; // Almacenar la velocidad en el eje Y del Rigidbody

        float distance = Vector3.Distance(transform.position, player.transform.position); // Calcular la distancia entre el enemigo y el jugador

        if (distance < chaseRange)
        {
            //transform.LookAt(player.transform.position);
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z)); // Mirar al jugador en el eje Y

            if (distance > stopCloseRange)
            {
                theRB.linearVelocity = (transform.forward + (transform.right * strafeAmount))* moveSpeed; // Movimiento del enemigo hacia el jugador con movimiento lateral aleatorio

                anim.SetBool("moving", true); // Hacer que la animación de caminar se reproduzca cuando el enemigo se mueva
            }
            else
            {
                theRB.linearVelocity = Vector3.zero; // Detener el movimiento del enemigo cuando está cerca del jugador

                anim.SetBool("moving", false); // Hacer que la animación de caminar se detenga cuando el enemigo no se mueva
            }
            
        }
        else
        {
            if (patrolPoints.Length > 0) // Si hay puntos de patrullage definidos
            {
                if(Vector3.Distance(transform.position, new Vector3(patrolPoints[currentPatrolPoint].position.x, transform.position.y, patrolPoints[currentPatrolPoint].position.z)) < .25f) // Si el enemigo está cerca del punto de patrullaje actual
                {
                    // Reducir el contador y detener el movimiento
                    waitCounter -= Time.deltaTime; // Reducir el contador de espera
                    theRB.linearVelocity = Vector3.zero; // Detener el movimiento del enemigo
                    anim.SetBool("moving", false); // Hacer que la animación de caminar se detenga cuando el enemigo no se mueva

                    if (waitCounter <= 0) // Si el contador de espera ha llegado a cero
                    {
                        // Cambiar al siguiente punto de patrullaje
                        currentPatrolPoint++; // Cambiar al siguiente punto de patrullaje
                        if (currentPatrolPoint >= patrolPoints.Length) // Si se ha llegado al último punto de patrullaje
                        {
                            currentPatrolPoint = 0; // Volver al primer punto de patrullaje
                        }

                        waitCounter = Random.Range(.75f, 1.25f) * pointWaitTime; // Reiniciar el contador de espera en un rango aleatorio entre 0.75 y 1.25 segundos
                    }
                    
                }
                else
                {
                    transform.LookAt(new Vector3(patrolPoints[currentPatrolPoint].position.x, transform.position.y, patrolPoints[currentPatrolPoint].position.z)); // Mirar al siguiente punto de patrullaje

                    theRB.linearVelocity = transform.forward * moveSpeed; // Movimiento del enemigo hacia el siguiente punto de patrullaje

                    anim.SetBool("moving", true); // Hacer que la animación de caminar se reproduzca cuando el enemigo se mueva     
                }
            }
            else
            {
                theRB.linearVelocity = Vector3.zero; // Detener el movimiento del enemigo cuando está fuera del rango de persecución

                anim.SetBool("moving", false); // Hacer que la animación de caminar se detenga cuando el enemigo no se mueva
            }
            
        }

        theRB.linearVelocity = new Vector3(theRB.linearVelocity.x, yStore, theRB.linearVelocity.z); // Mantener la velocidad en el eje Y
    }
}
