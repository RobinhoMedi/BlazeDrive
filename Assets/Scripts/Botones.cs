using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botones : MonoBehaviour
{
    [Header("Autos")]
    public GameObject Auto;

    [Header("GameObjects")]
    public GameObject Ciudad;

//    public GameObject CamaraPrincipal;
    public GameObject CamaraConcesionario;

    [Header("Botones")]
    public GameObject BotonConcesionario;
    public GameObject BotonHome;

    public void EntrarAlConcesionario()
    {
        Auto.SetActive(true);
        CamaraConcesionario.SetActive(true);
        BotonConcesionario.SetActive(false);
        Time.timeScale = 0f;
    }

    public void SalirConcesionario()
    {
        CamaraConcesionario.SetActive(false);
        Time.timeScale = 1f;
    }
}
