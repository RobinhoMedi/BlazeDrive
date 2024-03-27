using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using SimpleInputNamespace;
using System.Collections;
public class Auto : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject Panel_Poder;

    [Header("Policia")]
    public GameObject PoliciaDesactivar;
    [Header("Joystick")]
    public VariableJoystick joystick;
    public SteeringWheel steeringWheel;
    private bool isUserInputEnabled = true;
    private float userInputDisabledDuration = 0.1f;
    [Header("Audio")]
    public AudioSource ExplosionSonido;
    public AudioSource DerrapeSound;
    public AudioSource Bocina;
    [Header("Particula")]
    public GameObject Poder;
    public GameObject ParticulaDerrape;
    public GameObject ParticulaDerrape1;
    public ParticleSystem Derrape;
    public ParticleSystem Derrape1;
    public GameObject ParticulaExplosion;
    public GameObject particulaTurboObj;
    public ParticleSystem TurboParticle;
    [Space]
    [Header("Obstacle")]
    public float backwardForce = 10f;
    [Space]
    [Header("ConfiguracionDelAuto")]
    public GameObject GetPoder;
    public float normalSpeed = 10f;
    public float turboSpeed = 20f;
    public float backwardSpeed = 5f;
    public float rotationSpeed = 100f;
    public float backwardRotationMultiplier = 2f;
    public float brakeDeceleration = 10f;
    public float turboDuration = 3f; // Duración del turbo en segundos

    [Header("Derrape")]
    private bool isDerrapeActive = false;
    public float derrapeForce = 5f;
    public float derrapeDrag = 2f;
    // Campo para evitar vuelco
    [Header("Anti-Rollover")]
    public float rotationForceMultiplier = 10f;

    private bool isGrounded;
    private bool isMovingForward;
    private bool isTurboActive;
    private float turboTimer;
    private Rigidbody rb;
    [Header("Boton")]
    public Button botonDerrape;
    public Button botonTurbo;
    public GameObject BotonConcesionario;

    void Start()
    {
        botonTurbo.onClick.AddListener(ActivarTurboDesdeBoton);
        botonTurbo.onClick.AddListener(ActivarBotonDerrape);
        // Asegúrate de que haya un Rigidbody en el objeto
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        // Asegúrate de que haya un Collider en el objeto
        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            // Puedes ajustar el tipo de collider según tus necesidades (BoxCollider, SphereCollider, etc.)
            collider = gameObject.AddComponent<BoxCollider>();
        }

        // Configura el Collider como sólido físico, no un trigger
        collider.isTrigger = false;
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        if (!isUserInputEnabled)
        {
            // Evitar que se realice cualquier entrada del usuario mientras está deshabilitada
            return;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Bocina.Play();
        }

        // Reiniciar Posicion Z
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReiniciarRotacionZ();
            Debug.Log("Pressed R");
        }

        // Obtener la entrada del joystick
        float joystickHorizontalInput = joystick.Horizontal;
        float joystickVerticalInput = joystick.Vertical;

        // Obtener la entrada del volante
        float steeringWheelInput = steeringWheel.Value;

        // Obtener la entrada del teclado
        float keyboardVerticalInput = Input.GetAxis("Vertical");
        float keyboardHorizontalInput = Input.GetAxis("Horizontal"); // Obtener entrada horizontal del teclado

        // Combinar ambas entradas
        float horizontalInput = Mathf.Clamp(joystickHorizontalInput + steeringWheelInput + keyboardHorizontalInput, -1f, 1f);
        float verticalInput = Mathf.Clamp(joystickVerticalInput + keyboardVerticalInput, -1f, 1f);

        float translation = 0f;
        float currentSpeed = isTurboActive ? turboSpeed : (Mathf.Abs(verticalInput) > 0 ? normalSpeed : backwardSpeed);

        // Cambios en la lógica de movimiento
        if (Mathf.Abs(verticalInput) > 0)
        {
            translation = verticalInput * currentSpeed * Time.deltaTime;

            float rotation = 0f;

            // Girar en la dirección adecuada según la marcha (adelante o atrás)
            if (verticalInput > 0)
            {
                rotation = horizontalInput * rotationSpeed * Time.deltaTime;
            }
            else if (verticalInput < 0)
            {
                rotation = -horizontalInput * rotationSpeed * backwardRotationMultiplier * Time.deltaTime;
            }

            transform.Rotate(0, rotation, 0);

            // Aplicar fuerza de derrape para frenar
            if (!Input.GetKey(KeyCode.Space))
            {
                ApplyBrake();
            }
        }

        // Aplicar fuerza de derrape
        if (Input.GetKey(KeyCode.Space) && Mathf.Abs(verticalInput) > 0)
    {
        ParticulaDerrape.SetActive(true);
        ParticulaDerrape1.SetActive(true);
        Derrape.Play();
        Derrape1.Play();
        DerrapeSound.Play();

        // Calcular la dirección del derrape en función de la dirección del movimiento
        Vector3 derrapeDirection = verticalInput > 0 ? horizontalInput > 0 ? transform.right : -transform.right : -transform.forward;

        // Aplicar la fuerza de derrape
        rb.AddForce(derrapeDirection * derrapeForce, ForceMode.Acceleration);
        rb.angularDrag = derrapeDrag;

        // Ajustar la velocidad angular para un derrape más suave
        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, Time.deltaTime * 5f);
    }
    else
    {
        // Desactivar las partículas y el sonido de derrape
        if (Derrape.isPlaying)
        {
            Derrape.Pause();
            Derrape1.Pause();
            DerrapeSound.Pause();
        }

        // Restablecer el arrastre angular cuando no se está derrapando
        rb.angularDrag = 0.05f;
    }

