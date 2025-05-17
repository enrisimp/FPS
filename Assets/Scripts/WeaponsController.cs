using Unity.Collections;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    public float range; // Rango de disparo 35
    public Transform cam; // Referencia a la cámara 

    public LayerMask validLayers; // Máscaras de capas válidas para el rayo

    public GameObject impactEffect, damageEffect; // Prefab del efecto de impacto

    public GameObject muzzleFlare; // Prefab del destello de boca de fuego
    public float flareDisplayTime = .1f; // Tiempo de visualización del destello de boca de fuego
    private float flareCounter; // Contador para el tiempo de visualización del destello

    public bool canAutoFire; // Indica si el arma puede disparar automáticamente
    public float timeBetweenShots = .1f; // Tiempo entre disparos automáticos
    private float shotCounter; // Contador para el tiempo entre disparos automáticos

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flareCounter > 0) // Verificar si el contador del destello es mayor que cero
        {
            flareCounter -= Time.deltaTime; // Reducir el contador por el tiempo transcurrido

            if(flareCounter <= 0) // Verificar si el contador ha llegado a cero
            {
                muzzleFlare.SetActive(false); // Desactivar el destello de boca de fuego
            }
        }
        else
        {
            muzzleFlare.SetActive(false); // Desactivar el destello de boca de fuego
        }
    }

    public void Shoot()
    {
        //Debug.Log("Disparar"); // Imprimir "Disparar" en la consola
        RaycastHit hit; // Variable para almacenar la información del rayo
        if(Physics.Raycast(cam.position, cam.forward, out hit, range, validLayers)) // Lanzar un rayo desde la cámara hacia adelante
        {
            // Debug.Log(hit.transform.name); // Imprimir el nombre del objeto golpeado en la consola

            if(hit.transform.tag == "Enemy") // Verificar si el objeto golpeado tiene la etiqueta "Enemy"
            {
                Instantiate(damageEffect, hit.point, Quaternion.identity); // Instanciar el efecto de daño en el punto de colisión
            } 
            else
            {
                Instantiate(impactEffect, hit.point, Quaternion.identity); // Instanciar el efecto de impacto en el punto de colisión
            }
            
        }

        muzzleFlare.SetActive(true); // Activar el destello de boca de fuego
        flareCounter = flareDisplayTime; // Reiniciar el contador del destello


    }

    public void ShootHeld()
    {
        if(canAutoFire) // Verificar si el arma puede disparar automáticamente
        {
            if(shotCounter <= 0) // Verificar si el contador de disparos es menor o igual a cero
            {
                Shoot(); // Llamar al método Shoot para disparar
            }
            else
            {
                shotCounter -= Time.deltaTime; // Reducir el contador de disparos por el tiempo transcurrido
            }
        }
    }
}
