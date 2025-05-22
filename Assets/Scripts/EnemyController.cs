using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private PlayerController player; // Referencia al objeto del jugador

    public float moveSpeed; // Velocidad de movimiento del enemigo

    public Rigidbody theRB; // Rigidbody reference

    public Animator anim; // Animator reference

    // Persecucion
    public float chaseRange = 15f, stopCloseRange = 4f; // Rango de persecuci�n y rango de parada del enemigo
    
    // Patrullaje
    private float strafeAmount; // Cantidad de movimiento lateral aleatorio del enemigo
    public Transform[] patrolPoints; // Puntos de patrullaje del enemigo
    private int currentPatrolPoint; // �ndice del punto de patrullaje actual del enemigo
    public Transform pointsHolder; // Objeto padre que contiene los puntos de patrullaje
    public float pointWaitTime = 3f; // Tiempo de espera en cada punto de patrullaje
    public float waitCounter; // Contador de espera en cada punto de patrullaje

    // Da�o
    private bool isDead; // Indica si el enemigo est� muerto
    public float currentHealth = 25f; // Vida restante
    public float waitToDisappear = 4f; // Tiempo de espera para desaparecer despu�s de morir

    // Balas
    public Transform shootPoint; // Punto de disparo del enemigo
    public EnemyProjectile projectile; // Prefab del proyectil del enemigo
    public float timeBetweenShots = 1f; // Tiempo entre disparos
    private float shotCounter; // Contador de disparos
    public float shotDamage; // Da�o del proyectil


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerController>(); // Encontrar el objeto del jugador en la escena

        // Patrullaje
        strafeAmount = Random.Range(-.75f, .75f); // Cantidad de movimiento lateral aleatorio del enemigo
        pointsHolder.SetParent(null); // Obtener el objeto padre que contiene los puntos de patrullaje
        waitCounter = Random.Range(.75f, 1.25f) * pointWaitTime; // Inicializar el contador de espera en un rango aleatorio entre 0.75 y 1.25 segundos

        shotCounter = timeBetweenShots; // Inicializar el contador de disparos

        anim.SetTrigger("shooting"); // Iniciar la animaci�n de disparo
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == true)
        {
            waitToDisappear -= Time.deltaTime; // Reducir el contador de espera para desaparecer

            if(waitToDisappear <= 0) // Si el contador ha llegado a cero
            {
                // se reduce la escala del enemigo
                transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime); // Hacer que el enemigo desaparezca lentamente

                if(transform.localScale.x <= .1f) // Si la escala del enemigo es menor o igual a 0.1
                {
                    Destroy(gameObject); // Destruir el objeto del enemigo
                    Destroy(pointsHolder.gameObject); // Destruir el objeto padre que contiene los puntos de patrullaje
                }
            }
            return; // Si el enemigo est� muerto, no hacer nada y termina el update
        }
        float yStore = theRB.linearVelocity.y; // Almacenar la velocidad en el eje Y del Rigidbody

        float distance = Vector3.Distance(transform.position, player.transform.position); // Calcular la distancia entre el enemigo y el jugador

        if (distance < chaseRange && PlayerController.instance.isDead == false) // Si el jugador est� dentro del rango de persecuci�n y el jugador no esa muerto
        {
            //transform.LookAt(player.transform.position);
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z)); // Mirar al jugador en el eje Y

            if (distance > stopCloseRange) // Si el jugador esta en el rango de parada
            {
                theRB.linearVelocity = (transform.forward + (transform.right * strafeAmount))* moveSpeed; // Movimiento del enemigo hacia el jugador con movimiento lateral aleatorio

                anim.SetBool("moving", true); // Hacer que la animaci�n de caminar se reproduzca cuando el enemigo se mueva
            }
            else
            {
                theRB.linearVelocity = Vector3.zero; // Detener el movimiento del enemigo cuando est� cerca del jugador

                anim.SetBool("moving", false); // Hacer que la animaci�n de caminar se detenga cuando el enemigo no se mueva
            }

            // Disparar proyectiles
            shotCounter -= Time.deltaTime; // Reducir el contador de disparos
            if (shotCounter <= 0) // Si el contador de disparos ha llegado a cero
            {
                shootPoint.LookAt(player.theCam.transform.position); // Mirar al jugador en el eje Y

                EnemyProjectile newProjectile = Instantiate(projectile, shootPoint.position, shootPoint.rotation); // Instanciar un nuevo proyectil en el punto de disparo
                newProjectile.damageAmount = shotDamage; // Asignar el da�o del proyectil

                shotCounter = timeBetweenShots; // Reiniciar el contador de disparos

                anim.SetTrigger("shooting"); // Iniciar la animaci�n de disparo
 
            }

            
        }
        else
        {
            if (patrolPoints.Length > 0) // Si hay puntos de patrullage definidos
            {
                if(Vector3.Distance(transform.position, new Vector3(patrolPoints[currentPatrolPoint].position.x, transform.position.y, patrolPoints[currentPatrolPoint].position.z)) < .25f) // Si el enemigo est� cerca del punto de patrullaje actual
                {
                    // Reducir el contador y detener el movimiento
                    waitCounter -= Time.deltaTime; // Reducir el contador de espera
                    theRB.linearVelocity = Vector3.zero; // Detener el movimiento del enemigo
                    anim.SetBool("moving", false); // Hacer que la animaci�n de caminar se detenga cuando el enemigo no se mueva

                    if (waitCounter <= 0) // Si el contador de espera ha llegado a cero
                    {
                        // Cambiar al siguiente punto de patrullaje
                        currentPatrolPoint++; // Cambiar al siguiente punto de patrullaje
                        if (currentPatrolPoint >= patrolPoints.Length) // Si se ha llegado al �ltimo punto de patrullaje
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

                    anim.SetBool("moving", true); // Hacer que la animaci�n de caminar se reproduzca cuando el enemigo se mueva     
                }
            }
            else
            {
                theRB.linearVelocity = Vector3.zero; // Detener el movimiento del enemigo cuando est� fuera del rango de persecuci�n

                anim.SetBool("moving", false); // Hacer que la animaci�n de caminar se detenga cuando el enemigo no se mueva
            }
            
        }

        theRB.linearVelocity = new Vector3(theRB.linearVelocity.x, yStore, theRB.linearVelocity.z); // Mantener la velocidad en el eje Y
    }

    public void TakeDamage(float damageToTake)
    {
        // Debug.Log("Enemy hit"); // Imprimir un mensaje en la consola cuando el enemigo recibe da�o

        // Destroy(gameObject); // Destruir el objeto del enemigo

        currentHealth -= damageToTake;

        if (currentHealth <= 0)
        {
            anim.SetTrigger("die"); // inicia animaci�n de muerte

            isDead = true; // Marcar al enemigo como muerto

            theRB.linearVelocity = Vector3.zero; // Detener el movimiento del enemigo
            theRB.isKinematic = true; // Hacer que el Rigidbody sea cinem�tico para que no se vea afectado por la f�sica

            GetComponent<Collider>().enabled = false; // Desactivar el collider del enemigo
        }
    }

}
