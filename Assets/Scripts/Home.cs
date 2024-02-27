using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public  GameObject selectorDePersonajes;
    [Header("Camaras")]
    [Space]
    public GameObject CamaraAutoDefault;
    public GameObject CamaraAutoNormal;
    public GameObject CamaraAutoDeportivo;

    public void BotonHome()
    {
        selectorDePersonajes.SetActive(false);
        Time.timeScale = 1f;
    }
}
