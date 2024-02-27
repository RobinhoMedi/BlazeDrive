using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public float distanciaRaycast = 0.5f;
    public LayerMask capaParedes;

    void LateUpdate()
    {
        // Obtener la posición actual de la cámara
        Vector3 posicionCamara = transform.position;

        // Obtener la dirección hacia donde mira la cámara
        Vector3 direccionCamara = transform.forward;

        // Lanzar un rayo desde la cámara en la dirección en que mira
        RaycastHit hit;
        if (Physics.Raycast(posicionCamara, direccionCamara, out hit, distanciaRaycast, capaParedes))
        {
            // Si el rayo golpea una pared, ajusta la posición de la cámara
            transform.position = hit.point - direccionCamara * 0.1f; // Puedes ajustar el factor multiplicador según tus necesidades
        }
    }
}
