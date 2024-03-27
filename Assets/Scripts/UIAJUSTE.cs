using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAJUSTE : MonoBehaviour
{
    public GameObject PanelPausa;
    public GameObject PanelAjuste;
    public GameObject MiniMapa;
    public GameObject BotonPausa;

    public void Ajuste()
    {
        PanelPausa.SetActive(false);
        PanelAjuste.SetActive(true);
    }
    public void SalirAjuste()
    {   
        PanelAjuste.SetActive(false);
        MiniMapa.SetActive(true);
        BotonPausa.SetActive(true);
        Time.timeScale = 1f;
    }
}
