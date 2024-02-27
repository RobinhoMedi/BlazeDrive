using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Objeto al que seguirá la cámara
    public Vector3 offset;

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}
