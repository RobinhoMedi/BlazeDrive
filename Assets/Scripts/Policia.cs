using UnityEngine;

public class Policia : MonoBehaviour
{
    public GameObject PanelPerder;
    public float Velocidad = 2f;
    public Transform Objetivo;
    private bool movimientoActivado = false;
        private Vector3 posicionInicial;
        [Header("Particulas")]
        public GameObject ParticulaExplosion;
        public GameObject PoliciaGameObject;

    void Update()
    {
        if (movimientoActivado && Objetivo != null)
        {
            // Calcular la dirección original hacia el objetivo
            Vector3 direccionOriginal = (Objetivo.position - transform.position).normalized;

            // Verificar si hay obstáculos en la dirección original
            if (!HayObstaculoEnDireccion(direccionOriginal))
            {
                // Si no hay obstáculos, moverse en la dirección original
                transform.position = Vector3.MoveTowards(transform.position, Objetivo.position, Velocidad * Time.deltaTime);
            }
            else
            {
                // Si hay obstáculos, calcular una nueva dirección evitando los obstáculos
                Vector3 nuevaDireccion = CalcularNuevaDireccion(direccionOriginal);
                transform.position = Vector3.MoveTowards(transform.position, transform.position + nuevaDireccion, Velocidad * Time.deltaTime);
            }

            // Rotación
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccionOriginal);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * 5f);
        }
    }

    bool HayObstaculoEnDireccion(Vector3 direccion)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direccion, out hit, 1f)) // Ajusta el rango según sea necesario
        {
            if (hit.collider.gameObject.CompareTag("Obstacle"))
            {
                return true;
            }
        }
        return false;
    }

    Vector3 CalcularNuevaDireccion(Vector3 direccionOriginal)
    {
        // Puedes implementar lógica más avanzada aquí, pero por ahora, simplemente rota 90 grados en sentido horario

        return Quaternion.Euler(0, 90, 0) * direccionOriginal;
    }

    public void ActivarMovimiento(Transform objetivo)
    {
        Objetivo = objetivo;
        movimientoActivado = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PanelPerder.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PoderPlayer"))
        {
            Instantiate(ParticulaExplosion, transform.position, Quaternion.identity);
            PoliciaGameObject.SetActive(false);
        }
    }
    public void DesactivarMovimiento()
{
    movimientoActivado = false;
    Objetivo = null;
    transform.position = posicionInicial; // Asegúrate de tener una variable "posicionInicial" para almacenar la posición inicial de la policía.
}
    
    public void DejarDeSeguir()
    {
        // Método que puedes llamar desde otro script para que la policía deje de seguir al jugador
        DesactivarMovimiento();
    }
}
