using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Dinero : MonoBehaviour
{
    [Header("Animaciones")]
    public GameObject MarcoMiniMapaNormal;
    public GameObject MarcoMiniMapaPolicia;
    [Header("Paredes")]
    public GameObject Pared1;
    public GameObject Pared2;
    [Header("SINESTRELLAS")]
    public GameObject NoStars;
    [Header("Texto")]
    [Space]
    public Text contadorMonedasTexto;
    [Header("Auto")]
    public GameObject BotonTurbo;
    public GameObject ParticulaConfeti;
    // Lista para mantener contadores individuales para cada moneda
    private static List<int> contadoresMonedas = new List<int>();

    [Header("Policia")]
    [Space]
    public GameObject PoliciaBool;
    public Policia policia;
    public GameObject PoliciaGameObj, policia1;
    public GameObject Star, Star_1, Star_2;
    public GameObject particulaDinero;

    public GameObject panelMonedasRecogidas;

    void Start()
    {
        // Inicializar el contador para esta moneda
        contadoresMonedas.Add(0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Animacion Del Mini Mapa
            MarcoMiniMapaPolicia.SetActive(true);
            MarcoMiniMapaNormal.SetActive(false);
            //Eliminar las estrellas
            NoStars.SetActive(true);
            Instantiate(particulaDinero, transform.position, Quaternion.identity);

            // Desactivar el objeto en lugar de destruirlo
            gameObject.SetActive(false);
            Star.SetActive(true);
            Star_1.SetActive(true);
            Star_2.SetActive(true);

            // Incrementar el contador de monedas específico para esta moneda
            contadoresMonedas[contadoresMonedas.Count - 1]++;

            // Actualizar el texto del contador específico para esta moneda
            ActualizarContadorMonedas();

            // Activar el movimiento del policía
            policia.ActivarMovimiento(other.transform);
            Debug.Log("La Policia Te Persigue");
            PoliciaBool.SetActive(true);

            // Verificar si se han recogido 5 monedas
            if (contadoresMonedas[contadoresMonedas.Count - 1] == 5)
            {
                MostrarPanel();
            }
        }
    }

    void ActualizarContadorMonedas()
    {
        // Actualizar el texto del contador específico para esta moneda
        contadorMonedasTexto.text = "" + contadoresMonedas[contadoresMonedas.Count - 1].ToString();
    }

    void MostrarPanel()
    {
        // Activar el GameObject del panel
        panelMonedasRecogidas.SetActive(true);
        Instantiate(ParticulaConfeti, transform.position, Quaternion.identity);
        BotonTurbo.SetActive(true);
        Pared1.SetActive(false);
        Pared2.SetActive(false);
        Debug.Log("Se han recogido 5 monedas. Mostrar panel o realizar acción deseada.");

        // Llamar a la función para desactivar el panel después de 3 segundos
        Invoke("OcultarPanel", 3f);
    }

    void OcultarPanel()
    {
        // Desactivar el GameObject del panel después de 3 segundos
        panelMonedasRecogidas.SetActive(false);
    }

}
