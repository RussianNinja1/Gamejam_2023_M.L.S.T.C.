using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeStats : MonoBehaviour
{
    [SerializeField] float waitUntilStart = 5;
    [SerializeField] float waitUntilFinish = 5;
    [SerializeField] GameObject upgradeSystem;

    [Header("Upgrade Percentage Amounts")]
    [SerializeField] float maxHealthPercent = 10;
    [SerializeField] float speedPercent = 7;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowUpgrades());
    }

    // When any button is pressed, do appropriate upgrade, hide system and load new scene
    public void UpgradeChoice(int choice)
    {
        switch (choice)
        {
            case 0: // Skipped
                break;
            case 1: // More HP
                SaveData.maxHealth += SaveData.baseMaxHealth * maxHealthPercent / 100;
                break;
            case 2: // More Speed
                SaveData.speed += SaveData.baseSpeed * speedPercent / 100;
                break;
        }

        StartCoroutine(HideUpgrades());
    }

    IEnumerator ShowUpgrades()
    {
        yield return new WaitForSeconds(waitUntilStart);
        upgradeSystem.SetActive(true);
    }

    IEnumerator HideUpgrades()
    {
        upgradeSystem.SetActive(false);
        yield return new WaitForSeconds(waitUntilFinish);
        SceneManager.LoadScene(SaveData.nextSceneName);
    }
}