// Simular frenado gradual al soltar la tecla de aceleración
if (Mathf.Abs(verticalInput) == 0)
{
    // Comentamos la línea que aplica una fuerza de freno
    // rb.velocity *= 0.95f;

    // Ajustar la velocidad lineal directamente
    rb.velocity += transform.forward * brakeDeceleration * Time.deltaTime;

    // Limitar la velocidad mínima para evitar que el auto se detenga por completo
    rb.velocity = Vector3.ClampMagnitude(rb.velocity, 0.1f);
}

    transform.Translate(0, 0, translation);


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ActivateTurbo();
            particulaTurboObj.SetActive(true);
            TurboParticle.Play();
        }

        if (isTurboActive)
        {
            turboTimer -= Time.deltaTime;

            if (turboTimer <= 0)
            {
                particulaTurboObj.SetActive(false);
                 TurboParticle.Pause();
                DeactivateTurbo();
            }
        }
    }
        public void ActivarTurboDesdeBoton()
    {
        ActivateTurbo();
        particulaTurboObj.SetActive(true);
        TurboParticle.Play();
    }
    public void ActivarBotonDerrape()
    {
    isDerrapeActive = !isDerrapeActive;

    if (isDerrapeActive)
    {
        ParticulaDerrape.SetActive(true);
        ParticulaDerrape1.SetActive(true);
        Derrape.Play();
        Derrape1.Play();
        DerrapeSound.Play();
    }
    else
    {
        ParticulaDerrape.SetActive(false);
        ParticulaDerrape1.SetActive(false);
        Derrape.Stop();
        Derrape1.Stop();
        DerrapeSound.Stop();
    }
    }
    //Fuerza de Freno
private void ApplyBrake()
{
    rb.velocity *= 0.95f; // Ajusta el factor de frenado según tu preferencia
}
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Cars"))
        {
            // Detener el automóvil al chocar con un obstáculo
            isMovingForward = false;

            // Aplicar fuerza hacia atrás al colisionar con un obstáculo
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(-transform.forward * backwardForce, ForceMode.Impulse);

            // Deshabilitar la entrada del usuario para evitar una mayor aceleración
            DeshabilitarEntradaUsuario();

            // Puedes realizar acciones adicionales, como reproducir un sonido o mostrar un efecto visual.

            // Llamar a ReactivarEntradaUsuario después de un tiempo determinado
            Invoke("ReactivarEntradaUsuario", userInputDisabledDuration);
        }

        if(collision.gameObject.CompareTag("Cars"))
        {
            BotonConcesionario.SetActive(true);
        }
    }

    private void DeshabilitarEntradaUsuario()
    {
        isUserInputEnabled = false;
    }

    private void ReactivarEntradaUsuario()
    {
        // Reactivar la entrada del usuario después de la duración especificada
        // Puedes realizar acciones adicionales según tu lógica de juego.
        isUserInputEnabled = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Cars"))
        {
            BotonConcesionario.SetActive(true);
        }
        else if (other.gameObject.CompareTag("GasStation"))
        {
            ExplosionSonido.Play();
            Instantiate(ParticulaExplosion, transform.position, Quaternion.identity);
        }
        else if(other.gameObject.CompareTag("PoderPlayer"))
        {
            Destroy(GetPoder);
            Poder.SetActive(true);
        }

        else if(other.gameObject.CompareTag("Policia"))
        {
            PoliciaDesactivar.SetActive(false);
            Poder.SetActive(false);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Cars"))
        {
            BotonConcesionario.SetActive(false);
        }
    }
    //Panel Activacion de Poder
    IEnumerator ActivarDesactivarPanel()
    {
        Panel_Poder.SetActive(true); // Activa el panel

        yield return new WaitForSeconds(3f); // Espera 3 segundos

        Poder.SetActive(false); // Desactiva el panel después de 3 segundos
    }

    private void ActivateTurbo()
    {
        isTurboActive = true;
        turboTimer = turboDuration;
    }

    private void DeactivateTurbo()
    {
        isTurboActive = false;
    }

    public void BocinaButton()
    {
       Bocina.Play();
    }

private void ReiniciarRotacionZ()
{
    Vector3 currentRotation = transform.rotation.eulerAngles;
    transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0f);
}
}