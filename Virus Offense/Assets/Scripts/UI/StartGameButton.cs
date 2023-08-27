using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] string firstLevelName = "Level_1";


    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Save the data from prefab settings, and then start game
    void StartGame()
    {
        SaveData.maxHealth = playerPrefab.GetComponent<PlayerHealth>().maxHealth;
        SaveData.speed = playerPrefab.GetComponent<PlayerMovement>().moveSpeed;

        SaveData.baseMaxHealth = playerPrefab.GetComponent<PlayerHealth>().maxHealth;
        SaveData.baseSpeed = playerPrefab.GetComponent<PlayerMovement>().moveSpeed;

        SceneManager.LoadScene(firstLevelName);
    }
}
