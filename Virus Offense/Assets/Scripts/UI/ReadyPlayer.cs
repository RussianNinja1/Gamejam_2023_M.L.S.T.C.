using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReadyPlayer : MonoBehaviour
{
    [SerializeField] float timeUntilSpawn = 3;
    [SerializeField] float angleOfSpawnAnimation = 0;
    [SerializeField] float distanceToCenter = 35;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject playerObject;

    [Header("Spawn Objects")]
    [SerializeField] GameObject dummyBolt;

    Vector3 spawnPos;

    private void Start()
    {
        // Hide player and then do "SpawnPlayer" coroutine
        playerObject.GetComponent<PlayerHealth>().HidePlayer();
        StartCoroutine(SpawnPlayer());
    }

    IEnumerator SpawnPlayer()
    {
        spawnPos = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0);

        // Spawn playerBolt at camera center, and move it off screen to be dragged in
        GameObject playerbolt = Instantiate(dummyBolt, spawnPos, mainCamera.transform.rotation) as GameObject;
        playerbolt.transform.Rotate(new Vector3(0, 0, angleOfSpawnAnimation));
        playerbolt.transform.Translate(playerbolt.transform.up * distanceToCenter);
        playerbolt.GetComponent<SpriteRenderer>().enabled = true;

        // Let bolt go to center, destroy it, show player and make this object inactive
        playerbolt.transform.position = Vector3.MoveTowards(playerbolt.transform.position, spawnPos, distanceToCenter * Time.deltaTime / timeUntilSpawn);
        yield return new WaitForSeconds(timeUntilSpawn);
        Destroy(playerbolt);
        playerObject.GetComponent<PlayerHealth>().ShowPlayer();
        gameObject.SetActive(false);
        yield return null;
    }
}
