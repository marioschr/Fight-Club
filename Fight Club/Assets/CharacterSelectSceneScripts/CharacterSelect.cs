using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour // Script για την επιλογή χαρακτήρα
{



    [SerializeField] private GameObject characterSelectDisplay = default;
    [SerializeField] private Transform characterPreviewParent = default;
    [SerializeField] private TMP_Text characterNameText = default;
    [SerializeField] private Character[] characters = default;

    private int currentCharacterIndex = 0;
    private List<GameObject> characterInstances = new List<GameObject>();

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
 
    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
 
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
        GameObject.FindGameObjectWithTag("MainMenuUI Canvas").transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Start()
    {
        foreach(var character in characters)
        {
            GameObject characterInstance =
                Instantiate(character.CharacterPreviewPrefab, characterPreviewParent);

            characterInstance.SetActive(false);

            characterInstances.Add(characterInstance);
        }

        characterInstances[currentCharacterIndex].SetActive(true);
        characterNameText.text = characters[currentCharacterIndex].CharacterName;

        characterSelectDisplay.SetActive(true);
    }

    public void Select()
    {
        PlayerPrefs.SetInt("playerPrefab", currentCharacterIndex);
        GameObject.FindGameObjectWithTag("MainMenuUI Canvas").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Multiplayer Canvas").transform.GetChild(0).transform.GetChild(5).gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync(GameConstants.CHARACTER_SELECT_INDEX);
    }

    public void Right()
    {
        characterInstances[currentCharacterIndex].SetActive(false);

        currentCharacterIndex = (currentCharacterIndex + 1) % characterInstances.Count;

        characterInstances[currentCharacterIndex].SetActive(true);
        characterNameText.text = characters[currentCharacterIndex].CharacterName;
    }

    public void Left()
    {
        characterInstances[currentCharacterIndex].SetActive(false);

        currentCharacterIndex--;
        if(currentCharacterIndex < 0)
        {
            currentCharacterIndex += characterInstances.Count;
        }

        characterInstances[currentCharacterIndex].SetActive(true);
        characterNameText.text = characters[currentCharacterIndex].CharacterName;
    }
}
