using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] string nextLevelName = "Level_2";
    [SerializeField] string upgradeScreenName = "UpgradeScene";

    [SerializeField] float timeUntilLoad = 3;
    [SerializeField] float angleOfSpawnAnimation = 0;
    [SerializeField] float distance = 35;
    [SerializeField] GameObject playerObject;

    [Header("Spawn Objects")]
    [SerializeField] GameObject dummyBolt;

    bool isEnd = false;

    // Trigger Level Transition
    public void TurnOnTransition()
    {
        if (isEnd == false)
        {
            isEnd = true;
            playerObject.GetComponent<PlayerHealth>().HidePlayer();
            StartCoroutine(LevelTransition());
        }
    }

    // Shoot out dummy bolt, and load next scene
    IEnumerator LevelTransition()
    {
        // Spawn playerBolt at camera center
        GameObject playerbolt = Instantiate(dummyBolt, playerObject.transform.position, playerObject.transform.rotation) as GameObject;
        playerbolt.transform.Rotate(new Vector3(0, 0, angleOfSpawnAnimation));
        playerbolt.GetComponent<SpriteRenderer>().enabled = true;

        // Let bolt go to away from center, destroy it and load upgrade scene
        for (float t = 0; t < timeUntilLoad; t += Time.deltaTime) 
        {
            playerbolt.transform.Translate(playerbolt.transform.up * distance * Time.deltaTime / timeUntilLoad);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(playerbolt);

        SaveData.nextSceneName = nextLevelName;
        SceneManager.LoadScene(upgradeScreenName);
    }
}
