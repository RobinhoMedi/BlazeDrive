using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NO_STARS : MonoBehaviour
{
    [Header("Animaciones")]
    public GameObject MarcoMiniMapaNormal;
    public GameObject MarcoMiniMapaPolicia;

    public Text mensajeTexto;
    public float TiempoMostrandoTexto = 3f;
    [Header("Estrellas")]
    public GameObject Star, Star_1, Star_2;
    [Header("Policias")]
    public GameObject Policia, Policia1;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MarcoMiniMapaNormal.SetActive(true);
            MarcoMiniMapaPolicia.SetActive(false);
            Policia.SetActive(false);
            Star.SetActive(false);
            Star_1.SetActive(false);
            Star_2.SetActive(false);

            StartCoroutine(MostrarMensajeTemporal());
        }
    }

    IEnumerator MostrarMensajeTemporal()
    {
        mensajeTexto.gameObject.SetActive(true);
        yield return new WaitForSeconds(TiempoMostrandoTexto);
        mensajeTexto.gameObject.SetActive(false);
    }
}
