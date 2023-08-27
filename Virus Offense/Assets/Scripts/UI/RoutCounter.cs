using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoutCounter : MonoBehaviour
{
    [SerializeField] GameObject levelExit;

    [Header("Display Settings")]
    [SerializeField] string statName;

    TMP_Text textDisplay;
    int totalEnemyCount;
    int enemies;
    int spawnerCount;
    GameObject[] spawners;

    private void Start()
    {
        textDisplay = gameObject.GetComponent<TMP_Text>();
        UpdateDisplay(0);

        levelExit.SetActive(false);
    }

    private void Update()
    {
        // Count all enemies and add to totalEnemyCount
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        totalEnemyCount = enemies + spawnerCount;
        UpdateDisplay(totalEnemyCount);

        // If totalEnemycount reaches 0 and levelExit is disabled, enable it.
        if (totalEnemyCount == 0 && levelExit.activeSelf == false)
        {
            levelExit.SetActive(true);
        }
    }

    // Update the Stat Display text with a whole number
    public void UpdateDisplay(int wholeNumber)
    {
        textDisplay.text = statName + ":\n" + wholeNumber.ToString();
    }

    // Add or subtract to spawnerCount
    public void UpdateSpawnerCount(int numberOfEnemies)
    {
        spawnerCount += numberOfEnemies;
    }
}