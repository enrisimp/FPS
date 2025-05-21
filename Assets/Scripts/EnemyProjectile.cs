using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float moveSpeed = 10f, damageAmount = 15f; // Velocidad de movimiento del proyectil y cantidad de daño

    public Rigidbody theRB; // Referencia al Rigidbody del proyectil

    public GameObject impactEffect, damageEffect; // Efectos de impacto y daño

    public float lifetime = 5f; // Tiempo de vida del proyectil


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime); // Destruir el proyectil después de un tiempo de vida especificado
    }

    // Update is called once per frame
    void Update()
    {
        theRB.linearVelocity = transform.forward * moveSpeed; // Mover el proyectil hacia adelante a la velocidad especificada


    }

    private void OnTriggerEnter(Collider other) // Método que se llama cuando el proyectil colisiona con otro objeto
    {
        if(other.tag == "Player") // Si el objeto colisionado tiene la etiqueta "Player"
        {
            Instantiate(damageEffect, other.transform.position, Quaternion.identity); // Instanciar el efecto de daño en la posición del jugador

            Debug.Log("Damaging Player for " + damageAmount); // Imprimir en la consola que el proyectil ha golpeado al jugador
        }
        else
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity); // Instanciar el efecto de impacto en la posición del proyectil
        }

        Destroy(gameObject); // Destruir el proyectil
        
    }
}
