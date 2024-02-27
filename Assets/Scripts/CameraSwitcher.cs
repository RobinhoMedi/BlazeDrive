using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject characterSelectorCamera;
    public GameObject[] autoCameras;

    private void Start()
    {
        // Desactiva todas las cámaras de los autos al inicio
        foreach (var autoCamera in autoCameras)
        {
            autoCamera.SetActive(false);
        }
    }

    public void SwitchToCharacterSelectorCamera()
    {
        characterSelectorCamera.SetActive(true);

        // Desactiva todas las cámaras de los autos
        foreach (var autoCamera in autoCameras)
        {
            autoCamera.SetActive(false);
        }
    }

    public void SwitchToAutoCamera(int index)
    {
        characterSelectorCamera.SetActive(false);

        // Activa la cámara específica del auto seleccionado
        if (index >= 0 && index < autoCameras.Length)
        {
            autoCameras[index].SetActive(true);
        }
    }
}
