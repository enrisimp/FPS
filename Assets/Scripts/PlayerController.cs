using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Singleton est�tico para asegurar que solo haya una instancia de PlayerController
    public static PlayerController instance; // Instancia est�tica de PlayerController
    private void Awake()
    {
        instance = this; // Asignar la instancia si no existe
    }

    // Referencia al componente Character Controller
    private CharacterController charCon; // Componente Character Controller
    private Vector3 currentMovement; // Movimiento actual del personaje
    private Vector2 rotStore; // Almacenar la rotaci�n del personaje

    public InputActionReference moveAction; // Referencia a la acci�n de movimiento
    public InputActionReference lookAction; // Referencia a la acci�n de mirar
    public InputActionReference jumpAction; // Referencia a la acci�n de saltar
    public InputActionReference SprintAction; // Referencia a la acci�n de correr
    public InputActionReference shootAction; // Referencia a la acci�n de disparar
    public InputActionReference reloadAction; // Referencia a la acci�n de recargar
    public Camera theCam; // Referencia a la c�mara
    public WeaponsController weaponCon; // Referencia al controlador de armas
    public InputActionReference nextWeapon, prevWeapon; // Referencias a las acciones de cambiar de arma



    public float moveSpeed = 5f; // Velocidad de movimiento

    public float lookSpeed = 2f; // Velocidad de mirar
    public float minLookAngle, maxLookAngle; // �ngulos m�nimo y m�ximo de mirada -85 y 75

    public float jumpPower; // Potencia de salto
    public float gravityModifier = 4f; // Gravedad

    public float runSpeed = 10f; // Velocidad de carrera
    public float camZoomNormal, camZoomOut, camZoomSpeed; // Zoom normal y zoom out de la c�mara al correr 60 y 75 y velocidad de zoom 5f

    public bool isDead; // Indica si el jugador est� muerto




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        charCon = GetComponent<CharacterController>(); // Obtener el componente Character Controller
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor en el centro de la pantalla

    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == true) // Verificar si el jugador est� muerto
        {
            return; // Salir del m�todo si el jugador est� muerto
        }

        // 
        if(Time.timeScale == 0) // Verificar si el tiempo est� pausado
        {
            return; // Salir del m�todo si el tiempo est� pausado
        }
        
        // Capturar el movimiento del personaje
        float yStore = currentMovement.y; // Almacenar el valor de Y del movimiento actual

        Vector2 moveInput = moveAction.action.ReadValue<Vector2>(); // Leer el valor de entrada de movimiento

        // Debug.Log(moveInput); // Imprimir el valor de entrada en la consola
                
        Vector3 moveFoward = transform.forward * moveInput.y; // Calcular el movimiento hacia adelante basado en la direccion del personaje
        Vector3 moveSideways = transform.right * moveInput.x; // Calcular el movimiento lateral basado en la direccion del personaje


        // Combinar el movimiento hacia adelante y lateral y aplicar la velocidad
        // Verificar si se est� corriendo
        if (SprintAction.action.IsPressed()) // Verificar si la acci�n de correr est� presionada
        {
            currentMovement = (moveFoward + moveSideways) * runSpeed; // Aumentar la velocidad de movimiento

            if(currentMovement != Vector3.zero) // Verificar si hay movimiento
            {
                theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, camZoomOut, camZoomSpeed * Time.deltaTime); // Aumentar el campo de visi�n de la c�mara al correr
            }
            
        }
        else
        {
            currentMovement = (moveFoward + moveSideways) * moveSpeed; // Mantener la velocidad de movimiento normal

            theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, camZoomNormal, camZoomSpeed * Time.deltaTime); // Restablecer el campo de visi�n de la c�mara al valor normal
        }

               
        if(charCon.isGrounded) // Verificar si el personaje est� en el suelo
        {
            yStore = 0f; // Reiniciar la gravedad si est� en el suelo
        }
        
        currentMovement.y = yStore + (Physics.gravity.y * Time.deltaTime * gravityModifier); // Aplicar la gravedad al movimiento actual

        // Aplicar el salto si se presiona la tecla de salto
        if(jumpAction.action.WasPressedThisFrame() && charCon.isGrounded == true)
        {
            currentMovement.y = jumpPower; // Aplicar la potencia de salto al movimiento actual
        }

        // Aplicar el movimiento al CharacterController
        charCon.Move(currentMovement * Time.deltaTime); // Mover el CharacterController

        //// Aplicar el movimiento al CharacterController
        //Vector3 move = new Vector3(moveInput.x * moveSpeed, 0, moveInput.y * moveSpeed); // Crear un vector de movimiento
        //charCon.Move(move * Time.deltaTime); // Mover el CharacterController

        // Mover el personaje hacia adelante en cada fotograma
        //charCon.Move(new Vector3(0, 0, moveSpeed) * Time.deltaTime);

        // Manejar la rotaci�n del personaje
        // Capturar la entrada de la rotraci�n
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>(); // Leer el valor de entrada de mirar
        lookInput.y = -lookInput.y; // Invertir el eje Y
        //Acumular la rotaci�n con el input multiplicado por la velocidad y el tiempo
        rotStore += lookInput * lookSpeed * Time.deltaTime; // Acumular la rotaci�n
        
        rotStore.y = Mathf.Clamp(rotStore.y, minLookAngle, maxLookAngle); // Limitar la rotaci�n en el eje Y
        
        transform.rotation = Quaternion.Euler(0, rotStore.x, 0f); // Rotar el personaje en el eje Y
        theCam.transform.localRotation = Quaternion.Euler(rotStore.y, 0f, 0f); // Rotar la c�mara en el eje X


        // Disparar el arma
        if (shootAction.action.WasPressedThisFrame()) // Verificar si la acci�n de disparar fue presionada
        {
            weaponCon.Shoot(); // Llamar al m�todo Shoot del controlador de armas
        }

        if (shootAction.action.IsPressed()) // Verificar si la acci�n de disparar est� presionada
        {
            weaponCon.ShootHeld(); // Llamar al m�todo ShootHeld del controlador de armas
        }

        // Recargar el arma
        if (reloadAction.action.WasPressedThisFrame()) // Verificar si la acci�n de recargar fue presionada
        {
            weaponCon.Reload(); // Llamar al m�todo Reload del controlador de armas
        }
        
        // Cambiar de arma
        if(nextWeapon.action.WasPressedThisFrame()) // Verificar si la acci�n de cambiar a la siguiente arma fue presionada
        {
            weaponCon.NextWeapon(); // Llamar al m�todo NextWeapon del controlador de armas
        }

        if(prevWeapon.action.WasPressedThisFrame()) // Verificar si la acci�n de cambiar a la arma anterior fue presionada
        {
            weaponCon.PreviousWeapon(); // Llamar al m�todo PrevWeapon del controlador de armas
        }
    }

}
