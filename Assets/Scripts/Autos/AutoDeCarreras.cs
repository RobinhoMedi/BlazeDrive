using UnityEngine;
using UnityEngine.Audio;

public class AutoDeCarreras : MonoBehaviour
{
    private bool isUserInputEnabled = true;
    private float userInputDisabledDuration = 0.1f;
    
    [Header("Audio")]
    public AudioSource Bocina;
    
    [Header("Particula")]
    public GameObject particulaTurboObj;
    public ParticleSystem TurboParticle;
    
    [Space]
    [Header("Obstacle")]
    public float backwardForce = 10f;
    
    [Space]
    [Header("ConfiguracionDelAuto")]
    public float normalSpeed = 10f;
    public float turboSpeed = 20f;
    public float backwardSpeed = 5f;
    public float rotationSpeed = 100f;
    public float backwardRotationMultiplier = 2f;
    public float jumpForce = 5f;
    public float brakeDeceleration = 10f;
    public float turboDuration = 3f; // Duración del turbo en segundos

    // Campo para evitar vuelco
    [Header("Anti-Rollover")]
    public float rotationForceMultiplier = 10f;

    private bool isGrounded;
    private bool isMovingForward;
    private bool isTurboActive;
    private float turboTimer;
    private Rigidbody rb;
    
    [Header("Boton")]
    public GameObject BotonConcesionario;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<BoxCollider>();
        }

        collider.isTrigger = false;
        rb.centerOfMass = new Vector3(0, -0.5f, 0);

        // Corrige la orientación inicial del auto
        transform.rotation = Quaternion.Euler(0f, -90f, 0f);
    }

void Update()
{
    if (!isUserInputEnabled)
    {
        return;
    }

    if (Input.GetKeyDown(KeyCode.H))
    {
        Bocina.Play();
    }

    if (Input.GetKeyDown(KeyCode.R))
    {
        ReiniciarRotacionZ();
        Debug.Log("Pressed R");
    }

float horizontalInput = Input.GetAxis("Horizontal");
float verticalInput = Input.GetAxis("Vertical");

float translation = 0f;
float lateralTranslation = 0f;

float currentSpeed = isTurboActive ? turboSpeed : (Mathf.Abs(verticalInput) > 0 ? normalSpeed : backwardSpeed);

if (Mathf.Abs(verticalInput) > 0)
{
    translation = verticalInput * currentSpeed * Time.deltaTime;
    isMovingForward = verticalInput > 0;
}
else
{
    translation = verticalInput * backwardSpeed * Time.deltaTime;
    isMovingForward = false;
}

lateralTranslation = horizontalInput * rotationSpeed * Time.deltaTime;

if (Mathf.Abs(verticalInput) == 0)
{
    translation -= brakeDeceleration * Time.deltaTime;
    translation = Mathf.Max(0f, translation);
}

rb.MovePosition(rb.position + transform.forward * translation);

// Ajustar la rotación directamente a través del Rigidbody
Quaternion deltaRotation = Quaternion.Euler(Vector3.up * lateralTranslation);
rb.MoveRotation(rb.rotation * deltaRotation);


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

    if (Input.GetButtonDown("Jump") && isGrounded)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }
}


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Cars"))
        {
            isGrounded = false;
            isTurboActive = false;

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(-transform.forward * backwardForce, ForceMode.Impulse);

            DeshabilitarEntradaUsuario();

            Invoke("ReactivarEntradaUsuario", userInputDisabledDuration);
        }

        if (collision.gameObject.CompareTag("Cars"))
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
        isUserInputEnabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cars"))
        {
            BotonConcesionario.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cars"))
        {
            BotonConcesionario.SetActive(false);
        }
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

    private void ReiniciarRotacionZ()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0f);
    }
}
