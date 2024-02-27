using UnityEngine;

public class BarcoMovement : MonoBehaviour
{
    public float velocidad = 5f;
    private bool comenzarMovimiento = false;

    void Update()
    {
        if (comenzarMovimiento)
        {
            MoverBarcoAutomaticamente();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            comenzarMovimiento = true;
        }
    }

    void MoverBarcoAutomaticamente()
    {
        // Mueve el barco hacia adelante frame por frame
        transform.position += Vector3.right * velocidad * Time.deltaTime;
    }
}
