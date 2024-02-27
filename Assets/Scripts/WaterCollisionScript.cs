using UnityEngine;
using UnityEngine.UI;

public class WaterCollisionScript : MonoBehaviour
{
    public Text mensajeText;
    public float TiempoMostrandoTexto = 5f;
    public GameObject BotonReiniciar;

    string[] mensajes = new string[]
    {
        "¡Cuidado! Los autos no son submarinos.",
        "¡Ups! Parece que este auto no tiene habilidades acuáticas.",
        "¡Splash! Este auto no está equipado para nadar.",
        "¡Advertencia! Los autos no flotan, ¿quién lo diría?",
        "¡Glub, glub! Los autos no son buenos nadadores.",
        "¡Alerta acuática! Este auto prefiere el asfalto.",
        "¡No es un anfibio! Este auto no se lleva bien con el agua.",
        "¡Error acuático! Los autos no son peces, evítalos en el agua.",
        "¡Hoy no es día de natación para autos!",
        "¡Crashtástrofe acuática! Los autos no están hechos para bucear.",
        "¡Splashdown! Este auto no ganará medallas en natación.",
        "¡S.O.S.! Este auto necesita una toalla, no flota.",
        "¡Cero en habilidades acuáticas! Este auto se hunde.",
        "¡Prohibido bucear! Este auto prefiere el pavimento.",
        "¡Bloop! Este auto no superó la prueba de agua.",
        "¡Houston, tenemos un problema acuático!",
        "¡Ups, error marítimo! Este auto no es un submarino.",
        "¡Bañarse no es lo suyo! Los autos se quedan en tierra firme.",
        "¡Esto no es un vehículo anfibio! Evita charcos profundos.",
        "¡Error acuático! Los autos no son adeptos a la natación."
    };

    private bool mostrarMensaje = true;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && mostrarMensaje)
        {
            MostrarMensajeAleatorio();
            Debug.Log("MensajeAgua");
            Invoke("ResetMostrarMensaje", TiempoMostrandoTexto);
            BotonReiniciar.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            BotonReiniciar.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
    void MostrarMensajeAleatorio()
    {
        if (mensajes.Length > 0 && mostrarMensaje)
        {
            // Elige un mensaje al azar del arreglo
            string mensajeAleatorio = mensajes[Random.Range(0, mensajes.Length)];

            mensajeText.text = mensajeAleatorio;
            mostrarMensaje = false;

            // Invoca la función para resetear el mensaje después de 3 segundos
            Invoke("ResetMostrarMensaje", TiempoMostrandoTexto);
        }
    }

    void ResetMostrarMensaje()
    {
        mensajeText.text = ""; // Limpiar el texto
        mostrarMensaje = true;  // Reactivar la muestra de mensajes después de 3 segundos
    }
}
