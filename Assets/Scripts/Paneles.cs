using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paneles : MonoBehaviour
{
    public GameObject PanelPausa;
    public GameObject MiniMapa;
    public GameObject UIAUTO;
    public GameObject BotonPausa;


    public void Pausa()
    {
        PanelPausa.SetActive(true);
        MiniMapa.SetActive(false);
        UIAUTO.SetActive(false);
        BotonPausa.SetActive(false);
        Time.timeScale = 0f;
    }
    public void SalirPausa()
    {
        PanelPausa.SetActive(false);
        MiniMapa.SetActive(true);
        UIAUTO.SetActive(true);
        BotonPausa.SetActive(true);
        Time.timeScale = 1f;
    }
}
