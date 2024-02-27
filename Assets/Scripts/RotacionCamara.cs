using UnityEngine;
using Cinemachine;

public class RotacionCamara : MonoBehaviour
{
    public Transform personaje;
    public float sensibilidadMouse = 2f;

    private CinemachineFreeLook freeLookCamera;

    void Start()
    {
        // Obtener la referencia al componente CinemachineFreeLook
        freeLookCamera = GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        float movimientoMouseX = Input.GetAxis("Mouse X");
        float movimientoMouseY = Input.GetAxis("Mouse Y");

        // Rotar la cámara solo en el eje Y (horizontal)
        freeLookCamera.m_XAxis.Value += movimientoMouseX * sensibilidadMouse;

        // Obtener la rotación actual en el eje X (vertical)
        float rotacionActualX = freeLookCamera.m_YAxis.Value;

        // Limitar la rotación en el eje X entre -80 y 80 grados
        float nuevaRotacionX = Mathf.Clamp(rotacionActualX - movimientoMouseY * sensibilidadMouse, -80f, 80f);

        // Aplicar la rotación solo en el eje X (vertical)
        freeLookCamera.m_YAxis.Value = nuevaRotacionX;
    }
}
