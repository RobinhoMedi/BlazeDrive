using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rendimiento
{
    public class Rendimiento : MonoBehaviour
    {
        public GameObject OcultarCiudad;
        public GameObject OcultarGranja;
        public GameObject OcultarNieve;
        public GameObject MostrarC;
        public GameObject MostrarG;
        public GameObject MostrarN;
        public GameObject OcultarObjetosC;
        public GameObject OcultarObjetosG;
        public GameObject OcultarObjetosN;

        // Variable para mantener el estado actual del trigger
        private bool triggerActivado = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Dependiendo del trigger en el que el jugador entre, se realizarán diferentes acciones
                if (gameObject.CompareTag("TriggerCiudad"))
                {
                    // Invertir el estado de los GameObjects
                    OcultarCiudad.SetActive(triggerActivado);
                    MostrarC.SetActive(!triggerActivado);
                    OcultarObjetosC.SetActive(triggerActivado);
                }
                else if (gameObject.CompareTag("TriggerGranja"))
                {
                    OcultarGranja.SetActive(triggerActivado);
                    MostrarG.SetActive(!triggerActivado);
                    OcultarObjetosG.SetActive(triggerActivado);
                }
                else if (gameObject.CompareTag("TriggerNieve"))
                {
                    OcultarNieve.SetActive(triggerActivado);
                    MostrarN.SetActive(!triggerActivado);
                    OcultarObjetosN.SetActive(triggerActivado);
                }

                // Cambiar el estado del trigger
                triggerActivado = !triggerActivado;
            }
        }
    }
}
