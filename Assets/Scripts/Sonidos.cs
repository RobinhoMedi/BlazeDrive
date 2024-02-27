using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Sonidos : MonoBehaviour
{
    public AudioSource Bocina;


    public void BotonBocina()
    {
        Bocina.Play();
    }
}
