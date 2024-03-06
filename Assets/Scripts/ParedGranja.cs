using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedGranja : MonoBehaviour
{
    public GameObject Ovni;
    public GameObject EfectoOvni;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Ovni.SetActive(true);
            Instantiate(EfectoOvni, transform.position, Quaternion.identity);
        }
    }
}
