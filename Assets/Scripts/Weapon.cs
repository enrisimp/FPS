using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float range; // Rango de disparo 35

    public GameObject muzzleFlare; // Prefab del destello de boca de fuego
    public float flareDisplayTime = .1f; // Tiempo de visualizaci�n del destello de boca de fuego


    public bool canAutoFire; // Indica si el arma puede disparar autom�ticamente
    public float timeBetweenShots = .1f; // Tiempo entre disparos autom�ticos

    public int currentAmmo = 100; // Munici�n actual
    public int clipSize = 15; // Tama�o del cargador
    public int remainingAmmo = 300; // Munici�n restante

    public int pickupAmount; // Munici�n recogida al recoger un objeto de munici�n

    public float damageAmount = 15f; // Da�o realizado
}
