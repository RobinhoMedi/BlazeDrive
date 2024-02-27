using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Policia"))
        {
            Debug.Log("CayoLaPoli");
        } 
    }
}
