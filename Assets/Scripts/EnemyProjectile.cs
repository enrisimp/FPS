using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float moveSpeed = 10f, damageAmount = 15f; // Velocidad de movimiento del proyectil y cantidad de da�o

    public Rigidbody theRB; // Referencia al Rigidbody del proyectil

    public GameObject impactEffect, damageEffect; // Efectos de impacto y da�o

    public float lifetime = 5f; // Tiempo de vida del proyectil


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime); // Destruir el proyectil despu�s de un tiempo de vida especificado
    }

    // Update is called once per frame
    void Update()
    {
        theRB.linearVelocity = transform.forward * moveSpeed; // Mover el proyectil hacia adelante a la velocidad especificada


    }

    private void OnTriggerEnter(Collider other) // M�todo que se llama cuando el proyectil colisiona con otro objeto
    {
        if(other.tag == "Player") // Si el objeto colisionado tiene la etiqueta "Player"
        {
            //Debug.Log("Damaging Player for " + damageAmount); // Imprimir en la consola que el proyectil ha golpeado al jugador

            Instantiate(damageEffect, other.transform.position, Quaternion.identity); // Instanciar el efecto de da�o en la posici�n del jugador

            PlayerHealthController.instance.TakeDamage(damageAmount); // Llamar al m�todo TakeDamage del controlador de salud del jugador para reducir su salud
        }
        else
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity); // Instanciar el efecto de impacto en la posici�n del proyectil
        }

        Destroy(gameObject); // Destruir el proyectil
        
    }
}
