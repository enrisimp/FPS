using Unity.Collections;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    public float range; // Rango de disparo 35
    public Transform cam; // Referencia a la c�mara 

    public LayerMask validLayers; // M�scaras de capas v�lidas para el rayo

    public GameObject impactEffect, damageEffect; // Prefab del efecto de impacto

    public GameObject muzzleFlare; // Prefab del destello de boca de fuego
    public float flareDisplayTime = .1f; // Tiempo de visualizaci�n del destello de boca de fuego
    private float flareCounter; // Contador para el tiempo de visualizaci�n del destello

    public bool canAutoFire; // Indica si el arma puede disparar autom�ticamente
    public float timeBetweenShots = .1f; // Tiempo entre disparos autom�ticos
    private float shotCounter; // Contador para el tiempo entre disparos autom�ticos

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
        RaycastHit hit; // Variable para almacenar la informaci�n del rayo
        if(Physics.Raycast(cam.position, cam.forward, out hit, range, validLayers)) // Lanzar un rayo desde la c�mara hacia adelante
        {
            // Debug.Log(hit.transform.name); // Imprimir el nombre del objeto golpeado en la consola

            if(hit.transform.tag == "Enemy") // Verificar si el objeto golpeado tiene la etiqueta "Enemy"
            {
                Instantiate(damageEffect, hit.point, Quaternion.identity); // Instanciar el efecto de da�o en el punto de colisi�n
            } 
            else
            {
                Instantiate(impactEffect, hit.point, Quaternion.identity); // Instanciar el efecto de impacto en el punto de colisi�n
            }
            
        }

        muzzleFlare.SetActive(true); // Activar el destello de boca de fuego
        flareCounter = flareDisplayTime; // Reiniciar el contador del destello


    }

    public void ShootHeld()
    {
        if(canAutoFire) // Verificar si el arma puede disparar autom�ticamente
        {
            if(shotCounter <= 0) // Verificar si el contador de disparos es menor o igual a cero
            {
                Shoot(); // Llamar al m�todo Shoot para disparar
            }
            else
            {
                shotCounter -= Time.deltaTime; // Reducir el contador de disparos por el tiempo transcurrido
            }
        }
    }
}
