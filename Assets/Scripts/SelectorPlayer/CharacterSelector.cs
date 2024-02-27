using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    [Header("Autos")]
    [Space]
    public GameObject AutoDefault;
    public GameObject AutoNormal;
    public GameObject AutoDeportivo;
    [Header("Botones")]
    public GameObject UIAuto;

    [System.Serializable]
    public struct CharacterData
    {
        public string characterName;
        public GameObject characterPrefab;
        public GameObject samplePrefab;
        public Transform initialPosition; // Agregado: la posición inicial del auto
    }

    public GameObject selectorDePersonajes;
    public CharacterData[] characters;
    public GameObject characterDisplay;
    public Text characterNameText;
    public Button selectButton;
    private int currentIndex = 0;

    private GameObject activeCharacter;
    private Transform activeCharacterInitialPosition;

    void Start()
    {
        UpdateCharacterUI();
        selectButton.onClick.AddListener(SelectCharacter);
    }

    void UpdateCharacterUI()
    {
        if (activeCharacter != null)
        {
            // Desactiva el auto activo actual
            activeCharacter.SetActive(false);
            // Teletransporta el auto activo actual a su posición inicial
            activeCharacter.transform.position = activeCharacterInitialPosition.position;
        }

        // Desactiva todos los modelos de muestra
        foreach (var characterData in characters)
        {
            characterData.samplePrefab.SetActive(false);
        }

        // Activa el modelo de muestra del personaje actual
        characters[currentIndex].samplePrefab.SetActive(true);

        // Almacena la posición inicial del auto activo actual
        activeCharacterInitialPosition = characters[currentIndex].initialPosition;

        // Almacena el modelo del auto activo actual
        activeCharacter = characters[currentIndex].characterPrefab;

        // Activa el modelo de muestra del personaje actual
        characters[currentIndex].samplePrefab.SetActive(true);

        // Desactiva el personaje activo actual
        if (activeCharacter != null)
        {
            activeCharacter.SetActive(false);
        }

        // Muestra el nombre del personaje actual en el panel
        characterNameText.text = characters[currentIndex].characterName;
    }

    public void NextCharacter()
    {
        currentIndex = (currentIndex + 1) % characters.Length;
        UpdateCharacterUI();
    }

    public void PreviousCharacter()
    {
        currentIndex = (currentIndex - 1 + characters.Length) % characters.Length;
        UpdateCharacterUI();
    }

    public void SelectCharacter()
    {
        UIAuto.SetActive(true);

        Debug.Log("Personaje seleccionado: " + characters[currentIndex].characterName);
        selectorDePersonajes.SetActive(false);
        Time.timeScale = 1f;
        // Desactiva el modelo de muestra del personaje seleccionado
        characters[currentIndex].samplePrefab.SetActive(false);

        // Almacena la posición inicial del auto activo actual
        activeCharacterInitialPosition = characters[currentIndex].initialPosition;

        // Desactiva el personaje activo actual
        if (activeCharacter != null)
        {
            activeCharacter.SetActive(false);
        }

        // Teletransporta el auto activo actual a su posición inicial
        activeCharacter.transform.position = activeCharacterInitialPosition.position;

        // Activa el GameObject del nuevo personaje seleccionado
        activeCharacter = characters[currentIndex].characterPrefab;
        if (activeCharacter != null)
        {
            activeCharacter.SetActive(true);
        }
    }
}
