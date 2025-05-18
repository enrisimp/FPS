using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TMP_Text ammoText, remainingAmmoText; // Texto de munición

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateAmmoText(int currentAmmo, int remainingAmmo)
    {
        ammoText.text = currentAmmo.ToString(); // Actualizar el texto de munición
        remainingAmmoText.text = "/" + remainingAmmo.ToString(); // Actualizar el texto de munición restante
    }
}
