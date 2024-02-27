using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public string NombreDeLaEscena;

    public void CargarEscena()
    {
        SceneManager.LoadScene(NombreDeLaEscena);
        Time.timeScale = 1f;
    }
}