using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoDeFuerza : MonoBehaviour
{
    private bool isMovingForward;
    public float backwardForce = 10f;
    private bool isUserInputEnabled = true;
    private float userInputDisabledDuration = 0.1f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CampoDeFuerza"))
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

    }
    private void DeshabilitarEntradaUsuario()
    {
        // Deshabilitar el script o cualquier componente relacionado con la entrada
        // Puedes usar un indicador booleano u otro mecanismo para controlar la entrada del usuario.
        // Por ejemplo, establecer un indicador como: isUserInputEnabled = false;
        isUserInputEnabled = false;
    }
    private void ReactivarEntradaUsuario()
    {
        // Reactivar la entrada del usuario después de la duración especificada
        // Puedes realizar acciones adicionales según tu lógica de juego.
        isUserInputEnabled = true;
    }

}
